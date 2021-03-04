using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LAB3
{
    public partial class GradeForm : Form
    {
        private LABForm labForm;
        private object buttonSend;
        public GradeForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSend = sender;
            int subjectIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Subject");
            int studentIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Student");

            cboSubject.DataSource = labForm.treeView.Nodes[0].Nodes[subjectIndex].Tag;
            cboStudent.DataSource = labForm.treeView.Nodes[0].Nodes[studentIndex].Tag;

            cboSubject.DisplayMember = "Subject_name";
            cboSubject.ValueMember = "UUID";

            cboStudent.DisplayMember = "Name";
            cboStudent.ValueMember = "UUID";
            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                cboSubject.SelectedValue = row.Cells["Subject"].Value.ToString();
                cboStudent.SelectedValue = row.Cells["Student"].Value.ToString();
                txtPoint.Text = row.Cells["Point"].Value.ToString();
            }
        }
        public decimal RoundDown(decimal i, double decimalPlaces)
        {
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(i * power) / power;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\d");
            if (Regex.IsMatch(txtPoint.Text, reg.ToString()) && RoundDown(decimal.Parse(txtPoint.Text), 2) >= 0 && RoundDown(decimal.Parse(txtPoint.Text), 2) <= 10)
            {
                string subjectId = cboSubject.SelectedValue.ToString();
                string studentId = cboStudent.SelectedValue.ToString();
                double point = Math.Round(Double.Parse(txtPoint.Text), 2);
                
                DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                if ((buttonSend as Button).Text.Equals("Add"))
                {
                    DataRow row = dataTable.NewRow();
                    row["UUID"] = Guid.NewGuid().ToString();
                    row["Subject"] = subjectId;
                    row["Student"] = studentId;
                    row["Point"] = point;
                    dataTable.Rows.Add(row);
                }
                else
                {
                    DataGridViewRow row = labForm.dataGridView.CurrentRow;
                    row.Cells["Subject"].Value = subjectId;
                    row.Cells["Student"].Value = studentId;
                    row.Cells["Point"].Value = point;
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
