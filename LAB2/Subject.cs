using LAB2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
namespace ProgramManagerStudent
{
    class Subject
    {
        private string uuid;
        private string levelUUID;
        private string fieldUUID;
        private string subject_name;

        public string FieldUUID { get { return fieldUUID; } }
        public string LevelUUID { get { return levelUUID; } }
        public string Uuid { get { return uuid; } }

        public string Subject_name { get { return subject_name; } }

        public Subject(string uuid, string levelUUID, string fieldUUID, string subject_name)
        {
            this.uuid = uuid;
            this.levelUUID = levelUUID;
            this.fieldUUID = fieldUUID;
            this.subject_name = subject_name;
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
                var jsonSubject = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonSubject != null)
                {
                    var jsonSubjectList = jsonSubject["Subject"];
                    foreach (var teacher in jsonSubjectList)
                    {
                        string uuid = teacher["Uuid"].ToString();
                        string levelId = teacher["LevelUUID"].ToString();
                        string fieldId = teacher["FieldUUID"].ToString();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Subject(UUID,[Level],Field) Values('" + uuid + "', '" + levelId + "', '" + fieldId + "')";
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
