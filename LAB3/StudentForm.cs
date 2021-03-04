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
    public partial class StudentForm : Form
    {
        private LABForm labForm = null;
        public StudentForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            int classIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Class");

            cboClass.DataSource = labForm.treeView.Nodes[0].Nodes[classIndex].Tag;
            cboClass.DisplayMember = "Name";
            cboClass.ValueMember = "UUID";

            if((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                txtStudent.Text = row.Cells["Name"].Value.ToString();
                birthday.Value = DateTime.Parse(row.Cells["Birthday"].Value.ToString());
                bool gender = bool.Parse(row.Cells["Gender"].Value.ToString());
                if (rdbFemale.Checked)
                {
                    gender = false;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtStudent.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be blank!");
            }
            else
            {
                string birthday = DateTime.Now.ToShortDateString();
                bool gender = true;
                if (rdbFemale.Checked)
                {
                    gender = false;
                }
                string classId = cboClass.SelectedValue.ToString();
                if ((sender as Button).Text.Equals("Add"))
                {
                    DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                    DataRow row = dataTable.NewRow();
                    row["UUID"] = Guid.NewGuid().ToString();
                    row["Name"] = name;
                    row["Birthday"] = birthday;
                    row["Gender"] = gender;
                    row["Class"] = classId;
                    dataTable.Rows.Add(row);
                } 
                else
                {
                    DataGridViewRow row = labForm.dataGridView.CurrentRow;
                    row.Cells["Name"].Value = name;
                    row.Cells["Birthday"].Value = birthday;
                    row.Cells["Gender"].Value = gender;
                    row.Cells["Class"].Value = classId;
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
