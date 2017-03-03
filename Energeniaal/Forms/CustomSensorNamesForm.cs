using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Energeniaal
{
    public partial class CustomSensorNamesForm : Form
    {
        private DataTable sensorNamesTable = new DataTable("SensorNames");

        public CustomSensorNamesForm()
        {
            InitializeComponent();

            // Create the columns in the datatable
            this.sensorNamesTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
            this.sensorNamesTable.Columns.AddRange(new DataColumn[] 
                {
                    new DataColumn(MyStrings.Number, typeof(int)),
                    new DataColumn(MyStrings.Name, typeof(string)) 
                });
            this.sensorNamesDataGridView.DataSource = this.sensorNamesTable;
            // Disable sorting by clicking on column headers
            foreach (DataGridViewColumn column in this.sensorNamesDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CustomSensorNamesForm_Load(object sender, EventArgs e)
        {
            this.Text = MyStrings.Appliances;
            this.cancelButton.Text = MyStrings.CancelButton;
            this.FillSensorNamesDataGridView();
        }

        private void FillSensorNamesDataGridView()
        {
            foreach (KeyValuePair<int, string> pair in SensorNameHandler.SensorNames.OrderBy(i => i.Key))
            {
                // Don't add sensor 0 (whole house) to grid
                if (pair.Key > 0)
                {
                    // Create row
                    DataRow row = this.sensorNamesTable.NewRow();
                    row[MyStrings.Number] = pair.Key;
                    row[MyStrings.Name] = pair.Value;

                    // Add row to table
                    this.sensorNamesTable.Rows.Add(row);
                }
            }

            this.sensorNamesDataGridView.DataSource = this.sensorNamesTable;
            // Make certain cells read only, and make them look disabled
            this.sensorNamesDataGridView.Columns[MyStrings.Number].ReadOnly = true;
            this.sensorNamesDataGridView.Columns[MyStrings.Number].DefaultCellStyle.BackColor 
                = SystemColors.Control;
            this.sensorNamesDataGridView.Columns[MyStrings.Number].DefaultCellStyle.ForeColor 
                = SystemColors.GrayText;
            this.sensorNamesDataGridView.Columns[MyStrings.Number].MinimumWidth = 35;
            this.sensorNamesDataGridView.Columns[MyStrings.Name].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.sensorNamesDataGridView.AutoResizeColumns(); 
            // SetWholeHouseSensorRowToReadOnly();
        }

        private void SetWholeHouseSensorRowToReadOnly()
        {
            int rowIndex = -1;

            DataGridViewRow row = sensorNamesDataGridView.Rows
                .Cast<DataGridViewRow>()
                .Where(r => Convert.ToInt32(r.Cells[MyStrings.Number].Value).Equals(0))
                .First();

            rowIndex = row.Index;

            // If row hasn't been found, then there's a problem, but check just to be safe
            if (rowIndex != -1)
            {
                this.sensorNamesDataGridView.Rows[rowIndex].ReadOnly = true;
                this.sensorNamesDataGridView.Rows[rowIndex].DefaultCellStyle.BackColor
                    = SystemColors.Control;
                this.sensorNamesDataGridView.Rows[rowIndex].DefaultCellStyle.ForeColor
                    = SystemColors.GrayText;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Save custom names to settings using SensorNameHandler
            Dictionary<int, string> sensorNames = new Dictionary<int, string>();

            foreach (DataGridViewRow row in this.sensorNamesDataGridView.Rows)
            {
                // Ignore read-only whole house sensor row
                if (!row.ReadOnly)
                {
                    string customName = row.Cells[MyStrings.Name].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(customName))
                    {
                        int sensorNumber = Convert.ToInt32(row.Cells[MyStrings.Number].Value);
                        sensorNames.Add(sensorNumber, customName);
                    }
                }
            }

            SensorNameHandler.SensorNames = sensorNames;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
