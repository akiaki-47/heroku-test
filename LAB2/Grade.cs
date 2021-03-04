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
    /// <summary>
    /// Contain method for create Grade
    /// </summary>
    class Grade
    {
        private string subjectUUID;
        private string studentUUID;
        private double gradeOfStudent;

        public Grade(string subjectUUID, string studentUUID, double gradeOfStudent)
        {
            this.subjectUUID = subjectUUID;
            this.studentUUID = studentUUID;
            this.gradeOfStudent = gradeOfStudent;
        }

        public double GradeOfStudent { get { return gradeOfStudent; } }
        public string StudentUUID { get { return studentUUID; } }
        public string SubjectUUID { get { return subjectUUID; } }

        public static bool SaveToDB(string fileName, string databaseName)
        {
            bool result = true;
            DBUtils util = new DBUtils();
            SqlConnection conn = null;
            SqlCommand command = null;
            try
            {
                string content = File.ReadAllText(fileName + ".json");
                var jsonGrade = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonGrade != null)
                {
                    var jsonGradeList = jsonGrade["Grade"];
                    foreach (var grade in jsonGradeList)
                    {
                        string uuid = grade["SubjectUUID"].ToString();
                        string studentId = grade["StudentUUID"].ToString();
                        uint grades = grade["GradeOfStudent"].ToObject<uint>();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Grade(Subject,Student,Point) Values('" + uuid + "', '" + studentId + "', '" + grades + "')";
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
