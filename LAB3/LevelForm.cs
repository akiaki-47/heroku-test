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
    public partial class LevelForm : Form
    {
        private LABForm labForm;
        private object buttonSend;
        public LevelForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSend = sender;
            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtNumbOfClass.Text = row.Cells["NumbOfClass"].Value.ToString();
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
                int numOfClass = int.Parse(txtNumbOfClass.Text);
                if((buttonSend as Button).Text.Equals("Add"))
                {
                    DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                    DataRow row = dataTable.NewRow();
                    row["UUID"] = Guid.NewGuid().ToString();
                    row["Name"] = name;
                    row["NumbOfClass"] = numOfClass;
                    dataTable.Rows.Add(row);
                }
                else
                {
                    DataGridViewRow row = labForm.dataGridView.CurrentRow;
                    row.Cells["Name"].Value = name;
                    row.Cells["NumbOfClass"].Value = numOfClass;
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
