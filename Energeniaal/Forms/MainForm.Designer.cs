

namespace Energeniaal
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.portReadingBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.screenUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.logfileSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.wattsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twohourHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailyHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yearHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.chooseLogfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applianceNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wipeDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TwoHourHistoryImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DayHistoryImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MonthHistoryImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.exportSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.realTimeGroupBox = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.minutesComboBox = new System.Windows.Forms.ComboBox();
            this.realTimeMinutesLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.realTimeSensorComboBox = new System.Windows.Forms.ComboBox();
            this.channelChoiceLabel = new System.Windows.Forms.Label();
            this.channelComboBox = new System.Windows.Forms.ComboBox();
            this.realTimeSensorChoiceLabel = new System.Windows.Forms.Label();
            this.noDataLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.energeniaalLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.chartTabControl = new System.Windows.Forms.TabControl();
            this.historyTabPage = new System.Windows.Forms.TabPage();
            this.noDataLabel2 = new System.Windows.Forms.Label();
            this.yearRadioButton = new System.Windows.Forms.RadioButton();
            this.histSensorComboBox = new System.Windows.Forms.ComboBox();
            this.histSensorChoiceLabel = new System.Windows.Forms.Label();
            this.historyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.monthRadioButton = new System.Windows.Forms.RadioButton();
            this.dayRadioButton = new System.Windows.Forms.RadioButton();
            this.twoHourRadioButton = new System.Windows.Forms.RadioButton();
            this.averagesTabPage = new System.Windows.Forms.TabPage();
            this.timeOfDayRadioButton = new System.Windows.Forms.RadioButton();
            this.dayOfWeekRadioButton = new System.Windows.Forms.RadioButton();
            this.averagesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.updateFormAfterConnectBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.variableLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tempUnitLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.tempLabel = new System.Windows.Forms.Label();
            this.wattsUnitLabel = new System.Windows.Forms.Label();
            this.wattsLabel = new System.Windows.Forms.Label();
            this.connectionBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wattsChart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.realTimeGroupBox.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.energeniaalLogoPictureBox)).BeginInit();
            this.chartTabControl.SuspendLayout();
            this.historyTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.historyChart)).BeginInit();
            this.averagesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.averagesChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.variableLogoPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // portReadingBackgroundWorker
            // 
            this.portReadingBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.portReadingBackgroundWorker_DoWork);
            this.portReadingBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.portReadingBackgroundWorker_RunWorkerCompleted);
            // 
            // screenUpdateTimer
            // 
            this.screenUpdateTimer.Interval = 1000;
            this.screenUpdateTimer.Tick += new System.EventHandler(this.screenUpdateTimer_Tick);
            // 
            // logfileSaveFileDialog
            // 
            this.logfileSaveFileDialog.FileName = "devicelog.txt";
            this.logfileSaveFileDialog.OverwritePrompt = false;
            // 
            // wattsChart
            // 
            this.wattsChart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.wattsChart.BorderSkin.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.wattsChart.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            this.wattsChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            chartArea1.AxisX.LabelStyle.Format = "t";
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.Name = "Default";
            this.wattsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.wattsChart.Legends.Add(legend1);
            this.wattsChart.Location = new System.Drawing.Point(20, 32);
            this.wattsChart.Name = "wattsChart";
            series1.BorderColor = System.Drawing.Color.White;
            series1.BorderWidth = 5;
            series1.ChartArea = "Default";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(132)))), ((int)(((byte)(195)))));
            series1.Legend = "Legend1";
            series1.Name = "Watts";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            this.wattsChart.Series.Add(series1);
            this.wattsChart.Size = new System.Drawing.Size(835, 158);
            this.wattsChart.TabIndex = 11;
            this.wattsChart.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Current energy use in Watts";
            title1.Visible = false;
            this.wattsChart.Titles.Add(title1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.databaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(892, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.disconnectToolStripMenuItem});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.connectToolStripMenuItem.Text = "&Connect";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(130, 6);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "&Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.twohourHistoryToolStripMenuItem,
            this.dailyHistoryToolStripMenuItem,
            this.monthlyHistoryToolStripMenuItem,
            this.yearHistoryToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "&Export";
            // 
            // twohourHistoryToolStripMenuItem
            // 
            this.twohourHistoryToolStripMenuItem.Name = "twohourHistoryToolStripMenuItem";
            this.twohourHistoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.twohourHistoryToolStripMenuItem.Text = "Two-hour history";
            this.twohourHistoryToolStripMenuItem.Click += new System.EventHandler(this.twohourHistoryToolStripMenuItem_Click);
            // 
            // dailyHistoryToolStripMenuItem
            // 
            this.dailyHistoryToolStripMenuItem.Name = "dailyHistoryToolStripMenuItem";
            this.dailyHistoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.dailyHistoryToolStripMenuItem.Text = "Daily history";
            this.dailyHistoryToolStripMenuItem.Click += new System.EventHandler(this.dailyHistoryToolStripMenuItem_Click);
            // 
            // monthlyHistoryToolStripMenuItem
            // 
            this.monthlyHistoryToolStripMenuItem.Name = "monthlyHistoryToolStripMenuItem";
            this.monthlyHistoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.monthlyHistoryToolStripMenuItem.Text = "Month history";
            this.monthlyHistoryToolStripMenuItem.Click += new System.EventHandler(this.monthlyHistoryToolStripMenuItem_Click);
            // 
            // yearHistoryToolStripMenuItem
            // 
            this.yearHistoryToolStripMenuItem.Name = "yearHistoryToolStripMenuItem";
            this.yearHistoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.yearHistoryToolStripMenuItem.Text = "Year history";
            this.yearHistoryToolStripMenuItem.Click += new System.EventHandler(this.yearHistoryToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.logToFileToolStripMenuItem,
            this.applianceNamesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // logToFileToolStripMenuItem
            // 
            this.logToFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.logFileNameToolStripMenuItem,
            this.toolStripSeparator2,
            this.chooseLogfileToolStripMenuItem});
            this.logToFileToolStripMenuItem.Name = "logToFileToolStripMenuItem";
            this.logToFileToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.logToFileToolStripMenuItem.Text = "Logfile";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // logFileNameToolStripMenuItem
            // 
            this.logFileNameToolStripMenuItem.Name = "logFileNameToolStripMenuItem";
            this.logFileNameToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.logFileNameToolStripMenuItem.Visible = false;
            this.logFileNameToolStripMenuItem.Click += new System.EventHandler(this.logFileNameToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(111, 6);
            // 
            // chooseLogfileToolStripMenuItem
            // 
            this.chooseLogfileToolStripMenuItem.Name = "chooseLogfileToolStripMenuItem";
            this.chooseLogfileToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.chooseLogfileToolStripMenuItem.Text = "Choose";
            this.chooseLogfileToolStripMenuItem.Click += new System.EventHandler(this.chooseLogfileToolStripMenuItem_Click);
            // 
            // applianceNamesToolStripMenuItem
            // 
            this.applianceNamesToolStripMenuItem.Name = "applianceNamesToolStripMenuItem";
            this.applianceNamesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.applianceNamesToolStripMenuItem.Text = "Appliance names";
            this.applianceNamesToolStripMenuItem.Click += new System.EventHandler(this.applianceNamesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.releaseNotesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // releaseNotesToolStripMenuItem
            // 
            this.releaseNotesToolStripMenuItem.Name = "releaseNotesToolStripMenuItem";
            this.releaseNotesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.releaseNotesToolStripMenuItem.Text = "Release notes";
            this.releaseNotesToolStripMenuItem.Click += new System.EventHandler(this.releaseNotesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wipeDatabaseToolStripMenuItem,
            this.importToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // wipeDatabaseToolStripMenuItem
            // 
            this.wipeDatabaseToolStripMenuItem.Name = "wipeDatabaseToolStripMenuItem";
            this.wipeDatabaseToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.wipeDatabaseToolStripMenuItem.Text = "Delete all history data";
            this.wipeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.wipeDatabaseToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TwoHourHistoryImportToolStripMenuItem,
            this.DayHistoryImportToolStripMenuItem,
            this.MonthHistoryImportToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.importToolStripMenuItem.Text = "Import ";
            // 
            // TwoHourHistoryImportToolStripMenuItem
            // 
            this.TwoHourHistoryImportToolStripMenuItem.Name = "TwoHourHistoryImportToolStripMenuItem";
            this.TwoHourHistoryImportToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.TwoHourHistoryImportToolStripMenuItem.Text = "Two-hour history";
            this.TwoHourHistoryImportToolStripMenuItem.Click += new System.EventHandler(this.TwoHourHistoryImportToolStripMenuItem_Click);
            // 
            // DayHistoryImportToolStripMenuItem
            // 
            this.DayHistoryImportToolStripMenuItem.Name = "DayHistoryImportToolStripMenuItem";
            this.DayHistoryImportToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DayHistoryImportToolStripMenuItem.Text = "Day history";
            this.DayHistoryImportToolStripMenuItem.Click += new System.EventHandler(this.DayHistoryImportToolStripMenuItem_Click);
            // 
            // MonthHistoryImportToolStripMenuItem1
            // 
            this.MonthHistoryImportToolStripMenuItem.Name = "MonthHistoryImportToolStripMenuItem1";
            this.MonthHistoryImportToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.MonthHistoryImportToolStripMenuItem.Text = "Month history";
            this.MonthHistoryImportToolStripMenuItem.Click += new System.EventHandler(this.MonthHistoryImportToolStripMenuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 661);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(892, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(877, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // realTimeGroupBox
            // 
            this.realTimeGroupBox.Controls.Add(this.panel3);
            this.realTimeGroupBox.Controls.Add(this.panel2);
            this.realTimeGroupBox.Controls.Add(this.noDataLabel);
            this.realTimeGroupBox.Controls.Add(this.wattsChart);
            this.realTimeGroupBox.Location = new System.Drawing.Point(11, 114);
            this.realTimeGroupBox.Name = "realTimeGroupBox";
            this.realTimeGroupBox.Size = new System.Drawing.Size(868, 247);
            this.realTimeGroupBox.TabIndex = 19;
            this.realTimeGroupBox.TabStop = false;
            this.realTimeGroupBox.Text = "Real-time monitoring";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.minutesComboBox);
            this.panel3.Controls.Add(this.realTimeMinutesLabel);
            this.panel3.Location = new System.Drawing.Point(606, 200);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(239, 36);
            this.panel3.TabIndex = 22;
            // 
            // minutesComboBox
            // 
            this.minutesComboBox.DisplayMember = "Value";
            this.minutesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.minutesComboBox.FormattingEnabled = true;
            this.minutesComboBox.Location = new System.Drawing.Point(111, 7);
            this.minutesComboBox.Name = "minutesComboBox";
            this.minutesComboBox.Size = new System.Drawing.Size(125, 21);
            this.minutesComboBox.TabIndex = 20;
            this.minutesComboBox.ValueMember = "Key";
            this.minutesComboBox.SelectedIndexChanged += new System.EventHandler(this.minutesComboBox_SelectedIndexChanged);
            // 
            // realTimeMinutesLabel
            // 
            this.realTimeMinutesLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.realTimeMinutesLabel.Location = new System.Drawing.Point(10, 11);
            this.realTimeMinutesLabel.Name = "realTimeMinutesLabel";
            this.realTimeMinutesLabel.Size = new System.Drawing.Size(95, 13);
            this.realTimeMinutesLabel.TabIndex = 19;
            this.realTimeMinutesLabel.Text = "Time";
            this.realTimeMinutesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.realTimeSensorComboBox);
            this.panel2.Controls.Add(this.channelChoiceLabel);
            this.panel2.Controls.Add(this.channelComboBox);
            this.panel2.Controls.Add(this.realTimeSensorChoiceLabel);
            this.panel2.Location = new System.Drawing.Point(6, 200);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 36);
            this.panel2.TabIndex = 21;
            // 
            // realTimeSensorComboBox
            // 
            this.realTimeSensorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.realTimeSensorComboBox.FormattingEnabled = true;
            this.realTimeSensorComboBox.Items.AddRange(new object[] {
            "Whole house",
            "Sensor 1",
            "Sensor 2",
            "Sensor 3",
            "Sensor 4",
            "Sensor 5",
            "Sensor 6",
            "Sensor 7",
            "Sensor 8",
            "Sensor 9"});
            this.realTimeSensorComboBox.Location = new System.Drawing.Point(105, 7);
            this.realTimeSensorComboBox.Name = "realTimeSensorComboBox";
            this.realTimeSensorComboBox.Size = new System.Drawing.Size(125, 21);
            this.realTimeSensorComboBox.TabIndex = 17;
            this.realTimeSensorComboBox.SelectedIndexChanged += new System.EventHandler(this.realTimeSensorComboBox_SelectedIndexChanged);
            // 
            // channelChoiceLabel
            // 
            this.channelChoiceLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.channelChoiceLabel.Location = new System.Drawing.Point(237, 11);
            this.channelChoiceLabel.Name = "channelChoiceLabel";
            this.channelChoiceLabel.Size = new System.Drawing.Size(95, 13);
            this.channelChoiceLabel.TabIndex = 13;
            this.channelChoiceLabel.Text = "Phase";
            this.channelChoiceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // channelComboBox
            // 
            this.channelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.channelComboBox.FormattingEnabled = true;
            this.channelComboBox.Items.AddRange(new object[] {
            "Channel 1",
            "Channel 2",
            "Channel 3",
            "Channel 4",
            "Channel 5",
            "Channel 6",
            "Channel 7",
            "Channel 8",
            "Channel 9",
            "Channel 10"});
            this.channelComboBox.Location = new System.Drawing.Point(338, 7);
            this.channelComboBox.Name = "channelComboBox";
            this.channelComboBox.Size = new System.Drawing.Size(125, 21);
            this.channelComboBox.TabIndex = 15;
            this.channelComboBox.SelectedIndexChanged += new System.EventHandler(this.channelComboBox_SelectedIndexChanged);
            // 
            // realTimeSensorChoiceLabel
            // 
            this.realTimeSensorChoiceLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.realTimeSensorChoiceLabel.Location = new System.Drawing.Point(4, 11);
            this.realTimeSensorChoiceLabel.Name = "realTimeSensorChoiceLabel";
            this.realTimeSensorChoiceLabel.Size = new System.Drawing.Size(95, 13);
            this.realTimeSensorChoiceLabel.TabIndex = 16;
            this.realTimeSensorChoiceLabel.Text = "Appliance";
            this.realTimeSensorChoiceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // noDataLabel
            // 
            this.noDataLabel.AutoSize = true;
            this.noDataLabel.BackColor = System.Drawing.Color.White;
            this.noDataLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.noDataLabel.Location = new System.Drawing.Point(326, 105);
            this.noDataLabel.Name = "noDataLabel";
            this.noDataLabel.Size = new System.Drawing.Size(143, 13);
            this.noDataLabel.TabIndex = 18;
            this.noDataLabel.Text = "No data for appliance/phase";
            // 
            // energeniaalLogoPictureBox
            // 
            this.energeniaalLogoPictureBox.Image = global::Energeniaal.Properties.Resources.Energeniaal_logo_medium;
            this.energeniaalLogoPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.energeniaalLogoPictureBox.Location = new System.Drawing.Point(13, 1);
            this.energeniaalLogoPictureBox.Name = "energeniaalLogoPictureBox";
            this.energeniaalLogoPictureBox.Size = new System.Drawing.Size(159, 47);
            this.energeniaalLogoPictureBox.TabIndex = 10;
            this.energeniaalLogoPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.energeniaalLogoPictureBox, "Visit Energeniaal website");
            this.energeniaalLogoPictureBox.Visible = false;
            this.energeniaalLogoPictureBox.Click += new System.EventHandler(this.logoPictureBox_Click);
            // 
            // chartTabControl
            // 
            this.chartTabControl.Controls.Add(this.historyTabPage);
            this.chartTabControl.Controls.Add(this.averagesTabPage);
            this.chartTabControl.Location = new System.Drawing.Point(12, 372);
            this.chartTabControl.Name = "chartTabControl";
            this.chartTabControl.Padding = new System.Drawing.Point(7, 4);
            this.chartTabControl.SelectedIndex = 0;
            this.chartTabControl.Size = new System.Drawing.Size(870, 282);
            this.chartTabControl.TabIndex = 20;
            this.chartTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.chartTabControl_Selected);
            // 
            // historyTabPage
            // 
            this.historyTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.historyTabPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.historyTabPage.Controls.Add(this.noDataLabel2);
            this.historyTabPage.Controls.Add(this.yearRadioButton);
            this.historyTabPage.Controls.Add(this.histSensorComboBox);
            this.historyTabPage.Controls.Add(this.histSensorChoiceLabel);
            this.historyTabPage.Controls.Add(this.energeniaalLogoPictureBox);
            this.historyTabPage.Controls.Add(this.historyChart);
            this.historyTabPage.Controls.Add(this.monthRadioButton);
            this.historyTabPage.Controls.Add(this.dayRadioButton);
            this.historyTabPage.Controls.Add(this.twoHourRadioButton);
            this.historyTabPage.Location = new System.Drawing.Point(4, 24);
            this.historyTabPage.Name = "historyTabPage";
            this.historyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.historyTabPage.Size = new System.Drawing.Size(862, 254);
            this.historyTabPage.TabIndex = 0;
            this.historyTabPage.Text = "Historical data";
            // 
            // noDataLabel2
            // 
            this.noDataLabel2.AutoSize = true;
            this.noDataLabel2.BackColor = System.Drawing.Color.White;
            this.noDataLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.noDataLabel2.Location = new System.Drawing.Point(335, 116);
            this.noDataLabel2.Name = "noDataLabel2";
            this.noDataLabel2.Size = new System.Drawing.Size(109, 13);
            this.noDataLabel2.TabIndex = 23;
            this.noDataLabel2.Text = "No data for appliance";
            // 
            // yearRadioButton
            // 
            this.yearRadioButton.AutoSize = true;
            this.yearRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.yearRadioButton.Location = new System.Drawing.Point(534, 222);
            this.yearRadioButton.Name = "yearRadioButton";
            this.yearRadioButton.Size = new System.Drawing.Size(80, 17);
            this.yearRadioButton.TabIndex = 33;
            this.yearRadioButton.Tag = "Year";
            this.yearRadioButton.Text = "Year history";
            this.yearRadioButton.UseVisualStyleBackColor = true;
            // 
            // histSensorComboBox
            // 
            this.histSensorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.histSensorComboBox.FormattingEnabled = true;
            this.histSensorComboBox.Items.AddRange(new object[] {
            "Whole house",
            "Sensor 1",
            "Sensor 2",
            "Sensor 3",
            "Sensor 4",
            "Sensor 5",
            "Sensor 6",
            "Sensor 7",
            "Sensor 8",
            "Sensor 9"});
            this.histSensorComboBox.Location = new System.Drawing.Point(713, 16);
            this.histSensorComboBox.Name = "histSensorComboBox";
            this.histSensorComboBox.Size = new System.Drawing.Size(125, 21);
            this.histSensorComboBox.TabIndex = 32;
            this.histSensorComboBox.SelectedIndexChanged += new System.EventHandler(this.histSensorComboBox_SelectedIndexChanged);
            // 
            // histSensorChoiceLabel
            // 
            this.histSensorChoiceLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.histSensorChoiceLabel.Location = new System.Drawing.Point(612, 20);
            this.histSensorChoiceLabel.Name = "histSensorChoiceLabel";
            this.histSensorChoiceLabel.Size = new System.Drawing.Size(95, 13);
            this.histSensorChoiceLabel.TabIndex = 31;
            this.histSensorChoiceLabel.Text = "Appliance";
            this.histSensorChoiceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // historyChart
            // 
            this.historyChart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.historyChart.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            this.historyChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Raised;
            chartArea2.AxisX.MajorTickMark.Interval = 0D;
            chartArea2.AxisX.MajorTickMark.IntervalOffset = 0D;
            chartArea2.AxisX.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisX.MinorTickMark.Enabled = true;
            chartArea2.AxisX.ScrollBar.IsPositionedInside = false;
            chartArea2.BackColor = System.Drawing.Color.White;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "Default";
            this.historyChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.historyChart.Legends.Add(legend2);
            this.historyChart.Location = new System.Drawing.Point(17, 52);
            this.historyChart.Name = "historyChart";
            series2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.VerticalCenter;
            series2.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(162)))), ((int)(((byte)(225)))));
            series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(164)))));
            series2.BorderWidth = 2;
            series2.ChartArea = "Default";
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(164)))));
            series2.Legend = "Legend1";
            series2.Name = "kWh";
            this.historyChart.Series.Add(series2);
            this.historyChart.Size = new System.Drawing.Size(833, 158);
            this.historyChart.TabIndex = 30;
            this.historyChart.Text = "s";
            title2.Name = "Title1";
            title2.Text = "Energy use in kWh";
            title2.Visible = false;
            this.historyChart.Titles.Add(title2);
            this.historyChart.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.historyChart_GetToolTipText);
            // 
            // monthRadioButton
            // 
            this.monthRadioButton.AutoSize = true;
            this.monthRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.monthRadioButton.Location = new System.Drawing.Point(364, 222);
            this.monthRadioButton.Name = "monthRadioButton";
            this.monthRadioButton.Size = new System.Drawing.Size(88, 17);
            this.monthRadioButton.TabIndex = 29;
            this.monthRadioButton.Tag = "Month";
            this.monthRadioButton.Text = "Month history";
            this.monthRadioButton.UseVisualStyleBackColor = true;
            this.monthRadioButton.CheckedChanged += new System.EventHandler(this.historyRadioButton_CheckedChanged);
            // 
            // dayRadioButton
            // 
            this.dayRadioButton.AutoSize = true;
            this.dayRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dayRadioButton.Location = new System.Drawing.Point(194, 222);
            this.dayRadioButton.Name = "dayRadioButton";
            this.dayRadioButton.Size = new System.Drawing.Size(81, 17);
            this.dayRadioButton.TabIndex = 28;
            this.dayRadioButton.Tag = "Day";
            this.dayRadioButton.Text = "Daily history";
            this.dayRadioButton.UseVisualStyleBackColor = true;
            this.dayRadioButton.CheckedChanged += new System.EventHandler(this.historyRadioButton_CheckedChanged);
            // 
            // twoHourRadioButton
            // 
            this.twoHourRadioButton.AutoSize = true;
            this.twoHourRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.twoHourRadioButton.Location = new System.Drawing.Point(24, 222);
            this.twoHourRadioButton.Name = "twoHourRadioButton";
            this.twoHourRadioButton.Size = new System.Drawing.Size(103, 17);
            this.twoHourRadioButton.TabIndex = 27;
            this.twoHourRadioButton.Tag = "TwoHour";
            this.twoHourRadioButton.Text = "Two-hour history";
            this.twoHourRadioButton.UseVisualStyleBackColor = true;
            this.twoHourRadioButton.CheckedChanged += new System.EventHandler(this.historyRadioButton_CheckedChanged);
            // 
            // averagesTabPage
            // 
            this.averagesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.averagesTabPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.averagesTabPage.Controls.Add(this.timeOfDayRadioButton);
            this.averagesTabPage.Controls.Add(this.dayOfWeekRadioButton);
            this.averagesTabPage.Controls.Add(this.averagesChart);
            this.averagesTabPage.Location = new System.Drawing.Point(4, 24);
            this.averagesTabPage.Name = "averagesTabPage";
            this.averagesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.averagesTabPage.Size = new System.Drawing.Size(862, 254);
            this.averagesTabPage.TabIndex = 1;
            this.averagesTabPage.Text = "Averages";
            // 
            // timeOfDayRadioButton
            // 
            this.timeOfDayRadioButton.AutoSize = true;
            this.timeOfDayRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.timeOfDayRadioButton.Location = new System.Drawing.Point(194, 222);
            this.timeOfDayRadioButton.Name = "timeOfDayRadioButton";
            this.timeOfDayRadioButton.Size = new System.Drawing.Size(95, 17);
            this.timeOfDayRadioButton.TabIndex = 22;
            this.timeOfDayRadioButton.Text = "Per time of day";
            this.timeOfDayRadioButton.UseVisualStyleBackColor = true;
            this.timeOfDayRadioButton.CheckedChanged += new System.EventHandler(this.averagesRadioButton_CheckedChanged);
            // 
            // dayOfWeekRadioButton
            // 
            this.dayOfWeekRadioButton.AutoSize = true;
            this.dayOfWeekRadioButton.Checked = true;
            this.dayOfWeekRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dayOfWeekRadioButton.Location = new System.Drawing.Point(24, 222);
            this.dayOfWeekRadioButton.Name = "dayOfWeekRadioButton";
            this.dayOfWeekRadioButton.Size = new System.Drawing.Size(102, 17);
            this.dayOfWeekRadioButton.TabIndex = 21;
            this.dayOfWeekRadioButton.TabStop = true;
            this.dayOfWeekRadioButton.Text = "Per day of week";
            this.dayOfWeekRadioButton.UseVisualStyleBackColor = true;
            this.dayOfWeekRadioButton.CheckedChanged += new System.EventHandler(this.averagesRadioButton_CheckedChanged);
            // 
            // averagesChart
            // 
            this.averagesChart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.averagesChart.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            this.averagesChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Raised;
            chartArea3.AxisX.MajorTickMark.Interval = 0D;
            chartArea3.AxisX.MajorTickMark.IntervalOffset = 0D;
            chartArea3.AxisX.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea3.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea3.AxisX.MinorTickMark.Enabled = true;
            chartArea3.AxisX.ScrollBar.IsPositionedInside = false;
            chartArea3.BackColor = System.Drawing.Color.White;
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.Name = "Default";
            this.averagesChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.averagesChart.Legends.Add(legend3);
            this.averagesChart.Location = new System.Drawing.Point(17, 52);
            this.averagesChart.Name = "averagesChart";
            series3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.VerticalCenter;
            series3.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(162)))), ((int)(((byte)(225)))));
            series3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(164)))));
            series3.BorderWidth = 2;
            series3.ChartArea = "Default";
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(164)))));
            series3.Legend = "Legend1";
            series3.Name = "kWh";
            this.averagesChart.Series.Add(series3);
            this.averagesChart.Size = new System.Drawing.Size(833, 158);
            this.averagesChart.TabIndex = 20;
            this.averagesChart.Text = "History";
            title3.Name = "Title1";
            title3.Text = "Energy use in kWh";
            title3.Visible = false;
            this.averagesChart.Titles.Add(title3);
            // 
            // updateFormAfterConnectBackgroundWorker
            // 
            this.updateFormAfterConnectBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.connectionBackgroundWorker_DoWork);
            this.updateFormAfterConnectBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.connectionBackgroundWorker_RunWorkerCompleted);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Energeniaal.Properties.Resources.Energeniaal_logo_extra_small;
            this.pictureBox1.Location = new System.Drawing.Point(10, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // variableLogoPictureBox
            // 
            this.variableLogoPictureBox.Location = new System.Drawing.Point(628, 0);
            this.variableLogoPictureBox.Name = "variableLogoPictureBox";
            this.variableLogoPictureBox.Size = new System.Drawing.Size(230, 65);
            this.variableLogoPictureBox.TabIndex = 21;
            this.variableLogoPictureBox.TabStop = false;
            this.variableLogoPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.variableLogoPictureBox_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.variableLogoPictureBox);
            this.panel1.Controls.Add(this.tempUnitLabel);
            this.panel1.Controls.Add(this.timeLabel);
            this.panel1.Controls.Add(this.tempLabel);
            this.panel1.Controls.Add(this.wattsUnitLabel);
            this.panel1.Controls.Add(this.wattsLabel);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 70);
            this.panel1.TabIndex = 22;
            // 
            // tempUnitLabel
            // 
            this.tempUnitLabel.AutoSize = true;
            this.tempUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tempUnitLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.tempUnitLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tempUnitLabel.Location = new System.Drawing.Point(359, 20);
            this.tempUnitLabel.Name = "tempUnitLabel";
            this.tempUnitLabel.Size = new System.Drawing.Size(37, 25);
            this.tempUnitLabel.TabIndex = 25;
            this.tempUnitLabel.Text = "°C";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.timeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.timeLabel.Location = new System.Drawing.Point(168, 20);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(112, 25);
            this.timeLabel.TabIndex = 21;
            this.timeLabel.Text = "10:13 PM";
            // 
            // tempLabel
            // 
            this.tempLabel.AutoSize = true;
            this.tempLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tempLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.tempLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tempLabel.Location = new System.Drawing.Point(327, 20);
            this.tempLabel.Name = "tempLabel";
            this.tempLabel.Size = new System.Drawing.Size(38, 25);
            this.tempLabel.TabIndex = 22;
            this.tempLabel.Text = "25";
            this.tempLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // wattsUnitLabel
            // 
            this.wattsUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wattsUnitLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.wattsUnitLabel.Location = new System.Drawing.Point(498, 20);
            this.wattsUnitLabel.Name = "wattsUnitLabel";
            this.wattsUnitLabel.Size = new System.Drawing.Size(78, 20);
            this.wattsUnitLabel.TabIndex = 24;
            this.wattsUnitLabel.Text = "KWatt";
            // 
            // wattsLabel
            // 
            this.wattsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wattsLabel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.wattsLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.wattsLabel.Location = new System.Drawing.Point(424, 20);
            this.wattsLabel.Name = "wattsLabel";
            this.wattsLabel.Size = new System.Drawing.Size(68, 23);
            this.wattsLabel.TabIndex = 23;
            this.wattsLabel.Text = "5000";
            this.wattsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // connectionBackgroundWorker
            // 
            this.connectionBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.connectionBackgroundWorker_DoWork);
            this.connectionBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.connectionBackgroundWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 683);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chartTabControl);
            this.Controls.Add(this.realTimeGroupBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wattsChart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.realTimeGroupBox.ResumeLayout(false);
            this.realTimeGroupBox.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.energeniaalLogoPictureBox)).EndInit();
            this.chartTabControl.ResumeLayout(false);
            this.historyTabPage.ResumeLayout(false);
            this.historyTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.historyChart)).EndInit();
            this.averagesTabPage.ResumeLayout(false);
            this.averagesTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.averagesChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.variableLogoPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker portReadingBackgroundWorker;
        private System.Windows.Forms.Timer screenUpdateTimer;
        private System.Windows.Forms.SaveFileDialog logfileSaveFileDialog;
        private System.Windows.Forms.DataVisualization.Charting.Chart wattsChart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twohourHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailyHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlyHistoryToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog exportSaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox realTimeGroupBox;
        private System.Windows.Forms.Label channelChoiceLabel;
        private System.Windows.Forms.ComboBox channelComboBox;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ComboBox realTimeSensorComboBox;
        private System.Windows.Forms.Label realTimeSensorChoiceLabel;
        private System.Windows.Forms.ToolStripMenuItem logToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem chooseLogfileToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker updateFormAfterConnectBackgroundWorker;
        private System.Windows.Forms.TabControl chartTabControl;
        private System.Windows.Forms.TabPage historyTabPage;
        private System.Windows.Forms.ComboBox histSensorComboBox;
        private System.Windows.Forms.Label histSensorChoiceLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart historyChart;
        private System.Windows.Forms.RadioButton monthRadioButton;
        private System.Windows.Forms.RadioButton dayRadioButton;
        private System.Windows.Forms.RadioButton twoHourRadioButton;
        private System.Windows.Forms.TabPage averagesTabPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart averagesChart;
        private System.Windows.Forms.RadioButton timeOfDayRadioButton;
        private System.Windows.Forms.RadioButton dayOfWeekRadioButton;
        private System.Windows.Forms.PictureBox energeniaalLogoPictureBox;
        private System.Windows.Forms.Label noDataLabel;
        private System.Windows.Forms.Label realTimeMinutesLabel;
        private System.Windows.Forms.ComboBox minutesComboBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applianceNamesToolStripMenuItem;
        private System.Windows.Forms.RadioButton yearRadioButton;
        private System.Windows.Forms.ToolStripMenuItem yearHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.PictureBox variableLogoPictureBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label tempUnitLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label tempLabel;
        private System.Windows.Forms.Label wattsUnitLabel;
        private System.Windows.Forms.Label wattsLabel;
        private System.ComponentModel.BackgroundWorker connectionBackgroundWorker;
        private System.Windows.Forms.Label noDataLabel2;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wipeDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TwoHourHistoryImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DayHistoryImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MonthHistoryImportToolStripMenuItem;
    }
}

