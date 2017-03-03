using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace Energeniaal
{
    public class PortRead
    {
        public DateTime LastTime { get { return xmlParser.LastTime; } }
        public double LastTemp { get { return xmlParser.LastTemp; } }
        public int MessageQueueCount { get { return xmlParser.MessageQueueCount; } }
        public int MaxChannels { get { return XmlParser.MaxChannels; } }
        public int MaxSensors { get { return XmlParser.MaxSensors; } }
        public enum State { Connected, Connecting, Disconnected, Disconnecting };
        public State CurrentState { get; private set; }
        private XmlParser xmlParser;
        private Database database;
        private int connectionTimeout = Energeniaal.Properties.Settings.Default.ConnectionTimeout;
        private int readTimeout = Energeniaal.Properties.Settings.Default.ReadTimeout;
        private bool continueReading;

        public PortRead()
        {
            xmlParser = new XmlParser();
            database = new Database();
            CurrentState = State.Disconnected;
            // throw out data that's older than what the device stores in its own history
            // to avoid potential gaps in database data
            database.PurgeOldHistoryData();
        }

        public WattsMessageData GetWattsMessageDataFromQueue()
        {
            return xmlParser.GetWattsMessageDataFromQueue();
        }

        public void ConnectToSerialPort(string portName)
        {
            CurrentState = State.Connecting;

            try
            {
                using (SerialPort serialPort = new SerialPort(portName, 57600))
                {
                    serialPort.ReadTimeout = connectionTimeout;
                    // set various serial port properties and open it
                    InitializeSerialPort(serialPort);
                    serialPort.ReadLine();
                }
            }
            catch (Exception)
            {
                CurrentState = State.Disconnected;
                throw;
            }

            CurrentState = State.Connected;
        }

        public void ReadFromSerialPort(string portName, string logFileName)
        {
            CurrentState = State.Connected;
            continueReading = true;
            StreamWriter writer = null;

            try
            {
                using (SerialPort serialPort = new SerialPort(portName, 57600))
                {
                    serialPort.ReadTimeout = readTimeout;
                    // set various serial port properties and open it
                    InitializeSerialPort(serialPort);
                    // if a logfile was specified, open one
                    if (logFileName != "")
                    {
                        writer = new StreamWriter(logFileName, false); // overwrite
                    }

                    while (continueReading)
                    {
                        string line;

                        try
                        {
                            line = serialPort.ReadLine();
                        }
                        catch (IOException)
                        {
                            // default exception message is kind of confusing, so supply our own
                            throw new Exception("Could not read from device.");
                        }

                        List<string> messages = GetCompleteMessagesFromLine(line);

                        foreach (string message in messages)
                        {
                            xmlParser.ParseMessage(message);
                            if (logFileName != "")
                            {
                                writer.WriteLine(message);
                            }
                        }

                    } // end while (continueReading)
                } // end using (SerialPort serialPort...
            }
            catch (Exception)
            {
                StopReadingFromSerialPort();
                throw;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                CurrentState = State.Disconnected;
            }
        }

        private void InitializeSerialPort(SerialPort serialPort)
        {
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            serialPort.NewLine = "\r\n";
            serialPort.ReadBufferSize = 32768;
            // default timeout is infinite 
            // leave it that way because of long delays with history dump
            serialPort.Open();
        }

        // process xml line to make sure it's not two messages stuck together
        private List<string> GetCompleteMessagesFromLine(string line)
        {
            List<string> messages = new List<string>();

            // if we don't have a message start or end, 
            // then it's incomplete and we can't use it, so don't add anything to queue
            int start = line.IndexOf(XmlParser.MessageStart, StringComparison.InvariantCultureIgnoreCase);
            if (start == -1)
            {
                return messages;
            }
            else if (start > 0)
            {
                line = line.Substring(start);
            }

            int end = line.LastIndexOf(XmlParser.MessageEnd, StringComparison.InvariantCultureIgnoreCase);
            if (end == -1)
            {
                return messages;
            }
            else if (end < line.Length - XmlParser.MessageEnd.Length)
            {
                line = line.Remove(end + XmlParser.MessageEnd.Length);
            }

            string[] separator = { XmlParser.MessageStart };
            string[] possibleMessages = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string message in possibleMessages)
            {
                if (message.EndsWith(XmlParser.MessageEnd))
                {
                    messages.Add(XmlParser.MessageStart + message);
                }
                else if (Program.TestMode)
                {
                    throw new Exception("Incomplete message read from serial port:\n" + message);
                }
            }

            return messages;
        }

        public void StopReadingFromSerialPort()
        {
            if (CurrentState == State.Connected)
            {
                CurrentState = State.Disconnecting;
            }
            else
            {
                CurrentState = State.Disconnected;
            }
            continueReading = false;
        }

        // the following functions provide an interface to database functions
        public List<HistoryDataPoint> GetTwoHourHistory()
        {
            return database.GetTwoHourHistory();
        }

        public List<HistoryDataPoint> GetTwoHourHistory(int sensor)
        {
            return database.GetTwoHourHistory(sensor);
        }

        public List<HistoryDataPoint> GetDayHistory()
        {
            return database.GetDayHistory();
        }

        public List<HistoryDataPoint> GetDayHistory(int sensor)
        {
            return database.GetDayHistory(sensor);
        }

        public List<HistoryDataPoint> GetDayHistoryRange(int sensor, DateTime startDate, DateTime endDate)
        {
            return database.GetDayHistoryRange(sensor, startDate, endDate);
        }

        public List<HistoryDataPoint> GetMonthHistory()
        {
            return database.GetMonthHistory();
        }

        public List<HistoryDataPoint> GetMonthHistory(int sensor)
        {
            return database.GetMonthHistory(sensor);
        }

        public void PurgeAllHistoryData()
        {
            database.PurgeAllHistoryData();
        }

        public bool ImportHistoryData(HistoryType historyType, string filename)
        {
            return database.ImportHistoryData(historyType, filename); 
        }

    }

}

