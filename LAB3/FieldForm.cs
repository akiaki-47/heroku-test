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
    public partial class FieldForm : Form
    {
        private LABForm labForm;
        private object buttonSend;
        public FieldForm()
        {
            InitializeComponent();
        }
        public FieldForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSend = sender;
            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                txtName.Text = row.Cells["Name"].Value.ToString();
            }  
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be blank!");
            } else 
            {
                if((buttonSend as Button).Text.Equals("Add"))
                {
                    DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                    DataRow row = dataTable.NewRow();
                    row["UUID"] = Guid.NewGuid().ToString();
                    row["Name"] = name;
                    dataTable.Rows.Add(row);
                }
                else
                {
                    DataGridViewRow row = labForm.dataGridView.CurrentRow;
                    row.Cells["Name"].Value = name;
                }
                this.Close();
            }
        }
    }
}
