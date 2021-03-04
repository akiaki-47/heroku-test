using LAB2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
namespace ProgramManagerStudent
{
    class Room
    {
        /// <summary>
        /// generate uuid, no
        /// </summary>
        private string uuid;
        private uint no;
        private uint classId;
        
        /// <value>uuid</value>
        public string Uuid { get { return uuid; }}
        
        /// <value>getNo</value>
        public uint No { get { return no; }}

        public uint ClassId { get => classId; set => classId = value; }

        /// <summary>
        /// Generate Constructor Room
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="no"></param>
        /// <param name="numbOfStudent"></param>
        public Room(string uuid, uint no)
        {
            this.uuid = uuid;
            this.no = no;
        }

        /// <summary>
        /// Generate Room without parameter
        /// </summary> 
        public Room()
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
                var jsonRoom = JObject.Parse(content);
                conn = util.getConnection();
                conn.Open();
                
                if (jsonRoom != null)
                {
                    var jsonRoomsList = jsonRoom["Room"];
                    foreach (var room in jsonRoomsList)
                    {
                        string uuid = room["Uuid"].ToString();
                        uint no = room["No"].ToObject<uint>();
                        string sql = "INSERT INTO " + databaseName + ".dbo.Room(UUID,No) Values('" + uuid + "', '" + no + "')";
                        command = new SqlCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                    result = true;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(command != null)
                {
                    command.Dispose();
                }
                if(conn != null)
                {
                    conn.Close();
                }
            }
            
            return result;
        }
    }
}
