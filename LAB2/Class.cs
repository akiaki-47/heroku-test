using LAB2;
using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ProgramManagerStudent
{
    /// <summary>
    /// Generate methods class UUID, levelUUID, roomUUID, name
    /// </summary>
    class Class
    {
        private string uuid;
        private string levelUUID;
        private string roomUUID;
        private string name;

        /// <summary>
        /// Create constructor of Class
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="levelUUID"></param>
        /// <param name="roomUUID"></param>
        /// <param name="name"></param>
        public Class(string uuid, string levelUUID, string roomUUID, string name)
        {
            this.uuid = uuid;
            this.levelUUID = levelUUID;
            this.roomUUID = roomUUID;
            this.name = name;
        }

        /// <summary>
        /// Get uuid of class
        /// </summary>
        public string Uuid { get { return uuid; } set { Uuid = value; } }
        /// <summary>
        /// Get LevelUUID
        /// </summary>
        public string LevelUUID { get { return levelUUID; } }
        /// <summary>
        /// Get RoomUUID
        /// </summary>
        public string RoomUUID { get { return roomUUID; } }
        /// <summary>
        /// Get Name of Class
        /// </summary>
        public string Name { get { return name; } }

        public static bool SaveToDB(string fileName, string databaseName)
        {
            bool result = true;
            DBUtils util = new DBUtils();
            SqlConnection conn = null;
            SqlCommand command = null;
            try
            {
                string content = File.ReadAllText(fileName + ".json");
                var jsonClass = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();

                if (jsonClass != null)
                {
                    var jsonClassList = jsonClass["Class"];
                    foreach (var classes in jsonClassList)
                    {
                        string uuid = classes["Uuid"].ToString();
                        string levelUUID = classes["LevelUUID"].ToString();
                        string roomUUID = classes["RoomUUID"].ToString();
                        string name = classes["Name"].ToString();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Class(UUID,Name,Room,[Level]) Values('" + uuid + "', '" + name + "', '" + roomUUID + "', '" + levelUUID + "')";
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
