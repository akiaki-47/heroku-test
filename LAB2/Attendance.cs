using LAB2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ProgramManagerStudent
{
    class Attendance
    {
        private string teacherId;
        private string classId;
        private string subjectId;

        public string TeacherId { get { return teacherId; } }
        public string ClassId { get { return classId; } }
        public string SubjectId { get { return subjectId; } }

        public Attendance(string teacherId, string classId, string subjectId)
        {
            this.teacherId = teacherId;
            this.classId = classId;
            this.subjectId = subjectId;
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
                var jsonAttendance = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonAttendance != null)
                {
                    var jsonAttendanceList = jsonAttendance["Attendence"];
                    foreach (var attendance in jsonAttendanceList)
                    {
                        string teacher = attendance["TeacherId"].ToString();
                        string classes = attendance["ClassId"].ToString();
                        string subject = attendance["SubjectId"].ToString();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Attendance(Teacher,Class,Subject) Values('" + teacher + "', '" + classes + "', '" + subject + "')";
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
