using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using System.Globalization;

namespace Energeniaal
{
    public partial class MainForm : Form, IReLocalizable
    {
        const string ExportSaveFilter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
        const string LogfileSaveFilter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        PortRead portRead;
        private bool emergencyExit = false; // if something goes wrong, bypass various shutdown things on exit
        private bool usingWMI = true; // if something goes wrong with WMI, we can stry to work around it
        private bool busySwitchingLanguage = false; // to stop screen updates while language is being changed
        private List<WattsDataPoint>[,] wattsData;
        private WqlEventQuery eventQuery = new WqlEventQuery(
                    "SELECT * FROM Win32_DeviceChangeEvent");
        private ManagementEventWatcher watcher;
        private BindingList<KeyValuePair<int, string>> minutesComboBoxItems = new BindingList<KeyValuePair<int, string>>();
        private static string variableLogoPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\VarLogo.png";

        public MainForm()
        {
            // If something goes wrong here, we need to show an error message and exit
            // because there's no point in continuing
            try
            {
                InitializeComponent();
                portRead = new PortRead();
                // Sensors (Appliances): whole house, sensor 1, sensor 2 ... sensor 9 (10 sensors)
                // Channels (Phases): total, phase 1, phase 2, phase 3 (3 channels + total)
                wattsData = new List<WattsDataPoint>[portRead.MaxSensors, portRead.MaxChannels + 1];
                for (int i = 0; i < portRead.MaxSensors; ++i)
                {
                    for (int j = 0; j < portRead.MaxChannels + 1; ++j)
                    {
                        wattsData[i, j] = new List<WattsDataPoint>();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowInitErrorMessageAndExit(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // If something goes wrong here, we need to show an error message and exit
            // because there's no point in continuing
            try
            {
                this.Text = Energeniaal.Properties.Settings.Default.AppName;
                SetText();
                FillConnectDropDown();
                // this property is not bound to an application setting, so not restored automatically
                // the other logfile stuff is bound
                noneToolStripMenuItem.Checked = !Energeniaal.Properties.Settings.Default.LogToFile;
                CreateLanguageRadioButtonItems();

                // If no logfile has been chosen yet, then "None" can't be unselected,
                // and don't show menu item with logfile name
                if (string.IsNullOrWhiteSpace(logFileNameToolStripMenuItem.Text))
                {
                    noneToolStripMenuItem.Enabled = false;
                    logFileNameToolStripMenuItem.Visible = false;
                }
                else
                {
                    noneToolStripMenuItem.Enabled = true;
                    logFileNameToolStripMenuItem.Visible = true;
                }

                // these contain units for the displayed values, but they're not yet relevant
                // make them invisible
                //tempUnitLabel.Visible = false;
                //wattsUnitLabel.Visible = false;

                InitializeComboBoxes();
                InitializeHistoryRadioButtons();
                UpdateAveragesChart();

                SetStatusText();

                if (usingWMI)
                {
                    watcher = new ManagementEventWatcher(eventQuery);
                    watcher.EventArrived +=
                                        new EventArrivedEventHandler(
                                        HandleDeviceChangeEvent);
                    // Start listening for events
                    watcher.Start();
                }

            }
            catch (Exception ex)
            {
                ShowInitErrorMessageAndExit(ex);
            }
        }

        private void ShowInitErrorMessageAndExit(Exception ex)
        {
            string errorMessage = ErrorHelper.GetFormattedErrorMessage(ex, "Error while initializing application.");
            MessageBox.Show(errorMessage);
            this.EmergencyExit();
        }

        private void ShowErrorMessageAndExit(Exception ex)
        {
            string errorMessage = ErrorHelper.GetFormattedErrorMessage(ex, string.Empty);
            MessageBox.Show(errorMessage);
            this.EmergencyExit();
        }

        private void EmergencyExit()
        {
            emergencyExit = true; // bypass shutdown stuff
            Application.Exit();
        }

        private void SetText()
        {
            aboutToolStripMenuItem.Text = MyStrings.About;
            applianceNamesToolStripMenuItem.Text = MyStrings.ApplianceNames;
            averagesTabPage.Text = MyStrings.Averages;
            channelChoiceLabel.Text = MyStrings.Channel;
            chooseLogfileToolStripMenuItem.Text = MyStrings.Choose;
            connectToolStripMenuItem.Text = MyStrings.ConnectMenu;
            dailyHistoryToolStripMenuItem.Text = MyStrings.DailyHistory;
            dayOfWeekRadioButton.Text = MyStrings.PerDayOfWeekRadioButton;
            dayRadioButton.Text = MyStrings.DayRadioButton;
            disconnectToolStripMenuItem.Text = MyStrings.DisconnectMenuItem;
            exitToolStripMenuItem.Text = MyStrings.ExitMenu;
            exportToolStripMenuItem.Text = MyStrings.ExportMenu;
            fileToolStripMenuItem.Text = MyStrings.FileMenu;
            helpToolStripMenuItem.Text = MyStrings.HelpMenu;
            historyTabPage.Text = MyStrings.HistoricalData;
            histSensorChoiceLabel.Text = MyStrings.Appliance;
            languageToolStripMenuItem.Text = MyStrings.Language;
            logToFileToolStripMenuItem.Text = MyStrings.Logfile;
            toolTip1.SetToolTip(energeniaalLogoPictureBox, MyStrings.VisitEnergeniaalWebsite);
            monthlyHistoryToolStripMenuItem.Text = MyStrings.MonthHistoryMenuItem;
            monthRadioButton.Text = MyStrings.MonthHistoryRadioButton;
            noDataLabel.Text = MyStrings.NoDataForApplianceAndPhase;
            noDataLabel2.Text = MyStrings.NoDataForAppliance;
            noneToolStripMenuItem.Text = MyStrings.None;
            realTimeMinutesLabel.Text = MyStrings.Time;
            realTimeSensorChoiceLabel.Text = MyStrings.Appliance;
            releaseNotesToolStripMenuItem.Text = MyStrings.ReleaseNotes;
            settingsToolStripMenuItem.Text = MyStrings.SettingsMenu;
            timeOfDayRadioButton.Text = MyStrings.PerTimeOfDayRadioButton;
            twohourHistoryToolStripMenuItem.Text = MyStrings.TwoHourHistoryMenuItem;
            twoHourRadioButton.Text = MyStrings.TwoHourHistoryRadioButton;
            yearHistoryToolStripMenuItem.Text = MyStrings.YearHistoryMenuItem;
            yearRadioButton.Text = MyStrings.YearHistoryRadioButton;
            wipeDatabaseToolStripMenuItem.Text = MyStrings.DeleteAllHistoryData;
            TwoHourHistoryImportToolStripMenuItem.Text = MyStrings.TwoHourHistoryMenuItem;
            DayHistoryImportToolStripMenuItem.Text = MyStrings.DailyHistory;
            MonthHistoryImportToolStripMenuItem.Text = MyStrings.MonthHistoryMenuItem;

            // if not connected, initialize time, temp and watts labels with 
            // culture-specific zero values
            if (portRead.CurrentState != PortRead.State.Connected)
            {
                timeLabel.Text = DateTime.MinValue.ToShortTimeString();
                tempLabel.Text = "0";
                wattsLabel.Text = "0";
            }
        }

        private void InitializeLanguageRadioButtonSelection()
        {
            // keep track of which item, if any, is checked
            ToolStripRadioButtonMenuItem checkedItem = null;
            // keep track of which item is the one for English
            ToolStripRadioButtonMenuItem englishItem = null;

            // for each object because it could be toolstrip separator as well
            foreach (ToolStripItem item in this.languageToolStripMenuItem.DropDownItems)
            {
                ToolStripRadioButtonMenuItem radioItem =
                    item as ToolStripRadioButtonMenuItem;
                if ((string)radioItem.Tag == Energeniaal.Properties.Settings.Default.Language)
                {
                    radioItem.Checked = true;
                    checkedItem = radioItem;
                }
                else if (Energeniaal.Properties.Settings.Default.Language == "??"
                    && (string)radioItem.Tag == System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                {
                    radioItem.Checked = true;
                    checkedItem = radioItem;
                }
                else
                {
                    radioItem.Checked = false;
                }

                if ((string)radioItem.Tag == Energeniaal.Properties.Settings.Default.DefaultCulture)
                {
                    englishItem = radioItem;
                }
            }

            // If still no items checked, then make sure English is checked
            if (checkedItem == null)
            {
                englishItem.Checked = true;
            }
        }

        private void CreateLanguageRadioButtonItems()
        {
            // First add default language manually because there's no satellite assembly
            ToolStripRadioButtonMenuItem subItem
                = (NewToolStripRadioButtonMenuItem(CultureInfo.GetCultureInfo(Energeniaal.Properties.Settings.Default.DefaultCulture)));
            languageToolStripMenuItem.DropDownItems.Add(subItem);

            string executablePath = Path.GetDirectoryName(Application.ExecutablePath);
            // Get list of all subdirectories with 2-character names 
            // to find available satellite assemblies
            string[] directories = Directory.GetDirectories(executablePath, "??");
            foreach (string s in directories)
            {
                DirectoryInfo langDirectory = new DirectoryInfo(s);
                CultureInfo culture;

                // First check to see if directory name represents a valid culture
                try
                {
                    culture = CultureInfo.GetCultureInfo(langDirectory.Name);
                }
                catch (CultureNotFoundException)
                {
                    // Guess not. Just ignore it and move to the next one
                    continue;
                }

                subItem = NewToolStripRadioButtonMenuItem(culture);
                languageToolStripMenuItem.DropDownItems.Add(subItem);
            }

            InitializeLanguageRadioButtonSelection();
        }

        // Create new ToolStripRadioButtonMenuItem with default settings
        private ToolStripRadioButtonMenuItem NewToolStripRadioButtonMenuItem(CultureInfo culture)
        {
            string languageName = culture.NativeName;

            // Make sure native language name is capitalized, for uniformity
            string capLanguageName = string.Empty;
            if (languageName.Length > 0)
            {
                capLanguageName += char.ToUpper(languageName[0]);
            }

            if (languageName.Length > 1)
            {
                capLanguageName += languageName.Substring(1);
            }

            ToolStripRadioButtonMenuItem subItem
                = (new ToolStripRadioButtonMenuItem(capLanguageName));
            subItem.Tag = culture.TwoLetterISOLanguageName;
            subItem.Click += new System.EventHandler(this.LanguageRadioButton_Checked);
            return subItem;
        }

        private void LanguageRadioButton_Checked(object sender, EventArgs e)
        {
            ToolStripRadioButtonMenuItem radio = (ToolStripRadioButtonMenuItem)sender;
            if (radio.Checked)
            {
                this.languageToolStripMenuItem.Tag = radio.Tag;
                Energeniaal.Properties.Settings.Default.Language = (string)this.languageToolStripMenuItem.Tag;
                Energeniaal.Program.ReLocalizeAll();
            }
        }

        private void InitializeComboBoxes()
        {
            minutesComboBox.DataSource = minutesComboBoxItems;

            FillComboBoxes();
            if (realTimeSensorComboBox.Items.Count > 0)
            {
                realTimeSensorComboBox.SelectedIndex = 0;
            }

            if (channelComboBox.Items.Count > 0)
            {
                channelComboBox.SelectedIndex = 0;
            }

            if (histSensorComboBox.Items.Count > 0)
            {
                histSensorComboBox.SelectedIndex = 0;
            }

            minutesComboBox.SelectedValue = Energeniaal.Properties.Settings.Default.ChartMinutes;
        }

        private void FillComboBoxes()
        {
            FillSensorComboBoxes();
            FillChannelComboBox();
            FillMinutesComboBox();
        }

        private void FillSensorComboBoxes()
        {
            // Fill the two sensor combo boxes with the same contents
            int realTimeSensorSelected = realTimeSensorComboBox.SelectedIndex;
            int histSensorSelected = histSensorComboBox.SelectedIndex;

            realTimeSensorComboBox.Items.Clear();
            histSensorComboBox.Items.Clear();

            foreach (KeyValuePair<int, string> pair in SensorNameHandler.SensorNames.OrderBy(i => i.Key))
            {
                realTimeSensorComboBox.Items.Add(pair.Value);
                histSensorComboBox.Items.Add(pair.Value);
            }

            realTimeSensorComboBox.SelectedIndex = realTimeSensorSelected;
            histSensorComboBox.SelectedIndex = histSensorSelected;
        }

        private void FillChannelComboBox()
        {
            int channelSelected = channelComboBox.SelectedIndex;
            channelComboBox.Items.Clear();

            channelComboBox.Items.Add(MyStrings.Total);
            for (int i = 1; i <= portRead.MaxChannels; i++)
            {
                channelComboBox.Items.Add(MyStrings.Channel + " " + i);
            }

            channelComboBox.SelectedIndex = channelSelected;
        }

        private void FillMinutesComboBox()
        {
            int minutesSelected = minutesComboBox.SelectedIndex;
            minutesComboBoxItems.Clear();

            for (int minutes = 15; minutes <= Energeniaal.Properties.Settings.Default.MaxChartMinutes; minutes *= 2)
            {
                minutesComboBoxItems.Add(new KeyValuePair<int, string>(minutes, this.ConvertMinutesToString(minutes)));
            }

            minutesComboBox.SelectedIndex = minutesSelected;
        }

        private string ConvertMinutesToString(int minutes)
        {
            string timeString = string.Empty;

            if (minutes < 60)
            {
                timeString = string.Format("{0} {1}", minutes, MyStrings.Minutes);
            }
            else if (minutes == 60)
            {
                timeString = string.Format("{0} {1}", minutes / 60, MyStrings.Hour);
            }
            else
            {
                timeString = string.Format("{0:0.#} {1}", (double)minutes / 60, MyStrings.Hours);
            }

            return timeString;
        }

        // decide which radio button to check on startup,
        // based on available data
        private void InitializeHistoryRadioButtons()
        {
            List<HistoryDataPoint> data = new List<HistoryDataPoint>();
            // if month data available, show that by default
            data = portRead.GetMonthHistory();
            if (data.Count > 0)
            {
                monthRadioButton.Checked = true;
                return;
            }
            // otherwise if day data available, show that by default
            data = portRead.GetDayHistory();
            if (data.Count > 0)
            {
                dayRadioButton.Checked = true;
                return;
            }
            // otherwise show 2-hour data
            twoHourRadioButton.Checked = true;
        }

        private void HandleDeviceChangeEvent(object sender, EventArrivedEventArgs e)
        {
            try
            {
                this.BeginInvoke(new InvokeDelegate(FillConnectDropDown));
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        public delegate void InvokeDelegate();

        private void FillConnectDropDown()
        {
            connectToolStripMenuItem.DropDownItems.Clear();

            string[] portNames = GetComPortNames();
            ToolStripItem[] comPortMenuItems = new ToolStripItem[portNames.Length + 2];

            int i = 0;
            foreach (string portname in portNames)
            {
                comPortMenuItems[i] = new ToolStripMenuItem();
                comPortMenuItems[i].Name = portname + "ToolStripMenuItem";
                comPortMenuItems[i].Size = new System.Drawing.Size(152, 22);
                comPortMenuItems[i].Text = portname;
                comPortMenuItems[i].Click += new System.EventHandler(COMToolStripMenuItem_Click);
                // Don't allow connect to com port while already connected
                if (!portReadingBackgroundWorker.IsBusy)
                {
                    comPortMenuItems[i].Enabled = true;
                }
                else
                {
                    comPortMenuItems[i].Enabled = false;
                }
                i++;
            }

            comPortMenuItems[i++] = toolStripMenuItem1;
            comPortMenuItems[i] = disconnectToolStripMenuItem;
            // Don't allow disconnect if not connected
            if (portReadingBackgroundWorker.IsBusy)
            {
                comPortMenuItems[i].Enabled = true;
            }
            else
            {
                comPortMenuItems[i].Enabled = false;
            }

            connectToolStripMenuItem.DropDownItems.AddRange(comPortMenuItems);
        }

        private string[] GetComPortNames()
        {
            if (!usingWMI)
            {
                // something went horribly wrong with WMI, so we're returning a default list of COM ports
                return GetDefaultComPortNames();
            }

            try
            {
                System.Text.RegularExpressions.Regex friendlyNameToComPort = new System.Text.RegularExpressions.Regex(@".? \((COM\d+)\)$");  // "..... (COMxxx)" -> COMxxxx

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    @"root\CIMV2",
                    "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
                ManagementObjectCollection managementObjectCollection = searcher.Get();
                List<String> names = new List<String>(managementObjectCollection.Count);
                foreach (ManagementObject queryObj in managementObjectCollection)
                {
                    if (queryObj["Caption"] != null)
                    {
                        string text = queryObj["Caption"].ToString(); // Prolific USB-to-Serial Comm Port (COM3)
                        if (text.Contains("(COM") && friendlyNameToComPort.IsMatch(text))
                        {
                            string port = friendlyNameToComPort.Match(text).Groups[1].Value;
                            names.Add(port);
                        }
                    }
                }
                return names.ToArray();
            }
            catch
            {
                MessageBox.Show(MyStrings.WMIError);
                return GetDefaultComPortNames();
            }
        }

        private string[] GetDefaultComPortNames()
        {
            int maxPort = 10;

            List<String> names = new List<String>(maxPort);
            for (int i = 1; i <= maxPort; i++)
            {
                names.Add("COM" + i);
            }

            return names.ToArray();
        }

        private void COMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StartReadingFromDevice(sender.ToString());
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void SetStatusText()
        {
            switch (portRead.CurrentState)
            {
                case PortRead.State.Connected:
                    toolStripStatusLabel1.Text = MyStrings.Connected;
                    break;
                case PortRead.State.Connecting:
                    toolStripStatusLabel1.Text = MyStrings.Connecting;
                    break;
                case PortRead.State.Disconnected:
                    toolStripStatusLabel1.Text = MyStrings.Disconnected;
                    break;
                case PortRead.State.Disconnecting:
                    toolStripStatusLabel1.Text = MyStrings.Disconnecting;
                    break;
            }
        }

        private void StartReadingFromDevice(string port)
        {
            // don't use setStatusText here because actual status of portRead hasn't changed yet
            toolStripStatusLabel1.Text = MyStrings.Connecting + " " + port;
            foreach (ToolStripItem item in connectToolStripMenuItem.DropDownItems)
            {
                item.Enabled = false;
            }

            logToFileToolStripMenuItem.Enabled = false;
            foreach (ToolStripItem item in logToFileToolStripMenuItem.DropDownItems)
            {
                item.Enabled = false;
            }

            // call connectionBackgroundWorker_DoWork to try connecting to 
            // and reading from select COM port
            connectionBackgroundWorker.RunWorkerAsync(port);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                disconnectToolStripMenuItem.Enabled = false;
                StopReadingFromDevice();
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void StopReadingFromDevice()
        {
            screenUpdateTimer.Stop();
            portRead.StopReadingFromSerialPort();
            SetStatusText();
        }

        private void screenUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // reading hasn't gotten started yet, 
                // or user has selected another language and changes are being applied
                if (!DoScreenUpdates())
                    return;

                SetStatusText();

                while (portRead.MessageQueueCount > 0)
                {
                    WattsMessageData data = portRead.GetWattsMessageDataFromQueue();
                    UpdateScreenWithMessageData(data);
                }

            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void portReadingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string[] args = e.Argument as string[];

            string portName = args[0];
            string fileName = args[1];

            portRead.ReadFromSerialPort(portName, fileName);
        } // end portReadingBackgroundWorker_DoWork

        private void portReadingBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an unhandled exception was thrown.
            if (e.Error != null)
            {
                HandleErrorFromReading(e.Error.Message);
            }
            else
            {
                if (logFileNameToolStripMenuItem.Checked && File.Exists(logFileNameToolStripMenuItem.Text))
                {
                    System.Diagnostics.Process.Start(logFileNameToolStripMenuItem.Text);
                }
            }

            UpdateControlsAfterDisconnect();
        } // end portReadingBackgroundWorker_RunWorkerCompleted

        private void HandleErrorFromReading(string errorMessage)
        {
            StopReadingFromDevice();
            MessageBox.Show(errorMessage);
        }

        private void UpdateControlsAfterDisconnect()
        {
            disconnectToolStripMenuItem.Enabled = false;
            SetStatusText();
            foreach (ToolStripItem item in connectToolStripMenuItem.DropDownItems)
            {
                if (item != disconnectToolStripMenuItem)
                    item.Enabled = true;
            }
            logToFileToolStripMenuItem.Enabled = true;
            foreach (ToolStripItem item in logToFileToolStripMenuItem.DropDownItems)
            {
                item.Enabled = true;
            }
        }

        private void UpdateWattsChartData(WattsMessageData messageData)
        {
            DateTime removeBefore = DateTime.MinValue;

            try
            {
                int maxChartMinutes = Energeniaal.Properties.Settings.Default.MaxChartMinutes;
                removeBefore = messageData.TimeStamp.AddSeconds(-maxChartMinutes * 60);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // occasionally this happens, haven't yet figured out why
                // just try again at the next tick, unless debugging
                if (Program.TestMode)
                {
                    MessageBox.Show(string.Format("{0}: LastTime = {1}-{2}-{3} {4}:{5}:{6}", ex.Message,
                        messageData.TimeStamp.Year, messageData.TimeStamp.Month, messageData.TimeStamp.Day,
                        messageData.TimeStamp.Hour, messageData.TimeStamp.Minute, messageData.TimeStamp.Second));
                }
                return;
            }
            WattsDataPoint dataPoint = new WattsDataPoint(messageData.TimeStamp, messageData.Watts);
            wattsData[messageData.Sensor, messageData.Channel].Add(dataPoint);
            // remove all points from the data sources older than chartMinutes minutes
            // this isn't as bad as it looks. most sensor/channel combos are empty
            for (int s = 0; s < portRead.MaxSensors; s++)
            {
                for (int c = 0; c < portRead.MaxChannels + 1; c++)
                {
                    while (wattsData[s, c].Count > 0 && wattsData[s, c][0].TimeStamp < removeBefore)
                    {
                        wattsData[s, c].RemoveAt(0);
                    }
                }
            }

            // Add watts for this channel to channels total
            // If there isn't yet a total datapoint for sensor/timestamp, then add one
            int totalIndex = FindChannelTotalIndexInWattsData(messageData.Sensor, messageData.TimeStamp);
            if (totalIndex >= 0)
            {
                wattsData[messageData.Sensor, 0][totalIndex].Watts += messageData.Watts;
            }
            else
            {
                WattsDataPoint totalDataPoint = new WattsDataPoint(messageData.TimeStamp, messageData.Watts);
                wattsData[messageData.Sensor, 0].Add(totalDataPoint);
            }

        }

        private int FindChannelTotalIndexInWattsData(int sensor, DateTime timeStamp)
        {
            for (int i = wattsData[sensor, 0].Count - 1; i >= 0; i--)
            {
                if (wattsData[sensor, 0][i].TimeStamp < timeStamp)
                {
                    return -1; // already past the spot where it should be
                }

                if (wattsData[sensor, 0][i].TimeStamp == timeStamp)
                {
                    return i; // here it is
                }
            }

            return -1;
        }

        private void UpdateWattsChartSeries()
        {
            // Update real-time watts chart by filling series with data from selected channel
            // There must be a better way to keep track of multiple series with 
            // the same properties while only showing one at a time...
            Series series = wattsChart.Series["Watts"];
            int selectedSensor = realTimeSensorComboBox.SelectedIndex;
            int selectedChannel = channelComboBox.SelectedIndex;
            List<WattsDataPoint> wattsDataPoints = wattsData[selectedSensor, selectedChannel];
            int chartMinutes = Energeniaal.Properties.Settings.Default.ChartMinutes;

            int borderWidth = Math.Max((6 - chartMinutes / 30), 3);
            series.BorderWidth = borderWidth;
            series.Points.Clear();

            List<WattsDataPoint> pointsToAdd = GetPointsToAddToWattsChart(wattsDataPoints, chartMinutes);
            foreach (WattsDataPoint point in pointsToAdd)
            {
                series.Points.AddXY(point.TimeStamp, point.Watts);
            }

            // axis labels for chartMinutes minutes worth of data
            if (series.Points.Count > 0)
            {
                wattsChart.ChartAreas[0].AxisX.Minimum = series.Points[0].XValue;
                wattsChart.ChartAreas[0].AxisX.Maximum
                    = DateTime.FromOADate(series.Points[0].XValue).AddMinutes(chartMinutes).ToOADate();
            }

            // use annotation if no data
            if (series.Points.Count < 1)
            {
                noDataLabel.Visible = true;
            }
            else
            {
                noDataLabel.Visible = false;
            }
        }

        // returns list of last chartMinutes worth of data points
        private List<WattsDataPoint> GetPointsToAddToWattsChart(List<WattsDataPoint> wattsDataPoints, int chartMinutes)
        {
            List<WattsDataPoint> pointsToAdd = new List<WattsDataPoint>();

            DateTime addAfter = DateTime.MaxValue;
            if (wattsDataPoints.Count > 0)
            {
                WattsDataPoint point = wattsDataPoints[wattsDataPoints.Count - 1];
                addAfter = point.TimeStamp.AddMinutes(-chartMinutes);
            }

            // add last chartMinutes worth of data points to list
            int i = wattsDataPoints.Count - 1;
            while (i >= 0 && wattsDataPoints[i].TimeStamp >= addAfter)
            {
                pointsToAdd.Add(wattsDataPoints[i]);
                i--;
            }

            // points were added to list in reverse order, but they need to
            // be properly sorted in the series
            pointsToAdd.Reverse();

            return pointsToAdd;
        }

        private void FillHistoryChart(HistoryType historyType)
        {
            try
            {
                Series series = historyChart.Series["kWh"];
                series.Points.Clear();
                ChartArea area = historyChart.ChartAreas["Default"];
                int sensor = histSensorComboBox.SelectedIndex;
                List<HistoryDataPoint> histData = new List<HistoryDataPoint>();

                switch (historyType)
                {
                    case HistoryType.TwoHour:
                        area.AxisX.LabelStyle.Format = "g";
                        area.AxisX.IntervalType = DateTimeIntervalType.Auto;
                        area.AxisX.MinorTickMark.Enabled = true;
                        area.AxisX.LabelStyle.IntervalOffset = 1;
                        series.XValueType = ChartValueType.DateTime;
                        histData = portRead.GetTwoHourHistory(sensor);
                        area.AxisX.LabelStyle.Interval = GetAxisXInterval(histData.Count);
                        series.ToolTip = "#VALX{dddd}\n#VALX{" + area.AxisX.LabelStyle.Format + "}\n#VALY " + series.Name;
                        break;
                    case HistoryType.Day:
                        area.AxisX.LabelStyle.Format = "d";
                        area.AxisX.IntervalType = DateTimeIntervalType.Days;
                        area.AxisX.MinorTickMark.Enabled = true;
                        area.AxisX.LabelStyle.IntervalOffset = 0;
                        series.XValueType = ChartValueType.DateTime;
                        histData = portRead.GetDayHistory(sensor);
                        area.AxisX.LabelStyle.Interval = GetAxisXInterval(histData.Count);
                        series.ToolTip = "#VALX{dddd}\n#VALX{" + area.AxisX.LabelStyle.Format + "}\n#VALY " + series.Name;
                        break;
                    case HistoryType.Month:
                        area.AxisX.LabelStyle.Format = "Y";
                        area.AxisX.IntervalType = DateTimeIntervalType.Months;
                        area.AxisX.MinorTickMark.Enabled = false;
                        area.AxisX.LabelStyle.IntervalOffset = 0;
                        series.XValueType = ChartValueType.Date;
                        histData = portRead.GetMonthHistory(sensor);
                        area.AxisX.LabelStyle.Interval = GetAxisXInterval(histData.Count);
                        //series.ToolTip = "#VALX{d}\n#VALY " + series.Name;
                        // tooltip for month data will be set dynamically for each point
                        // to include end date of period
                        break;
                    case HistoryType.Year:
                        area.AxisX.LabelStyle.Format = "yyyy";
                        area.AxisX.IntervalType = DateTimeIntervalType.Years;
                        area.AxisX.MinorTickMark.Enabled = false;
                        area.AxisX.LabelStyle.IntervalOffset = 0;
                        series.XValueType = ChartValueType.Date;
                        histData = this.CalculateYearHistory(sensor);
                        area.AxisX.LabelStyle.Interval = GetAxisXInterval(histData.Count);
                        series.ToolTip = "#VALX{yyyy}\n#VALY " + series.Name;
                        break;
                }

                area.AxisX.ScaleView.ZoomReset();
                foreach (HistoryDataPoint point in histData)
                {
                    if (point.Kwh != 0)
                    {
                        series.Points.AddXY(point.TimeStamp, point.Kwh);
                    }
                }

                // use annotation if no data
                if (series.Points.Count < 1)
                {
                    noDataLabel2.Visible = true;
                }
                else
                {
                    noDataLabel2.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        // set label interval according to number of data points
        // avoid unwanted extra labels when using auto interval with a very small data set
        private int GetAxisXInterval(int pointsCount)
        {
            if (pointsCount < 5)
                return 1;
            else
                return 0; // auto interval
        }

        private List<HistoryDataPoint> CalculateYearHistory()
        {
            List<HistoryDataPoint> yearHistory = new List<HistoryDataPoint>();

            for (int sensor = 0; sensor < portRead.MaxSensors; sensor++)
            {
                yearHistory.AddRange(CalculateYearHistory(sensor));
            }

            return yearHistory;
        }

        private List<HistoryDataPoint> CalculateYearHistory(int sensor)
        {
            List<HistoryDataPoint> yearHistory = new List<HistoryDataPoint>();
            List<HistoryDataPoint> monthHistory = portRead.GetMonthHistory(sensor);

            int prevYear = 0;
            HistoryDataPoint currentYearDataPoint = new HistoryDataPoint();
            HistoryDataPoint prevMonthDataPoint = null;

            DateTime jan1 = new DateTime(DateTime.MinValue.Year, 1, 1);
            DateTime dec3 = new DateTime(DateTime.MinValue.Year, 12, 3);

            foreach (HistoryDataPoint point in monthHistory)
            {
                if (jan1.Year == DateTime.MinValue.Year)
                {
                    jan1 = jan1.AddYears(point.TimeStamp.Year - DateTime.MinValue.Year - 1);
                    dec3 = dec3.AddYears(point.TimeStamp.Year - DateTime.MinValue.Year - 1);
                }

                // Have we gone back to a previous year? Something wrong with the database
                if (point.TimeStamp.Year < prevYear)
                {
                    MessageBox.Show(MyStrings.DatabaseIncorrect);
                    return yearHistory;
                }

                if (point.TimeStamp.Year != prevYear)
                {
                    jan1 = jan1.AddYears(1);
                    dec3 = dec3.AddYears(1);
                    // This is the first bit of month data for this year,
                    // so add it to the year history list
                    yearHistory.Add(new HistoryDataPoint(jan1, sensor, point.Kwh));
                    currentYearDataPoint = yearHistory[yearHistory.Count - 1];
                    prevYear = point.TimeStamp.Year;

                    // Now we need to do something with the last month data 
                    // for the previous year (if present) to extrapolate  
                    // data for the beginning of January
                    if (prevMonthDataPoint != null
                        && point.TimeStamp != jan1
                        && point.TimeStamp.Month == 1) // just to make sure it IS January)
                    {
                        double extraJanKwh = prevMonthDataPoint.Kwh / 30
                            * (point.TimeStamp.Day - 1);
                        currentYearDataPoint.Kwh += extraJanKwh;
                    }

                }
                else if (point.TimeStamp < dec3)
                {
                    // This is data for a month in the same year, with 
                    // start date before December 3. 
                    // Just add Kwh to total for year
                    currentYearDataPoint.Kwh += point.Kwh;
                }
                else
                {
                    // Start date >= December 3, so extrapolate data
                    // for remaining days in December
                    double extraDecKwh = point.Kwh / 30
                            * (32 - point.TimeStamp.Day);
                    currentYearDataPoint.Kwh += extraDecKwh;
                }

                prevMonthDataPoint = point;
            }

            // If the last month data started at the end of December, 
            // extrapolate so that chart shows something for January 
            // of next year
            if (prevMonthDataPoint != null)
            {
                if (prevMonthDataPoint.TimeStamp >= dec3) // just to make sure it IS January)
                {
                    jan1 = jan1.AddYears(1);
                    // Check to see if there's any available day data for the part
                    // of the last month data period that falls in January,
                    // to use instead of extrapolated data
                    DateTime endDate = prevMonthDataPoint.TimeStamp.AddDays(29);
                    List<HistoryDataPoint> janDayHistory
                        = portRead.GetDayHistoryRange(sensor, jan1, endDate);
                    // Calculate sum of kwh in January day data using LINQ syntax
                    double janSum = janDayHistory.Sum(d => d.Kwh);
                    // Calculate average per day, to use for days without exact data
                    double avgKwhPerDay = prevMonthDataPoint.Kwh / 30;
                    // Number of days that we still need to extrapolate for January
                    int daysToExtrapolate = 30 - (32 - prevMonthDataPoint.TimeStamp.Day)
                        - janDayHistory.Count;
                    // Now calculate (approximate) usage for the part of the last 
                    // month data period that falls in January
                    double extraJanKwh = janSum + avgKwhPerDay * daysToExtrapolate;
                    yearHistory.Add(new HistoryDataPoint(jan1, sensor, extraJanKwh));
                    currentYearDataPoint = yearHistory[yearHistory.Count - 1];
                }

                // Check to see if there's any available day data after the last month period
                DateTime startDate = prevMonthDataPoint.TimeStamp.AddDays(30);
                List<HistoryDataPoint> lastDayHistory
                    = portRead.GetDayHistoryRange(sensor, startDate, DateTime.MaxValue);
                // Get sum of kwh in day data using LINQ syntax
                double sum = lastDayHistory.Sum(d => d.Kwh);
                currentYearDataPoint.Kwh += sum;
            }

            return yearHistory;
        }

        private void historyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHistoryChart();
        }

        private void UpdateHistoryChart()
        {
            HistoryType historyType = GetSelectedHistoryType();
            if (historyType != HistoryType.Undefined)
            {
                FillHistoryChart(historyType);
            }
        }

        // Returns HistoryType associated with selected history radio button
        private HistoryType GetSelectedHistoryType()
        {
            HistoryType historyType = HistoryType.Undefined;

            var checkedButton = historyTabPage.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            if (checkedButton != null)
            {
                object tag = ((RadioButton)checkedButton).Tag;
                historyType = (HistoryType)System.Enum.Parse(typeof(HistoryType), tag.ToString());
            }

            return historyType;
        }

        private void twohourHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(HistoryType.TwoHour);
        }

        private void dailyHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(HistoryType.Day);
        }

        private void monthlyHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(HistoryType.Month);
        }

        private void yearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(HistoryType.Year);
        }

        private void Export(HistoryType histType)
        {
            exportSaveFileDialog.Filter = ExportSaveFilter;
            switch (histType)
            {
                case HistoryType.TwoHour:
                    exportSaveFileDialog.FileName = MyStrings.TwoHourHistoryFilename;
                    break;
                case HistoryType.Day:
                    exportSaveFileDialog.FileName = MyStrings.DayHistoryFilename;
                    break;
                case HistoryType.Month:
                    exportSaveFileDialog.FileName = MyStrings.MonthHistoryFilename;
                    break;
                case HistoryType.Year:
                    exportSaveFileDialog.FileName = MyStrings.YearHistoryFilename;
                    break;
            }

            if (exportSaveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (StreamWriter writer = new StreamWriter(exportSaveFileDialog.FileName, false)) // overwrite
            {
                List<HistoryDataPoint> data = new List<HistoryDataPoint>();
                switch (histType)
                {
                    case HistoryType.TwoHour:
                        data = portRead.GetTwoHourHistory();
                        break;
                    case HistoryType.Day:
                        data = portRead.GetDayHistory();
                        break;
                    case HistoryType.Month:
                        data = portRead.GetMonthHistory();
                        break;
                    case HistoryType.Year:
                        data = this.CalculateYearHistory();
                        break;
                }

                if (histType == HistoryType.Year)
                {
                    writer.WriteLine(MyStrings.ExportColumnHeadersYear);
                }
                else
                {
                    writer.WriteLine(MyStrings.ExportColumnHeaders);
                }

                foreach (HistoryDataPoint point in data)
                {
                    string timeStamp = string.Empty;
                    if (histType == HistoryType.TwoHour)
                    {
                        timeStamp = point.TimeStamp.ToString();
                    }
                    else if (histType == HistoryType.Year)
                    {
                        // day and time not revelant with year history
                        timeStamp = point.TimeStamp.Year.ToString();
                    }
                    else
                    {
                        // time not relevant with day and month history
                        timeStamp = point.TimeStamp.ToShortDateString();
                    }
                    // use custom sensor name in output
                    string sensorName = point.Sensor.ToString();
                    SensorNameHandler.SensorNames.TryGetValue(point.Sensor, out sensorName);
                    writer.WriteLine(timeStamp + ";" + sensorName + ";" + point.Kwh + ";" + point.Sensor);
                }
            }
        } // end export

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Energeniaal.Properties.Settings.Default.EnergeniaalUrl);
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void realTimeSensorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DoScreenUpdates())
                {
                    UpdateWattsLabel();
                    UpdateWattsChartSeries();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private bool DoScreenUpdates()
        {
            if (!busySwitchingLanguage &&
                (portRead.CurrentState == PortRead.State.Connected))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void channelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DoScreenUpdates())
                {
                    UpdateWattsLabel();
                    UpdateWattsChartSeries();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void chooseLogfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = Energeniaal.Properties.Settings.Default.FullLogFilePath;
                if (string.IsNullOrWhiteSpace(filename))
                {
                    filename = Energeniaal.Properties.Settings.Default.DefaultLogFileName;
                }
                logfileSaveFileDialog.FileName = filename;
                logfileSaveFileDialog.Filter = LogfileSaveFilter;

                if (logfileSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    logFileNameToolStripMenuItem.Text = logfileSaveFileDialog.FileName;
                    logFileNameToolStripMenuItem.Checked = true;
                    logFileNameToolStripMenuItem.Visible = true;
                    noneToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = !noneToolStripMenuItem.Checked;
            logFileNameToolStripMenuItem.Checked = !noneToolStripMenuItem.Checked;
        }

        private void logFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logFileNameToolStripMenuItem.Checked = !logFileNameToolStripMenuItem.Checked;
            noneToolStripMenuItem.Checked = !logFileNameToolStripMenuItem.Checked;
        }

        private void histSensorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateHistoryChart();
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If something went seriously wrong (like portRead wasn't properly initialized), 
            // then trying to shut down properly will only make things worse
            if (emergencyExit)
            {
                return;
            }

            StopReadingFromDevice();
            if (usingWMI)
            {
                watcher.Stop();
            }
            SaveSettings();
            // make sure serial port is properly closed before exiting
            while (portRead.CurrentState != PortRead.State.Disconnected)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        private void connectionBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string portName = e.Argument as string;

            portRead.ConnectToSerialPort(portName);

            e.Result = portName;
        }

        private void connectionBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an unhandled exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                UpdateControlsAfterDisconnect();
                return;
            }

            // call portReadingBackgroundWorker_DoWork
            string port = e.Result as string;
            string fileName = logFileNameToolStripMenuItem.Text;
            portReadingBackgroundWorker.RunWorkerAsync(new string[] { port, fileName });
            // call enableDisconnectBackgroundWorker_DoWork to enable Disconnect menu item 
            // once device is connected

            disconnectToolStripMenuItem.Enabled = true;
            tempUnitLabel.Visible = true;
            wattsUnitLabel.Visible = true;
            // now that serial port is connected, start screen update
            screenUpdateTimer.Interval = 1000;
            screenUpdateTimer.Start();
        }

        private void averagesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateAveragesChart();
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void UpdateAveragesChart()
        {
            if (dayOfWeekRadioButton.Checked)
            {
                FillAveragesChartPerDayOfWeek();
            }
            else
            {
                FillAveragesChartPerTimeOfDay();
            }
        }

        public void FillAveragesChartPerDayOfWeek()
        {
            double[] averages = CalculateAveragesPerDayOfWeek();
            ChartArea area = averagesChart.ChartAreas["Default"];
            Series series = averagesChart.Series["kWh"];

            area.AxisX.LabelStyle.Format = "dddd";
            area.AxisX.MinorTickMark.Enabled = false;
            area.AxisX.LabelStyle.IsEndLabelVisible = false;
            area.AxisX.LabelStyle.IntervalOffset = 0;
            series.ToolTip = "#VALX{" + area.AxisX.LabelStyle.Format + "}\n#VALY{###.###} " + series.Name;
            series.Points.Clear();
            DateTime dayOfWeek = new DateTime(1929, 10, 28); // start with Monday
            for (int i = 0; i < averages.Length; i++)
            {
                series.Points.AddXY(dayOfWeek.AddDays(i), averages[i]);
            }
        }

        private double[] CalculateAveragesPerDayOfWeek()
        {
            double[] totals = { 0, 0, 0, 0, 0, 0, 0 }; // monday - sunday
            int[] numberOfPoints = { 0, 0, 0, 0, 0, 0, 0 }; // monday - sunday
            double[] averages = { 0, 0, 0, 0, 0, 0, 0 }; // monday - sunday
            List<HistoryDataPoint> data = new List<HistoryDataPoint>();

            data = portRead.GetDayHistory(0); // for whole house sensor
            foreach (HistoryDataPoint point in data)
            {
                // integer value of DayOfWeek: Sunday (0) - Saturday (6)
                int i = (int)point.TimeStamp.DayOfWeek;
                // make it so that it starts with Monday (0)
                i = (i + 6) % 7;
                totals[i] += point.Kwh;
                numberOfPoints[i] += 1;
            }
            for (int i = 0; i < totals.Length; i++)
            {
                if (numberOfPoints[i] > 0)
                    averages[i] = totals[i] / numberOfPoints[i];
            }
            return averages;
        }

        public void FillAveragesChartPerTimeOfDay()
        {
            double[] averages = CalculateAveragesPerTimeOfDay();
            ChartArea area = averagesChart.ChartAreas["Default"];
            Series series = averagesChart.Series["kWh"];

            area.AxisX.LabelStyle.Format = "t";
            area.AxisX.MinorTickMark.Enabled = false;
            area.AxisX.LabelStyle.IsEndLabelVisible = false;
            area.AxisX.LabelStyle.IntervalOffset = 1;
            series.ToolTip = "#VALX{" + area.AxisX.LabelStyle.Format + "}\n#VALY{###.###} " + series.Name;

            series.Points.Clear();
            DateTime timeOfDay = new DateTime(1970, 1, 1, 1, 0, 0); // start with 1:00
            for (int i = 0; i < averages.Length; i++)
            {
                series.Points.AddXY(timeOfDay.AddHours(2 * i), averages[i]);
            }
        }

        private double[] CalculateAveragesPerTimeOfDay()
        {
            double[] averages = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] totals = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // every 2 hours, 1:00 - 23:00
            int[] numberOfPoints = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<HistoryDataPoint> data = new List<HistoryDataPoint>();

            data = portRead.GetTwoHourHistory(0); // for whole house sensor
            foreach (HistoryDataPoint point in data)
            {
                int i = point.TimeStamp.Hour / 2;

                totals[i] += point.Kwh;
                numberOfPoints[i] += 1;
            }
            for (int i = 0; i < totals.Length; i++)
            {
                if (numberOfPoints[i] > 0)
                    averages[i] = totals[i] / numberOfPoints[i];
            }
            return averages;
        }


        private void chartTabControl_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                if (chartTabControl.SelectedTab == historyTabPage)
                {
                    UpdateHistoryChart();
                }
                else if (chartTabControl.SelectedTab == averagesTabPage)
                {
                    UpdateAveragesChart();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void UpdateScreenWithMessageData(WattsMessageData data)
        {
            // always update time and temp with latest values, regardless of selected sensor/channel
            timeLabel.Text = data.TimeStamp.ToShortTimeString();
            tempLabel.Text = data.Temp.ToString();

            UpdateWattsChartData(data);
            // update watts label with latest value for selected sensor/channel
            UpdateWattsLabel();

            // update chart if message was for selected sensor/channel
            int sensor = realTimeSensorComboBox.SelectedIndex;
            int channel = channelComboBox.SelectedIndex;
            if ((data.Sensor == sensor && data.Channel == channel)
                || channel == 0) // channels total selected
                UpdateWattsChartSeries();
        }

        private void UpdateWattsLabel()
        {
            int sensor = realTimeSensorComboBox.SelectedIndex;
            int channel = channelComboBox.SelectedIndex;

            float watts = 0;

            // display latest watts value from selected sensor and channel, if data available
            if (wattsData[sensor, channel].Count > 0)
            {
                int lastIndex = wattsData[sensor, channel].Count - 1;
                WattsDataPoint last = wattsData[sensor, channel][lastIndex];
                watts = Convert.ToSingle(last.Watts);
            }

            if (watts > 999)
            {
                wattsLabel.Text = (watts / 1000).ToString();
                wattsUnitLabel.Text = "KWatt";
            }
            else
            {
                wattsLabel.Text = watts.ToString();
                wattsUnitLabel.Text = "Watt";
            }
        }

        private void SaveSettings()
        {
            // these 2 lines shouldn't necessary because properties are bound to settings, 
            // but the binding doesn't seem to work
            Energeniaal.Properties.Settings.Default.LogToFile = logFileNameToolStripMenuItem.Checked;
            Energeniaal.Properties.Settings.Default.FullLogFilePath = logFileNameToolStripMenuItem.Text;

            // save settings
            Energeniaal.Properties.Settings.Default.Save();
        }

        void IReLocalizable.ReLocalize()
        {
            try
            {
                // suspend any updates to real-time display because combobox will be emptied and refilled
                busySwitchingLanguage = true;
                SetText();
                FillComboBoxes();
                // axis labels don't change when the language is switched, so refill chart manually
                // history chart already got refilled when combo boxes were refilled
                UpdateAveragesChart();
                SetStatusText();
                busySwitchingLanguage = false; // restart updates to real-time display
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void minutesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (minutesComboBox.SelectedValue != null)
                {
                    Energeniaal.Properties.Settings.Default.ChartMinutes = Convert.ToInt32(minutesComboBox.SelectedValue);
                }

                if (DoScreenUpdates())
                {
                    UpdateWattsChartSeries();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AboutBox1 aboutBox = new AboutBox1();
                aboutBox.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private void applianceNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomSensorNamesForm customNamesForm = new CustomSensorNamesForm();
            DialogResult result = customNamesForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                FillSensorComboBoxes();
            }
        }

        private void historyChart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType != ChartElementType.DataPoint)
                return;

            HistoryType historyType = GetSelectedHistoryType();
            if (historyType == HistoryType.Undefined)
                return;

            Series series = historyChart.Series["kWh"];

            if (historyType == HistoryType.Month)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                string startDate = DateTime.FromOADate(dp.XValue).ToString("d");

                e.Text = String.Format(
                    "{0} -\n{1}\n{2:F} {3}",
                    startDate,
                    ValxPlus29Days(dp.XValue),
                    dp.YValues[0],
                    series.Name);
            }
        }

