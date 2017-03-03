using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Energeniaal
{
    public class TestData
    {
        public static List<WattsMessageData> GenerateWattsMessageData()
        {
            List<WattsMessageData> messageDataList = new List<WattsMessageData>();
            DateTime timeStamp = DateTime.Now;

            //for (int sensor = 0; sensor < XmlParser.MaxSensors; sensor++)
            for (int sensor = 0; sensor < 1; sensor++)
            {
                for (int channel = 1; channel < XmlParser.MaxChannels + 1; channel++)
                {
                    double temp = RandomNumber(21, 23);
                    int watts = 0;
                    switch (channel)
                    {
                        case 1:
                            watts = RandomNumber(25, 75);
                            break;
                        case 2:
                            watts = RandomNumber(125, 175);
                            break;
                        case 3:
                            watts = RandomNumber(225, 275);
                            break;
                    }
                    WattsMessageData messageData
                        = new WattsMessageData(timeStamp, temp, sensor, channel, watts);
                    messageDataList.Add(messageData);
                }
            }
            return messageDataList;
        }

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
