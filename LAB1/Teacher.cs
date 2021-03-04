using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProgramManagerStudent
{
    class Teacher
    {
        private string uuid;
        private string name;
        private bool gender;
        private string fieldUUID;

        public string UUID { get { return uuid; } }
        public string Name { get { return name; } }
        public bool Gender { get { return gender; } }
        public string Field { get { return fieldUUID; } }

        public Teacher(string uuid, string name, bool gender, string fieldUUID)
        {
            this.uuid = uuid;
            this.name = name;
            this.gender = gender;
            this.fieldUUID = fieldUUID;
        }

        public static List<Teacher> CreateTeacher(List<Class> classList, List<Subject> subjectList)
        {
            List<Teacher> teacherList = new List<Teacher>();

            string content = File.ReadAllText(@"Configure.json");
            Configure config = JsonSerializer.Deserialize<Configure>(content);

            foreach (Subject subject in subjectList)
            {
                bool isMale = GenerateRandomGender();
                string fullName = GenerateRandomName(isMale);
                string uuid = Guid.NewGuid().ToString();
                teacherList.Add(new Teacher(uuid, fullName, isMale, subject.Field));
            }
            while(teacherList.Count < classList.Count)
            {
                bool isMale = GenerateRandomGender();
                string fullName = GenerateRandomName(isMale);
                string uuid = Guid.NewGuid().ToString();
                Random rnd = new Random();
                string fieldUuid = subjectList[rnd.Next(0, subjectList.Count)].Field;
                teacherList.Add(new Teacher(uuid, fullName, isMale, fieldUuid));
            }
            return teacherList;
        }
        public static void SaveTeachers(List<Teacher> teacherList, string filename)
        {
            String content = "UUID,Name,Gender,Field\n";
            foreach (Teacher classObject in teacherList)
            {
                content += classObject.UUID + ", " + classObject.Name + ", " + classObject.Gender + ", " + classObject.Field + "\n";
            }
            File.WriteAllText(@"" + filename + "/Teacher.csv", content);
        }

        private static bool GenerateRandomGender()
        {
            Random rnd = new Random();
            bool isMale = true;
            double tmp = rnd.NextDouble();
            if (tmp >= 0.5)
            {
                isMale = false;
            }
            return isMale;
        }

        private static string GenerateRandomName(bool isMale)
        {
            Random rnd = new Random();
            string content = File.ReadAllText(@"Configure.json");
            Configure config = JsonSerializer.Deserialize<Configure>(content);
            NameConfigure nameConfig = config.NameConfigure;
            string fullName = "";
            string lastName = nameConfig.last_name_set[rnd.Next(nameConfig.last_name_set.Length)];
            if (isMale)
            {
                string middleName = nameConfig.male_middle_name_set[rnd.Next(nameConfig.male_middle_name_set.Length)];
                string firstName = nameConfig.male_first_name_set[rnd.Next(nameConfig.male_first_name_set.Length)];
                fullName = lastName + " " + middleName + " " + firstName;
            }
            else
            {
                string middleName = nameConfig.female_middle_name_set[rnd.Next(nameConfig.female_middle_name_set.Length)];
                string firstName = nameConfig.female_first_name_set[rnd.Next(nameConfig.female_first_name_set.Length)];
                fullName = lastName + " " + middleName + " " + firstName;
            }
            return fullName;
        }
    }

}
