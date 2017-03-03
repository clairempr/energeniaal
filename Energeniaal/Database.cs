using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Energeniaal
{
    class Database
    {
        private const string TwoHourHistoryTable = "TwoHourHistory";
        private const string DayHistoryTable = "DayHistory";
        private const string MonthHistoryTable = "MonthHistory";
        private const string DaysSinceWipeTable = "DaysSinceWipe";
        private const string HistoryFieldsToSelect = "TimeStamp, Sensor, Kwh";
        private static string DataSource = System.Windows.Forms.Application.LocalUserAppDataPath + @"\HistoryData.s3db";

        public Database()
        {
            // creates new database and tables, if necessary
            InitializeDatabase();
        }

        public int GetDaysSinceWipe()
        {
            int dsw = 0;
            string query = "SELECT DAYSSINCEWIPE FROM " + DaysSinceWipeTable + ";";
            DataTable dt = GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                dsw = Convert.ToInt32(dt.Rows[0]["DaysSinceWipe"]);
            }

            return dsw;
        }

        public List<HistoryDataPoint> GetTwoHourHistory()
        {
            return GetHistory(TwoHourHistoryTable);
        }

        public List<HistoryDataPoint> GetTwoHourHistory(int sensor)
        {
            return GetHistory(TwoHourHistoryTable, sensor);
        }

        public List<HistoryDataPoint> GetDayHistory()
        {
            return GetHistory(DayHistoryTable);
        }

        public List<HistoryDataPoint> GetDayHistory(int sensor)
        {
            return GetHistory(DayHistoryTable, sensor);
        }

        public List<HistoryDataPoint> GetDayHistoryRange(int sensor, DateTime startDate, DateTime endDate)
        {
            return GetHistory(DayHistoryTable, sensor, startDate, endDate);
        }

        public List<HistoryDataPoint> GetMonthHistory()
        {
            return GetHistory(MonthHistoryTable);
        }

        public List<HistoryDataPoint> GetMonthHistory(int sensor)
        {
            return GetHistory(MonthHistoryTable, sensor);
        }

        private List<HistoryDataPoint> GetHistory(string tableName)
        {
            string query = "SELECT " + HistoryFieldsToSelect + " FROM " + tableName + ";";
            return DoHistoryQuery(query);
        }

        private List<HistoryDataPoint> GetHistory(string tableName, int sensor)
        {
            string query = "SELECT " + HistoryFieldsToSelect + " FROM " + tableName + " WHERE SENSOR = " + sensor + ";";
            return DoHistoryQuery(query);
        }

        private List<HistoryDataPoint> GetHistory(string tableName, int sensor, DateTime startDate, DateTime endDate)
        {
            string startDateString = startDate.ToString("yyyyMMdd");
            string endDateString = endDate.ToString("yyyyMMdd");
            string query = "SELECT " + HistoryFieldsToSelect + " FROM " + tableName + " WHERE SENSOR = " + sensor
                + " AND STRFTIME('%Y%m%d', TIMESTAMP) >= '" + startDateString
                + "' AND STRFTIME('%Y%m%d', TIMESTAMP) <= '" + endDateString + "';";
            return DoHistoryQuery(query);
        }

        private List<HistoryDataPoint> DoHistoryQuery(string query)
        {
            List<HistoryDataPoint> result = new List<HistoryDataPoint>();
            DataTable dt = GetDataTable(query);

            foreach (DataRow row in dt.Rows)
            {
                HistoryDataPoint point = new HistoryDataPoint();
                point.TimeStamp = Convert.ToDateTime(row["TimeStamp"]);
                point.Sensor = Convert.ToInt32(row["Sensor"]);
                point.Kwh = Convert.ToDouble(row["kWh"]);
                result.Add(point);
            }

            return result;
        }

        public void SaveDaysSinceWipe(int dsw)
        {
            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "UPDATE " + DaysSinceWipeTable + " SET DAYSSINCEWIPE = (@dsw);";
                    command.Parameters.AddWithValue("@dsw", dsw);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SaveTwoHourHistory(List<HistoryDataPoint> history)
        {
            InsertHistoryData(history, TwoHourHistoryTable);
        }

        public void SaveDayHistory(List<HistoryDataPoint> history)
        {
            InsertHistoryData(history, DayHistoryTable);
        }

        public void SaveMonthHistory(List<HistoryDataPoint> history)
        {
            // First remove any month history data roughly within the same date range 
            // to prevent double data being stored due to incorrectly set date/time
            // on PC or time on device
            if (history.Count > 1)
            {
                int sensor = history[0].Sensor;
                DateTime startDate = history[0].TimeStamp.AddDays(-2);
                DateTime endDate = history[history.Count - 1].TimeStamp.AddDays(2);
                ClearExistingMonthDataForSensorInRange(sensor, startDate, endDate);
            }

            InsertHistoryData(history, MonthHistoryTable);
        }


        private void ClearExistingMonthDataForSensorInRange(int sensor, DateTime startDate, DateTime endDate)
        {
            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "DELETE FROM " + MonthHistoryTable +
                            " WHERE SENSOR = (@sensor) AND TIMESTAMP >= (@startDate) AND TIMESTAMP <= (@endDate) ;";
                        command.Parameters.AddWithValue("@sensor", sensor);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        // throw out all data because device's history has been wiped or a newer
        // device has been plugged in or user has initiated wipe
        public void PurgeAllHistoryData()
        {
            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    DeleteTable(connection, TwoHourHistoryTable);
                    DeleteTable(connection, DayHistoryTable);
                    DeleteTable(connection, MonthHistoryTable);

                    transaction.Commit();
                }

                // Cannot VACUUM from within a transaction
                Vacuum(connection);
            }

            // Set DaysSinceWipe back to 0
            // Outside of connection, because it opens its own
            SaveDaysSinceWipe(0);
        }

        private void DeleteTable(SQLiteConnection connection, string tableName)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM " + tableName + ";";
                command.ExecuteNonQuery();
                //command.CommandText = "VACUUM;";
                //command.ExecuteNonQuery();
            }
        }

        private void DeleteTable(SQLiteConnection connection, HistoryType historyType)
        {
            string tableName = string.Empty;

            switch (historyType)
            {
                case HistoryType.TwoHour:
                    tableName = TwoHourHistoryTable;
                    break;
                case HistoryType.Day:
                    tableName = DayHistoryTable;
                    break;
                case HistoryType.Month:
                    tableName = MonthHistoryTable;
                    break;
            }

            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM " + tableName + ";";
                command.ExecuteNonQuery();
            }
        }

        private void Vacuum(SQLiteConnection connection)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "VACUUM;";
                command.ExecuteNonQuery();
            }
        }

        // throw out data that's older than what the device stores in its own history
        // to avoid potential gaps in database data
        public void PurgeOldHistoryData()
        {
            // getting date from PC, because there's no guarantee device will be connected
            // time may vary a little bit from the device's time, but it's not a disaster 
            // if an extra two-hour data point gets deleted or kept
            DateTime dt = DateTime.Now;
            // set time to last odd hour, because history is dumped every odd hour
            if (dt.Hour % 2 == 0)
            {
                dt = dt.AddHours(-1);
            };

            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    PurgeOldHistoryDataFromTable(connection, TwoHourHistoryTable,
                                    dt.AddDays(-Energeniaal.Properties.Settings.Default.MaxDaysTwoHourHistory));
                    PurgeOldHistoryDataFromTable(connection, DayHistoryTable,
                                    dt.AddDays(-Energeniaal.Properties.Settings.Default.MaxDaysDayHistory));
                    PurgeOldHistoryDataFromTable(connection, MonthHistoryTable,
                                    dt.AddDays(-Energeniaal.Properties.Settings.Default.MaxMonthsMonthHistory * 30));

                    transaction.Commit();
                }
            }
        }

        private void PurgeOldHistoryDataFromTable(SQLiteConnection connection, string tableName, DateTime deleteBeforeDate)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM " + tableName + " WHERE TIMESTAMP < (@timestamp);";
                command.Parameters.AddWithValue("@timestamp", deleteBeforeDate);
                command.ExecuteNonQuery();
            }
        }

        public bool ImportHistoryData(HistoryType historyType, string fileName)
        {
            List<HistoryDataPoint> history = new List<HistoryDataPoint>();

            using (StreamReader importFile = new StreamReader(fileName))
            {
                string[] columnHeaders = MyStrings.ExportColumnHeaders.Split(';');
                string line;
                int lineNumber = 0;
                System.Globalization.CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentCulture;

                while ((line = importFile.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] fields = line.Split(';');

                    if (fields.Length < 4)
                    {
                        return false;
                    }

                    if (lineNumber == 1)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (fields[i] != columnHeaders[i])
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        history.Add(ImportHistoryDataPoint(fields, culture));
                    }
                }
            }

            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                DeleteTable(connection, historyType);

                switch (historyType)
                {
                    case HistoryType.TwoHour:
                        SaveTwoHourHistory(history);
                        break;
                    case HistoryType.Day:
                        SaveDayHistory(history);
                        break;
                    case HistoryType.Month:
                        SaveMonthHistory(history);
                        break;
                }

                Vacuum(connection);
            }

            return true;
        }

        private HistoryDataPoint ImportHistoryDataPoint(string[] fields, System.Globalization.CultureInfo culture)
        {
            HistoryDataPoint dataPoint = new HistoryDataPoint();

            dataPoint.TimeStamp = DateTime.Parse(fields[0], culture);
            dataPoint.Kwh = Convert.ToDouble(fields[2], culture);
            dataPoint.Sensor = Convert.ToInt32(fields[3]);
            
            return dataPoint;
        }

        private SQLiteConnection OpenDatabaseConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(@"Data Source=" + DataSource + ";Version=3;");
            connection.Open();
            return connection;
        }

        // If database doesn't exist yet, create one with empty tables
        // If table doesn't exist, create it
        private void InitializeDatabase()
        {
            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        List<string> tables
                            = new List<string> { TwoHourHistoryTable, DayHistoryTable, MonthHistoryTable, DaysSinceWipeTable };
                        foreach (string tableName in tables)
                        {
                            if (!TableExists(tableName))
                            {
                                command.CommandText = GetTableCreationString(tableName);
                                command.ExecuteNonQuery();

                                // Insert initial record into Days Since Wipe so our later updates won't fail
                                if (tableName == DaysSinceWipeTable)
                                {
                                    command.CommandText = "INSERT INTO " + DaysSinceWipeTable + " VALUES (0)";
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        private bool TableExists(string tableName)
        {
            string query = "SELECT name FROM sqlite_master WHERE type='table' AND name='" + tableName + "';";
            DataTable dt = GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        // The 3 history tables all have the same fields, but
        // the days since wipe table is different
        private string GetTableCreationString(string tableName)
        {
            if (tableName == DaysSinceWipeTable)
            {
                return @"CREATE TABLE [DaysSinceWipe] (
                            [DaysSinceWipe] INTEGER DEFAULT '0' NOT NULL PRIMARY KEY
                        )";
            }
            else
            {
                return "CREATE TABLE [" + tableName + @"] (
                            [Key] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [TimeStamp] DATETIME  NULL,
                            [Sensor] INTEGER  NULL,
                            [Kwh] REAL NULL,
                            UNIQUE (TimeStamp, Sensor)
                        );";

            }
        }

        private DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = sql;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }

        // Allows the programmer to easily insert into the DB
        private void InsertHistoryData(List<HistoryDataPoint> data, String tableName)
        {
            using (SQLiteConnection connection = OpenDatabaseConnection())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        string columns = "TIMESTAMP, SENSOR, KWH";
                        command.CommandText = String.Format("INSERT OR REPLACE INTO {0}({1}) VALUES(?, ?, ?);",
                                    tableName, columns);
                        SQLiteParameter timestampParam = new SQLiteParameter("@timestamp");
                        SQLiteParameter sensorParam = new SQLiteParameter("@sensor");
                        SQLiteParameter kwhParam = new SQLiteParameter("@kwh");
                        command.Parameters.Add(timestampParam);
                        command.Parameters.Add(sensorParam);
                        command.Parameters.Add(kwhParam);

                        foreach (HistoryDataPoint point in data)
                        {
                            //timestampParam.Value = point.TimeStamp.ToOADate();
                            timestampParam.Value = point.TimeStamp;
                            sensorParam.Value = point.Sensor;
                            kwhParam.Value = point.Kwh;
                            command.ExecuteNonQuery();
                        }
                    } // end using (SQLiteCommand command = new SQLiteCommand(connection))
                    transaction.Commit();
                }
            }
        }

    }
}
