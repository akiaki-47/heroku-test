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
    public partial class ClassForm : Form
    {
        private LABForm labForm;
        private string level, room;
        private object buttonSen;
        public ClassForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSen = sender;
            int levelIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Level");
            int roomIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Room");
            cboLevel.DataSource = labForm.treeView.Nodes[0].Nodes[levelIndex].Tag;
            cboRoom.DataSource = labForm.treeView.Nodes[0].Nodes[roomIndex].Tag;
            cboLevel.DisplayMember = "Name";
            cboLevel.ValueMember = "UUID";
            cboRoom.DisplayMember = "No";
            cboRoom.ValueMember = "UUID";
            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                cboLevel.SelectedValue = row.Cells["Level"].Value.ToString();
                cboRoom.SelectedValue = row.Cells["Room"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = cboLevel.Text + "A" + cboRoom.Text;
            string levelId = cboLevel.SelectedValue.ToString();
            string roomId = cboRoom.SelectedValue.ToString();
            if ((buttonSen as Button).Text.Equals("Add"))
            {
                DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                DataRow row = dataTable.NewRow();
                row["UUID"] = Guid.NewGuid().ToString();
                row["Level"] = levelId;
                row["Room"] = roomId;
                row["Name"] = name;
                dataTable.Rows.Add(row);
            }
            else
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                row.Cells["Level"].Value = levelId;
                row.Cells["Room"].Value = roomId;
                row.Cells["Name"].Value = name;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLevel.Items.Count > 0)
            {
                level = cboLevel.Text;
                if (!string.IsNullOrEmpty(level) && !string.IsNullOrEmpty(room) && level.Length < 3 && room.Length < 3)
                {
                    txtName.Text = level + "A" + room;
                }
            }
        }

        private void cboRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRoom.Items.Count > 0)
            {
                room = cboRoom.Text;
                if (!string.IsNullOrEmpty(level) && !string.IsNullOrEmpty(room) && level.Length < 3 && room.Length < 3)
                {
                    txtName.Text = level + "A" + room;
                }
            }
        }
    }
}
