using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LAB3
{
    public partial class LABForm : Form
    {
        private TreeView tree;
        public LABForm()
        {
            InitializeComponent();
        }

        private void LoadDatabase(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                Tag = JsonConvert.DeserializeObject<School>(File.ReadAllText(filename));
                treeView.Nodes.Clear();
                TreeNode root = new TreeNode();
                root.Text = Path.GetFileNameWithoutExtension(filename);
                foreach (var prop in Tag.GetType().GetProperties())
                {
                    School school = (School)Tag;
                    TreeNode node = new TreeNode(prop.Name);
                    node.Name = prop.Name;
                    node.Tag = CreateTable(school.GetType().GetProperty(prop.Name).GetValue(school, null));
                    root.Nodes.Add(node);
                }
                treeView.Nodes.Add(root);
                treeView.ExpandAll();
            }
        }
        private DataTable CreateTable(object list)
        {
            DataTable table = new DataTable();
            treeView.Nodes.Clear();
            bool hasSchema = false;
            foreach (var obj in (Array)list)
            {
                if (!hasSchema)
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        table.Columns.Add(prop.Name, prop.GetValue(obj, null).GetType());
                    }
                    table.TableName = obj.GetType().Name;
                    table.PrimaryKey = new DataColumn[] { table.Columns["UUID"] };
                    hasSchema = true;
                }
                DataRow row = table.NewRow();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    row[prop.Name] = prop.GetValue(obj, null);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tree = (TreeView)sender;
            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                if (node == tree.SelectedNode)
                {
                    dataGridView.DataSource = node.Tag;
                }
                else
                {
                    node.BackColor = Color.Empty;
                    node.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != null)
            {
                LoadDatabase(openFileDialog.FileName);
            } else
            {
                MessageBox.Show("Wrong File");
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                DataTable table = (DataTable)dataGridView.DataSource;
                if(table.Rows.Count > e.RowIndex)
                {
                    DataRow selected_row = table.Rows[e.RowIndex];
                    if (selected_row != null)
                    {
                        foreach (TreeNode node in treeView.Nodes[0].Nodes)
                        {
                            if (node.Tag == selected_row.Table)
                            {
                                treeView.SelectedNode = node;
                                node.BackColor = SystemColors.Highlight;
                                node.ForeColor = SystemColors.HighlightText;
                            }
                            else
                            {
                                node.BackColor = Color.Empty;
                                node.ForeColor = SystemColors.ControlText;
                            }
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)dataGridView.DataSource;
            if (treeView.SelectedNode != null)
            {
                foreach (TreeNode node in treeView.Nodes[0].Nodes)
                {
                    if (node.Text == table.TableName)
                    {
                        treeView.SelectedNode = node;
                    }
                }
                //string form = treeView.SelectedNode.Text;
                if (table.TableName.Equals("Student"))
                {
                    new StudentForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Class"))
                {
                    new ClassForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Room"))
                {
                    new RoomForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Level"))
                {
                    new LevelForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Grade"))
                {
                    new GradeForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Field"))
                {
                    new FieldForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Attendance"))
                {
                    new AttendanceForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Teacher"))
                {
                    new TeacherForm(this, sender).ShowDialog();
                } else if (table.TableName.Equals("Subject"))
                {
                    new SubjectForm(this, sender).ShowDialog();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                if (this.dataGridView.SelectedRows.Count > 0)
                {
                    dataGridView.Rows.RemoveAt(this.dataGridView.SelectedRows[0].Index);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeView.Nodes.Count > 0)
            {
                Dictionary<string, object> schoolDict = new Dictionary<string, object>();
                foreach (TreeNode node in treeView.Nodes[0].Nodes)
                {
                    schoolDict.Add(node.Text, node.Tag);
                }
                String content = JsonConvert.SerializeObject(schoolDict, Formatting.Indented);
                File.WriteAllText(@"../" + treeView.TopNode.Text + ".json", content);
                MessageBox.Show("Save successfully!");
            }
            else
            {
                MessageBox.Show("Save failed!");
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("My app is released v1.0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)dataGridView.DataSource;
            if (treeView.SelectedNode != null)
            {
                foreach (TreeNode node in treeView.Nodes[0].Nodes)
                {
                    if (node.Text == table.TableName)
                    {
                        treeView.SelectedNode = node;
                    }
                }
                //string form = treeView.SelectedNode.Text;
                if (table.TableName.Equals("Student"))
                {
                    new StudentForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Class"))
                {
                    new ClassForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Room"))
                {
                    new RoomForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Level"))
                {
                    new LevelForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Grade"))
                {
                    new GradeForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Field"))
                {
                    new FieldForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Attendance"))
                {
                    new AttendanceForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Teacher"))
                {
                    new TeacherForm(this, sender).ShowDialog();
                }
                else if (table.TableName.Equals("Subject"))
                {
                    new SubjectForm(this, sender).ShowDialog();
                }
            }
        }
    }
}
