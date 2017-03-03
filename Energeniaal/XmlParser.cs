using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Energeniaal
{
    class XmlParser
    {
        public DateTime LastTime { get; private set; }
        public double LastTemp { get; private set; }
        public int MessageQueueCount { get { return wattsDataQueue.Count; } }
        public static int MaxChannels { get { return maxChannels; } }
        public static int MaxSensors { get { return maxSensors; } }
        private static int maxChannels = Math.Max(Energeniaal.Properties.Settings.Default.MaxChannels, 1);
        private static int maxSensors =  Math.Max(Energeniaal.Properties.Settings.Default.MaxSensors, 1);
        private Queue<WattsMessageData> wattsDataQueue;
        public const string MessageStart = "<msg>";
        public const string MessageEnd = "</msg>";
        private const string ChannelPath = "/msg/ch";
        private const string DSBPath = "/msg/dsb";
        private const string DSWPath = "/msg/hist/dsw";
        private const string ImpulsePath = "/msg/imp";
        private const string SensorPath = "/msg/sensor";
        private const string TimePath = "/msg/time";
        private const char TwoHourElementStart = 'h';
        private const char DayElementStart = 'd';
        private const char MonthElementStart = 'm';
        private int databaseDaysSinceWipe; // days since wipe associated with history data currently in database
        private Database database;

        public XmlParser()
        {
            InitializeLastValues();
            database = new Database();
            wattsDataQueue = new Queue<WattsMessageData>();
            databaseDaysSinceWipe = database.GetDaysSinceWipe();
        }

        private void InitializeLastValues()
        {
            LastTime = DateTime.MinValue;
            LastTemp = 0;
        }

        public WattsMessageData GetWattsMessageDataFromQueue()
        {
            return wattsDataQueue.Dequeue();
        }

        public void ParseMessage(string message)
        {
            XPathDocument doc = null;
            XPathNavigator testNav;
            XmlReaderSettings set = new XmlReaderSettings();
            set.ConformanceLevel = ConformanceLevel.Fragment;
            try
            {
                doc = new XPathDocument(
                            XmlReader.Create(new StringReader(message), set));
            }
            catch (XmlException)
            {
                // message was probably somehow garbled, don't bother user with error
                return;
            }
            XPathNavigator navigator = doc.CreateNavigator();

            navigator = navigator.SelectSingleNode(TimePath);
            if (navigator == null)
            {
                throw new Exception("Unexpected XML message format: no time information");
            }
            LastTime = navigator.ValueAsDateTime;
            navigator.MoveToNext();
            if (navigator.Name == "tmpr")
            {
                // watts or meter impulse
                // parse temperature
                LastTemp = navigator.ValueAsDouble;
                testNav = navigator.SelectSingleNode(ImpulsePath);
                if (testNav == null)
                {
                    // watts
                    ParseWattsMessage(navigator);

                    // TESTING: generate mock message data to test multiple channels
                    /*
                    List<WattsMessageData> messageDataList = TestData.GenerateWattsMessageData();
                    foreach (WattsMessageData messageData in messageDataList)
                    {
                        wattsDataQueue.Enqueue(messageData);
                    }
                    */
                }
            }
            else if (navigator.Name == "hist")
            {
                // history
                ParseHistoryMessage(navigator);
            }
            else
            {
                throw new Exception("Unexpected XML message format");
            }          
        } // end parseMessage

        // parse watts channels in watts message and fill array LastWatts 
        // for this sensor
        private void ParseWattsMessage(XPathNavigator navigator)
        {
            XPathNavigator testNav;

            // which sensor is it?
            testNav = navigator.SelectSingleNode(SensorPath);
            if (testNav == null)
            {
                throw new Exception("Unexpected XML message format: no sensor information");
            }
            int sensor = testNav.ValueAsInt;
            if (sensor > maxSensors - 1) // check this because maxSensors is configurable
                return;

            // parse watts channels 
            for (int channel = 1; channel < maxChannels + 1; channel++)
            {
                testNav = navigator.SelectSingleNode
                    (ChannelPath + (channel).ToString() + "/watts");

                if (testNav != null)
                {
                    // fill WattsMessageData with data from xml message and add it to queue
                    int watts = testNav.ValueAsInt;
                    WattsMessageData messageData
                        = new WattsMessageData(LastTime, LastTemp, sensor, channel, watts);
                    wattsDataQueue.Enqueue(messageData);
                }
            }
        } // end parseWattsMessage

        // parse history message
        private void ParseHistoryMessage(XPathNavigator navigator)
        {
            // get days since birth
            XPathNavigator testNav = navigator.SelectSingleNode(DSBPath);
            if (testNav == null)
            {
                throw new Exception("Unexpected XML message format: no \"days since birth\"");
            }
            int daysSinceBirth = testNav.ValueAsInt;

            // get days since wipe
            testNav = navigator.SelectSingleNode(DSWPath);
            if (testNav == null)
            {
                throw new Exception("Unexpected XML message format: no \"days since wipe\"");
            }
            int daysSinceWipe = testNav.ValueAsInt;

            // Compare to days since wipe associated with history data currently in database
            // to see if a newer device has been plugged in, or the device's history has been wiped.
            // If so, we need to purge all the history data before adding more
            if (daysSinceWipe < databaseDaysSinceWipe)
            {
                database.PurgeAllHistoryData();          
            }

            if (daysSinceWipe != databaseDaysSinceWipe)
            {
                database.SaveDaysSinceWipe(daysSinceWipe);
                databaseDaysSinceWipe = daysSinceWipe;
            }
 
            XPathNodeIterator dataNodes = navigator.SelectChildren("data", String.Empty);

            foreach (XPathNavigator data in dataNodes)
            {
                // daysSinceBirth and daysSinceWipe are used to calculate timestamp for month data
                ParseHistoryData(data, daysSinceBirth, daysSinceWipe);
            }
        }

        // parse data node of history message and write data to database
        private void ParseHistoryData(XPathNavigator navigator, int daysSinceBirth, int daysSinceWipe)
        {
            List<HistoryDataPoint> historyData = new List<HistoryDataPoint>();
            int sensor = 0;
            char elementStart = '0';

            XPathNodeIterator days = navigator.SelectChildren(XPathNodeType.Element);
            foreach (XPathNavigator day in days)
            {
                // get sensor number from the first child 
                if (day.Name == "sensor")
                {
                    sensor = day.ValueAsInt;
                    if (sensor > maxSensors - 1) // check this because maxSensors is configurable
                        return;
                }
                else if (day.Name.Length > 1)
                {
                    elementStart = day.Name[0];
                    int timeUnitsAgo = Convert.ToInt32(day.Name.Substring(1));
                    // timestamp is last odd hour
                    DateTime timeStamp = CalculateTimeStamp(LastTime, elementStart, timeUnitsAgo, 
                                            Math.Min(daysSinceBirth, daysSinceWipe));                 
                    double kwh = day.ValueAsDouble;
                    // Only save history data with non-zero value
                    if (kwh != 0)
                    {
                        HistoryDataPoint point = new HistoryDataPoint(timeStamp, sensor, kwh);
                        historyData.Add(point);
                    }                    
                }
            }

            // write to database
            switch (elementStart)
            {
                case TwoHourElementStart:
                    database.SaveTwoHourHistory(historyData);
                    break;
                case DayElementStart:
                    database.SaveDayHistory(historyData);
                    break;
                case MonthElementStart:
                    database.SaveMonthHistory(historyData);
                    break;
                default:
                    throw new Exception("XML contains unknown history type");
            }

        } // end parseHistoryData

        // calculate timestamp, depending on type of history
        // daysSince is the smaller of daysSinceBirth and daysSinceWipe, used to 
        // begin date for "month" data, which is actually 30-day periods
        private DateTime CalculateTimeStamp(DateTime dt, char elementStart, int timeUnitsAgo, int daysSince)
        {
            DateTime timeStamp = new DateTime(dt.Year, dt.Month, dt.Day,
                                              dt.Hour, 0, 0);

            // set time to last odd hour, in case history dump was done at nonstandard time
            if (timeStamp.Hour % 2 == 0)
            {
                timeStamp = timeStamp.AddHours(-1);
            }

            // calculate time based on timeUnitsAgo, which came out of xml tag
            // set day for month history to start of 30-day period
            // set hour for day history and month history to 0
            switch (elementStart)
            {
                case TwoHourElementStart:
                    timeStamp = timeStamp.AddHours(-timeUnitsAgo);
                    break;
                case DayElementStart:
                    timeStamp = timeStamp.AddDays(-timeUnitsAgo);
                    timeStamp = timeStamp.AddHours(-timeStamp.Hour);
                    break;
                case MonthElementStart:
                    timeStamp = timeStamp.AddDays(-(timeUnitsAgo * 30 + daysSince % 30 + 1));
                    timeStamp = timeStamp.AddHours(-timeStamp.Hour);
                    break;
            }  

            return timeStamp;
        }

    }
}
