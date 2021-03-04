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
    public partial class AttendanceForm : Form
    {
        public LABForm labForm;
        private object buttonSend;
        public AttendanceForm(LABForm labForm, object sender)
        {
            InitializeComponent();
            this.labForm = labForm;
            buttonSend = sender;
            int teacherIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Teacher");
            int classIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Class");
            int subjectIndex = labForm.treeView.Nodes[0].Nodes.IndexOfKey("Subject");

            cboTeacher.DataSource = labForm.treeView.Nodes[0].Nodes[teacherIndex].Tag;
            cboClass.DataSource = labForm.treeView.Nodes[0].Nodes[classIndex].Tag;
            cboSubject.DataSource = labForm.treeView.Nodes[0].Nodes[subjectIndex].Tag;

            cboTeacher.DisplayMember = "Name";
            cboClass.DisplayMember = "Name";
            cboSubject.DisplayMember = "Subject_name";

            cboTeacher.ValueMember = "UUID";
            cboClass.ValueMember = "UUID";
            cboSubject.ValueMember = "UUID";
            if ((sender as Button).Text.Equals("Update"))
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                cboTeacher.SelectedValue = row.Cells["Teacher"].Value.ToString();
                cboClass.SelectedValue = row.Cells["Class"].Value.ToString();
                cboSubject.SelectedValue = row.Cells["Subject"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string teacherId = cboTeacher.SelectedValue.ToString();
            string classId = cboClass.SelectedValue.ToString();
            string subjectId = cboSubject.SelectedValue.ToString();
            if ((buttonSend as Button).Text.Equals("Add"))
            {
                DataTable dataTable = (DataTable)labForm.dataGridView.DataSource;
                DataRow row = dataTable.NewRow();
                row["UUID"] = Guid.NewGuid().ToString();
                row["Teacher"] = teacherId;
                row["Class"] = classId;
                row["Subject"] = subjectId;
                dataTable.Rows.Add(row);
            }
            else
            {
                DataGridViewRow row = labForm.dataGridView.CurrentRow;
                row.Cells["Teacher"].Value = teacherId;
                row.Cells["Class"].Value = classId;
                row.Cells["Subject"].Value = subjectId;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