        private string ValxPlus29Days(Double valx)
        {
            DateTime dt = DateTime.FromOADate(valx);
            return dt.AddDays(29).ToString("d");
        }

        private void releaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void variableLogoPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (File.Exists(variableLogoPath))
            {
                var image = new System.Drawing.Bitmap(variableLogoPath);
                //Don't use pictureBox1.Image property because it will
                //draw the image 2 times.
                //Make sure the pictureBox1.Image property is null in Design Mode
                if (image != null)
                {
                    var g = e.Graphics;
                    // -- Optional -- //
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    // -- Optional -- //
                    g.DrawImage(image,
                        variableLogoPictureBox.Width - image.Width,  // to right
                        variableLogoPictureBox.Height - image.Height, // to bottom
                        image.Width,
                        image.Height);
                }
            }

        }

        // Hidden functionality for wiping database and importing to database from csv files
        private void wipeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(MyStrings.ConfirmDatabaseWipe,
                "", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    portRead.PurgeAllHistoryData();
                    UpdateHistoryChart();
                    UpdateAveragesChart();
                    MessageBox.Show(MyStrings.WipeConfirmation);
                }
                catch (Exception ex)
                {
                    ShowErrorMessageAndExit(ex);
                }
            }
        }

        private void TwoHourHistoryImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData(HistoryType.TwoHour);
        }

        private void DayHistoryImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData(HistoryType.Day);
        }

        private void MonthHistoryImportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportData(HistoryType.Month);
        }

        private void ImportData(HistoryType historyType)
        {
            if (!ImportConfirmation())
            {
                return;
            }

            try
            {
                OpenFileDialog importOpenFileDialog = new OpenFileDialog();
                importOpenFileDialog.Filter = ExportSaveFilter;

                if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bool success = portRead.ImportHistoryData(historyType, importOpenFileDialog.FileName);
                    if (!success)
                    {
                        MessageBox.Show(MyStrings.ImportFileUnexpectedFormat + " " + MyStrings.ExportColumnHeaders);
                    }

                    UpdateHistoryChart();
                    UpdateAveragesChart();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageAndExit(ex);
            }
        }

        private bool ImportConfirmation()
        {
            var confirmResult = MessageBox.Show(MyStrings.ConfirmImport,
                "", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class WattsDataPoint
    {
        public DateTime TimeStamp { get; set; }
        public double Watts { get; set; }

        public WattsDataPoint()
        {
        }

        public WattsDataPoint(DateTime timestamp, double watts)
        {
            TimeStamp = timestamp;
            Watts = watts;
        }
    }

}
