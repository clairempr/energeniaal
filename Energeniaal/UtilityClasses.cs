using System.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Energeniaal
{
    public enum HistoryType { Undefined, TwoHour, Day, Month, Year };

    public class HistoryDataPoint
    {
        public DateTime TimeStamp { get; set; }
        public int Sensor { get; set; }
        public double Kwh { get; set; }

        public HistoryDataPoint()
        {
        }

        public HistoryDataPoint(DateTime timeStamp, int sensor, double kwh)
        {
            TimeStamp = timeStamp;
            Sensor = sensor;
            Kwh = kwh;
        }
    }

    // all the data associated with one real-time message (one sensor and its channels)
    public class WattsMessageData
    {
        public DateTime TimeStamp { get; set; }
        public double Temp { get; set; }
        public int Sensor { get; set; }
        public int Channel { get; set; }
        public int Watts { get; set; }

        public WattsMessageData()
        {
            TimeStamp = DateTime.MinValue;
            Temp = 0;
            Sensor = 0;
            Channel = 0;
            Watts = 0;
        }

        public WattsMessageData(DateTime timeStamp, double temp,
                int sensor, int channel, int watts)
        {
            TimeStamp = timeStamp;
            Temp = temp;
            Sensor = sensor;
            Channel = channel;
            Watts = watts;
        }
    }

    public static class ErrorHelper
    {
        private static Regex pathRegex = new Regex(
            @"([a-z]:.+\\).+\.cs", RegexOptions.IgnoreCase);

        public static string GetSanitizedStackTrace(string stackTrace)
        {
            string sanitizedStackTrace = stackTrace;

            // Remove original source path from stack trace   
            Match match = pathRegex.Match(stackTrace);
            if (match.Success)
            {
                string path = match.Groups[1].Value;
                sanitizedStackTrace = stackTrace.Replace(path, "");
            }

            return sanitizedStackTrace;
        }

        public static string GetFormattedErrorMessage(Exception ex, string extraInfo)
        {
            string errorMessage = string.Empty;

            errorMessage = "An error has occurred in " + Energeniaal.Properties.Settings.Default.AppName + " version "
                + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "."
                + " Please contact the developers with the following information:\n\n";

            if (!string.IsNullOrWhiteSpace(extraInfo))
            {
                errorMessage += " " + extraInfo + " ";
            }

            errorMessage += ex.Message + "\n\nOperating system: " + Environment.OSVersion.ToString();

            if (Environment.Is64BitOperatingSystem)
            {
                errorMessage += ", 64-bit";
            }

            errorMessage += "\n.Net version: " + Environment.Version.ToString()
                + "\nStack trace:\n" + GetSanitizedStackTrace(ex.StackTrace);

            return errorMessage;
        }
    }

    // Custom sensor names stored in user settings as StringCollection, with each 
    // sensor name string having the following format: 
    // "1;Dryer"
    public static class SensorNameHandler
    {
        public static Dictionary<int, string> SensorNames
        {
            // StringCollection -> Dictionary<int, string>
            get
            {
                Dictionary<int, string> names = new Dictionary<int, string>();

                // Sensor 0 is "Whole house" and can't be changed
                names.Add(0, MyStrings.WholeHouseSensor);

                // Read custom sensor names from settings and add them to list
                StringCollection settingsNames = Energeniaal.Properties.Settings.Default.CustomSensorNames;
                if (settingsNames != null)
                {
                    foreach (string str in settingsNames)
                    {
                        // If valid entry, add it to dictionary, otherwise ignore and remove it
                        // from settings later
                        string[] words = str.Split(';');
                        if (words.Length == 2
                            && !(string.IsNullOrWhiteSpace(words[0]))
                            && !(string.IsNullOrWhiteSpace(words[0])))
                        {
                            int sensorNumber = 0;
                            // Ignore any sensor with number 0 or duplicate sensor
                            if (int.TryParse(words[0], out sensorNumber)
                                && sensorNumber > 0
                                && !names.ContainsKey(sensorNumber))
                            {
                                names.Add(sensorNumber, words[1]);
                            }
                        }
                    }
                }

                // Any sensor without a custom name gets a default name
                for (int i = 0; i < XmlParser.MaxSensors; i++)
                {
                    if (!names.ContainsKey(i))
                    {
                        names.Add(i, MyStrings.Sensor + " " + i);
                    }
                }

                return names;
            }

            // Dictionary<int, string> -> StringCollection
            set
            {
                StringCollection settingsNames = new StringCollection();
                foreach (KeyValuePair<int, string> pair in value)
                {
                    // don't save default sensor names like "Appliance 1"
                    // because the default varies from one language to another
                    if (pair.Value != MyStrings.Sensor + " " + pair.Key.ToString())
                    {
                        string str = pair.Key.ToString() + ";" + pair.Value;
                        settingsNames.Add(str);
                    }
                }

                Energeniaal.Properties.Settings.Default.CustomSensorNames = settingsNames;
                Energeniaal.Properties.Settings.Default.Save();
            }
        }
    }

}
