using LAB2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ProgramManagerStudent
{
    class Teacher
    {
        private string uuid;
        private string name;
        private bool gender;
        private string fieldUUID;

        public string Uuid { get { return uuid; } }
        public string Name { get { return name; } }
        public bool Gender { get { return gender; } }
        public string FieldUUID { get { return fieldUUID; } }

        public Teacher(string uuid, string name, bool gender, string fieldUUID)
        {
            this.uuid = uuid;
            this.name = name;
            this.gender = gender;
            this.fieldUUID = fieldUUID;
        }

        public static bool SaveToDB(string fileName, string databaseName)
        {
            bool result = true;
            DBUtils util = new DBUtils();
            SqlConnection conn = null;
            SqlCommand command = null;
            try
            {
                string content = File.ReadAllText(fileName + ".json");
                var jsonTeacher = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonTeacher != null)
                {
                    var jsonTeacherList = jsonTeacher["Teacher"];
                    foreach (var teacher in jsonTeacherList)
                    {
                        string uuid = teacher["Uuid"].ToString();
                        string name = teacher["Name"].ToString();
                        string gender = teacher["Gender"].ToString();
                        string fieldId = teacher["FieldUUID"].ToString();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Teacher(UUID,Name,Gender,Field) Values('" + uuid + "', '" + name + "', '" + gender + "', '" + fieldId + "')";
                        command = new SqlCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return result;
        }
    }

}
