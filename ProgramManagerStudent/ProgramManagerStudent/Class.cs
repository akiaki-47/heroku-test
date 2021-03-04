using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
        public string Uuid { get { return uuid; }  }
        /// <summary>
        /// Get LevelUUID
        /// </summary>
        public string LevelUUID { get { return levelUUID; }  }
        /// <summary>
        /// Get RoomUUID
        /// </summary>
        public string RoomUUID { get { return roomUUID; } }
        /// <summary>
        /// Get Name of Class
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Create a random Class.
        /// </summary>
        /// <param name="numbOfClass"></param>
        /// <param name="roomID"></param>
        /// <param name="levelID"></param>
        /// <returns>List class</returns>
        public static List<Class> CreateClass(List<Room> listRoom, List<Level> listLevel)
        {
            List<Class> classList = new List<Class>();
            string configContent = File.ReadAllText(@"..\..\..\Configure.json");
            Configure config = JsonSerializer.Deserialize<Configure>(configContent);
            ClassConfigure classConfig = config.ClassConfigure;
            int roomIndex = 0;
            foreach (Level level in listLevel)
            {
                for (uint i = 0; i < level.NumbOfClass; i++)
                {
                    uint classNameIndex = i + 1;
                    string uuid = Guid.NewGuid().ToString();
                    string classname = level.Name + classConfig.class_name_set[i];
                    Class newClass = new Class(uuid, level.Uuid, listRoom[roomIndex].Uuid, classname);
                    classList.Add(newClass);
                    roomIndex++;
                }
            }
            return classList;
        }

        /// <summary>
        /// Save file
        /// </summary>
        /// <param name="classList"></param>
        public static void SaveClasses(List<Class> classList)
        {
            String content = "UUID, LevelUUID, RoomUUID, Name\n";
            foreach (Class classObject in classList)
            {
                content += classObject.Uuid + ", " + classObject.LevelUUID + ", " + classObject.RoomUUID + ", " + classObject.Name + "\n";
            }
            File.WriteAllText(@"..\..\..\Class.csv", content);
        }
        public static string getLevelUuid(string classUuid, List<Class> classList)
        {
            string result = "";
            foreach(Class classes in classList)
            {
                if (classes.Uuid.Equals(classUuid))
                {
                    result = classes.levelUUID;
                    break;
                }
            }
            return result;
        }
    }
}
