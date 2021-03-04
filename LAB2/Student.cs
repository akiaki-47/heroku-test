using LAB2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Principal;
using System.Text.Json;



// Task list
//1. khoi tao json => Serializing JSON => chuyển 1 list thành String Json. lưu thành file txt.
//2. luu du lieu vao json va doc json => Lưu ở 1. rồi -> đọc là Deserializing JSON
//3. luu thanh dang csv => Thư viện support Excel của C#. hoặc đơn giản hoá thì lưu một string voà file, xong set đuôi file (Extension) là csv


namespace ProgramManagerStudent
{
    /// <summary>
    /// Generate method get, set id, fullname, birthday, isMale, classID
    /// </summary>
    class Student
    {
        private string id;
        private string fullname;
        private DateTime birthday;
        private bool isMale;
        private string classID;
        /// <summary>
        /// get id
        /// </summary>
        public string ID { get { return id; } }
        /// <summary>
        /// get Fullname
        /// </summary>
        public string FullName { get { return fullname; } }

        /// <summary>
        /// get birthday
        /// </summary>
        public DateTime Birthday { get { return birthday; } }

        /// <summary>
        /// get ClassID
        /// </summary>
        public string ClassID { get { return classID; } }

        /// <summary>
        /// Get Gender
        /// </summary>
        public bool IsMale { get { return isMale; } }

        /// <summary>
        /// Constructor of student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="birthday"></param>
        /// <param name="isMale"></param>
        /// <param name="classID"></param>
        public Student(string id, string fullname, DateTime birthday, bool isMale, string classID)
        {
            this.id = id;
            this.fullname = fullname;
            this.birthday = birthday;
            this.isMale = isMale;
            this.classID = classID;
        }

        public Student()
        {
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
                var jsonStudent = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonStudent != null)
                {
                    var jsonStudentList = jsonStudent["Student"];
                    foreach (var student in jsonStudentList)
                    {
                        string uuid = student["ID"].ToString();
                        string name = student["FullName"].ToString();
                        string birthday = student["Birthday"].ToString();
                        string gender = student["IsMale"].ToString();
                        string classId = student["ClassID"].ToString();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Student(UUID,Name,Birthday,Gender,Class) Values('" + uuid + "', '" + name + "','" + birthday + "', '" + gender + "', '" + classId + "')";
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
