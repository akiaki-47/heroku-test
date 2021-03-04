using System;
using System.Collections.Generic;
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
        public string UUID { get { return id; } }
        /// <summary>
        /// get Fullname
        /// </summary>
        public string Name { get { return fullname; } }

        /// <summary>
        /// get birthday
        /// </summary>
        public DateTime Birthday { get { return birthday; } }

        /// <summary>
        /// get ClassID
        /// </summary>
        public string Class { get { return classID; } }

        /// <summary>
        /// Get Gender
        /// </summary>
        public bool Gender { get { return isMale; } }

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


        /// <summary>
        /// Method Create Student
        /// </summary>
        /// <param name="classList"></param>
        /// <param name="roomList"></param>
        /// <returns>List student</returns>
        public static List<Student> CreateStudent(List<Class> classList, List<Room> roomList)
        {
            List<Student> studentList = new List<Student>();

            for(int i = 0; i < classList.Count; i++)
            {
                uint numOfStudent = roomList[i].NumbOfStudent;
                for(int j = 0; j < numOfStudent; j++)
                {
                    bool isMale = GenerateRandomGender();
                    string fullName = GenerateRandomName(isMale);
                    string classID = classList[i].UUID;
                    int level = Int32.Parse(classList[i].Name.Remove(2));
                    DateTime birthday = GenerateRandomBirthday(level);
                    string uuid = Guid.NewGuid().ToString();
                    studentList.Add(new Student(uuid, fullName, birthday, isMale, classID));
                }
            }
            return studentList;
        }
        public static void SaveStudents(List<Student> studentList, string filename)
        {
            String content = "UUID,Name,Birthday,Gender,Class";
            foreach (Student student in studentList)
            {
                string gender = "Female";
                if (student.isMale)
                {
                    gender = "Male";
                }
                content += "\n" + student.UUID + "," + student.Name + "," + student.Birthday + "," + gender + "," + student.Class;
            }
            File.WriteAllText(@"" + filename + "/Student.csv", content);
        }

        /// <summary>
        /// Generate Random Birthday
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Birthday of student</returns>
        private static DateTime GenerateRandomBirthday(int level)
        {
            int curYear = System.DateTime.Now.Year;
            DateTime start = new DateTime(curYear - (level + 9), 1, 1);
            DateTime end = new DateTime(curYear - (level + 5), 12, 12);
            Random rnd = new Random();
            int range = (end - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        /// <summary>
        /// Generate Random Gender
        /// </summary>
        /// <returns>student gender</returns>
        private static bool GenerateRandomGender()
        {
            Random rnd = new Random();
            bool isMale = true;
            double temp = rnd.NextDouble();
            if (temp >= 0.5)
            {
                isMale = false;
            }
            return isMale;
        }

        /// <summary>
        /// Generate method random Name
        /// </summary>
        /// <param name="isMale"></param>
        /// <returns>Fullname of student</returns>
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
