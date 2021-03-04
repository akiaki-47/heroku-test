using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ProgramManagerStudent
{
    /// <summary>
    /// Contain method for create Grade
    /// </summary>
    class Grade
    {
        private string uuid;
        private string subjectUUID;
        private string studentUUID;
        private double gradeOfStudent;

        public Grade(string uuid, string subjectUUID, string studentUUID, double gradeOfStudent)
        {
            this.UUID = uuid;
            this.subjectUUID = subjectUUID;
            this.studentUUID = studentUUID;
            this.gradeOfStudent = gradeOfStudent;
        }

        public string UUID { get => uuid; set => uuid = value; }
        public string Subject { get => subjectUUID; set => subjectUUID = value; }
        public string Student { get => studentUUID; set => studentUUID = value; }
        public double Point { get => gradeOfStudent; set => gradeOfStudent = value; }

        /// <summary>
        /// Create Grade by Subject, Student, Level, Class
        /// </summary>
        /// <param name="subjectList"></param>
        /// <param name="studentList"></param>
        /// <param name="levelList"></param>
        /// <param name="classList"></param>
        /// <returns></returns>
        public static List<Grade> CreateGrade(List<Subject> subjectList, List<Student> studentList, List<Level> levelList, List<Class> classList)
        {
            List<Grade> listGrade = new List<Grade>();
            foreach (Level level in levelList)
            {
                foreach (Subject subject in subjectList)
                {
                    if (level.UUID.Equals(subject.Level))
                    {
                        foreach (Student student in studentList)
                        {
                            string levelUuid = Class.getLevelUuid(student.Class, classList);
                            if (level.UUID.Equals(levelUuid))
                            {
                                string uuid = Guid.NewGuid().ToString();
                                Random rnd = new Random();
                                double grade = (double)rnd.Next(20, 101) / 10;
                                listGrade.Add(new Grade(uuid, subject.UUID, student.UUID, grade));
                            }
                        }
                    }
                }
            }
            return listGrade;
        }

        public static void SaveGrades(List<Grade> gradeList, string filename)
        {
            String content = "UUID,Subject,Student,Point\n";
            foreach (Grade classObject in gradeList)
            {
                content += classObject.UUID + "," + classObject.Subject + "," + classObject.studentUUID + "," + classObject.Point.ToString() + "\n";
            }
            File.WriteAllText(@"" + filename + "/Grade.csv", content);
        }

        public override bool Equals(object obj)
        {
            return obj is Grade grade &&
                   UUID == grade.UUID &&
                   subjectUUID == grade.subjectUUID &&
                   studentUUID == grade.studentUUID &&
                   gradeOfStudent == grade.gradeOfStudent;
        }
    }
}
