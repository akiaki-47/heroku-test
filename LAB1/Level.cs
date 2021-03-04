using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public string UUID { get { return uuid; } }
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

        /// <summary>
        /// Create Level.
        /// </summary>
        /// <param name="numbOfRoom"></param>
        /// <returns></returns>
        public static List<Level> CreateLevel(uint numbOfRoom)
        {
            List<Level> levelList = new List<Level>();
            string configContent = File.ReadAllText(@"Configure.json");
            Configure config = JsonSerializer.Deserialize<Configure>(configContent);
            LevelConfigure levelConfig = config.LevelConfig;
            for (int i = levelConfig.level_name_set.Length; i > 0 ; i--)
            {
                uint numbOfClass = (uint) Math.Ceiling((double)numbOfRoom / i);
                numbOfRoom -= numbOfClass;
                string uuid = Guid.NewGuid().ToString();
                string levelName = levelConfig.level_name_set[levelConfig.level_name_set.Length - i];
                levelList.Add(new Level(uuid, levelName, numbOfClass));
            }
            return levelList;
        }// end of CreateLevel
        /// <summary>
        /// Save to file
        /// </summary>
        /// <param name="levelList"></param>
        public static void SaveLevel(List<Level> levelList, string filename)
        {
            String content = "UUID,Name";
            foreach (Level level in levelList)
            {
                content += "\n" + level.UUID + "," + level.Name;
            }
            File.WriteAllText(@"" + filename + "/Level.csv", content);
        }// end of SaveLevel

        
    }
}
