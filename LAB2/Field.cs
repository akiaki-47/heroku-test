using LAB2;
using Newtonsoft.Json.Linq;
using ProgramManagerStudent;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ProgramManagerStudent
{
    class Field
    {
        private string uuid;
        private string name;

        public string Uuid { get { return uuid; } }
        public string Name { get { return name; } }

        public Field(string uuid, string name)
        {
            this.uuid = uuid;
            this.name = name;
        }

        public Field()
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
                var jsonField = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonField != null)
                {
                    var jsonFieldList = jsonField["Field"];
                    foreach (var field in jsonFieldList)
                    {
                        string uuid = field["Uuid"].ToString();
                        string name = field["Name"].ToString();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Field(UUID,Name) Values('" + uuid + "', '" + name + "')";
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
