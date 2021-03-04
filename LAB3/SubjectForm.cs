using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LAB3
{
    public partial class SubjectForm : Form
    {
        private LABForm labForm;
        private object buttonSend;
        public SubjectForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSend = sender;
            int levelIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Level");
            int fieldIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Field");
            cboLevel.DataSource = labForm.treeView.Nodes[0].Nodes[levelIndex].Tag;
            cboField.DataSource = labForm.treeView.Nodes[0].Nodes[fieldIndex].Tag;

            cboLevel.DisplayMember = "Name";
            cboLevel.ValueMember = "UUID";
            cboField.DisplayMember = "Name";
            cboField.ValueMember = "UUID";

            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                cboLevel.SelectedValue = row.Cells["Level"].Value.ToString();
                cboField.SelectedValue = row.Cells["Field"].Value.ToString();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string levelId = cboLevel.SelectedValue.ToString();
            string fieldId = cboField.SelectedValue.ToString();
            string name = cboField.Text + " " + cboLevel.Text;
            DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
            if ((buttonSend as Button).Text.Equals("Add"))
            {
                DataRow row = dataTable.NewRow();
                row["UUID"] = Guid.NewGuid().ToString();
                row["Level"] = levelId;
                row["Field"] = fieldId;
                row["Subject_name"] = name;
                dataTable.Rows.Add(row);
            }
            else
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                row.Cells["Level"].Value = levelId;
                row.Cells["Field"].Value = fieldId;
                row.Cells["Subject_name"].Value = name;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
