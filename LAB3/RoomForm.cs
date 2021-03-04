using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LAB3
{
    public partial class RoomForm : Form
    {
        private object buttonSender;
        public LABForm labForm;
        public RoomForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSender = sender;
            int nodeIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Class");
            cboClass.DataSource = labForm.treeView.Nodes[0].Nodes[nodeIndex].Tag;
            cboClass.DisplayMember = "Name";
            cboClass.ValueMember = "UUID";
            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                txtNo.Text = row.Cells["No"].Value.ToString();
                cboClass.SelectedValue = row.Cells["Class"].Value.ToString();
                txtNum.Text = row.Cells["NumbOfStudent"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Regex regex = new Regex(@"\d");
            int numofstudent = int.Parse(txtNum.Text);
            int numofRoom = int.Parse(txtNo.Text);
            string classId = cboClass.SelectedValue.ToString();
            if (!Regex.IsMatch(txtNo.Text, regex.ToString()) || int.Parse(txtNo.Text) < 1 || string.IsNullOrEmpty(txtNo.Text))
            {
                MessageBox.Show("There is no parameter, input must be number and positive number");
            } 
            else
            {
                DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                if ((buttonSender as Button).Text.Equals("Add"))
                {
                    DataRow row = dataTable.NewRow();
                    row["UUID"] = Guid.NewGuid().ToString();
                    row["Class"] = classId;
                    row["No"] = numofRoom;
                    row["NumbOfStudent"] = numofstudent;
                    dataTable.Rows.Add(row);
                }
                else
                {
                    DataGridViewRow row = labForm.dataGridView.CurrentRow;
                    row.Cells["Class"].Value = classId;
                    row.Cells["No"].Value = numofRoom;
                    row.Cells["NumbOfStudent"].Value = numofstudent;
                }
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}
