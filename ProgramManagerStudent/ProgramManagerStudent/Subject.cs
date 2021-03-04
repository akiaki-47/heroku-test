using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Create a random subject.
        /// </summary>
        public static List<Subject> CreateSubject(List<Level> levelList, List<Field> fieldList)
        {
            List<Subject> subjectsList = new List<Subject>();
            foreach (Level level in levelList)
            {
                Random rnd = new Random();
                int numOfSub = rnd.Next(8, 11);
                int[] temp = new int[numOfSub];
                for (int i = 0; i < numOfSub; i++)
                {
                    int index = rnd.Next(numOfSub);
                    if (temp[index] == 0)
                    {
                        string subject_name = fieldList[index].Name + " " + level.Name;
                        subjectsList.Add(new Subject(Guid.NewGuid().ToString(), level.Uuid, fieldList[index].Uuid, subject_name));
                        temp[index] = 1;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            return subjectsList;
        } //end Create method

        /// <summary>
        /// Save to file method.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void SaveSubject(List<Subject> subjectList)
        {
            try
            {
                String content = "UUID, LevelUUID, RoomUUID, Name\n";
                foreach (Subject classObject in subjectList)
                {
                    content += classObject.Uuid + ", " + classObject.LevelUUID + ", " + classObject.FieldUUID + ", " + classObject.Subject_name + "\n";
                }
                File.WriteAllText(@"..\..\..\Subjects.csv", content);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
