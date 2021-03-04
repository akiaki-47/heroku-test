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
    public partial class TeacherForm : Form
    {
        private LABForm labForm;
        private object buttonSend;
        public TeacherForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSend = sender;
            int fieldIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Field");
            cboField.DataSource = labForm.treeView.Nodes[0].Nodes[fieldIndex].Tag;
            cboField.DisplayMember = "Name";
            cboField.ValueMember = "UUID";
            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                cboField.SelectedValue = row.Cells["Field"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                bool gender = bool.Parse(row.Cells["Gender"].Value.ToString());
                if (rdFemale.Checked)
                {
                    gender = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be blank!");
            }
            else
            {
                bool gender = true;
                if (rdFemale.Checked)
                {
                    gender = false;
                }
                string fieldId = cboField.SelectedValue.ToString();
                DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                if ((buttonSend as Button).Text.Equals("Add"))
                {
                    DataRow row = dataTable.NewRow();
                    row["UUID"] = Guid.NewGuid().ToString();
                    row["Name"] = name;
                    row["Gender"] = gender;
                    row["Field"] = fieldId;
                    dataTable.Rows.Add(row);
                }
                else
                {
                    DataGridViewRow row = labForm.dataGridView.CurrentRow;
                    row.Cells["Name"].Value = name;
                    row.Cells["Gender"].Value = gender;
                    row.Cells["Field"].Value = fieldId;
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
