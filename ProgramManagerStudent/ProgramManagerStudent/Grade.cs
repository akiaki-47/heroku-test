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
        private string subjectUUID;
        private string studentUUID;
        private double gradeOfStudent;

        public Grade(string subjectUUID, string studentUUID, double gradeOfStudent)
        {
            this.subjectUUID = subjectUUID;
            this.studentUUID = studentUUID;
            this.gradeOfStudent = gradeOfStudent;
        }

        public double GradeOfStudent { get { return gradeOfStudent; } }
        public string StudentUUID { get { return studentUUID; } }
        public string SubjectUUID { get { return subjectUUID; } }

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
                    if (level.Uuid.Equals(subject.LevelUUID))
                    {
                        foreach (Student student in studentList)
                        {
                            string levelUuid = Class.getLevelUuid(student.ClassID, classList);
                            if (level.Uuid.Equals(levelUuid))
                            {
                                Random rnd = new Random();
                                double grade = (double)rnd.Next(20, 101) / 10;
                                listGrade.Add(new Grade(subject.Uuid, student.ID, grade));
                            }
                        }
                    }
                }
            }
            return listGrade;
        }

        public static void SaveGrades(List<Grade> gradeList)
        {
            String content = "SubjectUUID, StudentUUID, Grade\n";
            foreach (Grade classObject in gradeList)
            {
                content += classObject.SubjectUUID + ", " + classObject.studentUUID + ", " + classObject.GradeOfStudent + "\n";
            }
            File.WriteAllText(@"..\..\..\Grade.csv", content);
        }
    }
}
