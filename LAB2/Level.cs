using LAB2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;

namespace ProgramManagerStudent
{
    public class Level
    {
        /// <summary>
        /// Generate method create a level.
        /// </summary>
        private string uuid;
        private string name;
        private uint numbOfClass;

        /// <value>Get uuid</value>
        public string Uuid { get { return uuid; } }
        /// <value>Get Name</value>
        public string Name { get { return name; } }
        /// <value>Get NumbOfClass</value>
        public uint NumbOfClass { get { return numbOfClass; } }

        /// <summary>
        /// Generate method Level without parameter
        /// </summary>
        public Level()
        {
        }

        /// <summary>
        /// Generate method has parameter
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="name"></param>
        /// <param name="numbOfClass"></param>
        public Level(string uuid, string name, uint numbOfClass)
        {
            this.uuid = uuid;
            this.name = name;
            this.numbOfClass = numbOfClass;
        }

        // end of CreateLevel
        /// <summary>
        /// Save to file
        /// </summary>
        /// <param name="levelList"></param>
        public static bool SaveToDB(string fileName, string databaseName)
        {
            bool result = true;
            DBUtils util = new DBUtils();
            SqlConnection conn = null;
            SqlCommand command = null;
            try
            {
                string content = File.ReadAllText(fileName + ".json");
                var jsonLevel = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonLevel != null)
                {
                    var jsonLevelList = jsonLevel["Level"];
                    foreach (var level in jsonLevelList)
                    {
                        string uuid = level["Uuid"].ToString();
                        string name = level["Name"].ToString();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Level(UUID,Name) Values('" + uuid + "', '" + name + "')";
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
