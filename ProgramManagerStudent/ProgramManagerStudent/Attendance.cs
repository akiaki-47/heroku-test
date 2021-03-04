using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProgramManagerStudent
{
    class Attendance
    {
        private string teacherId;
        private string classId;
        private string subjectId;

        public string TeacherId { get { return teacherId; } }
        public string ClassId { get { return classId; } }
        public string SubjectId { get { return subjectId; } }

        public Attendance(string teacherId, string classId, string subjectId)
        {
            this.teacherId = teacherId;
            this.classId = classId;
            this.subjectId = subjectId;
        }

        public static List<Attendance> CreateAttendance(List<Teacher> teacherList, List<Subject> subjectList, List<Class> classList)
        {
            List<Attendance> attendanceList = new List<Attendance>();
            Random rnd = new Random();
            foreach(Teacher teacher in teacherList)
            {
                int[] tmp = new int[rnd.Next(4, 11)];
                string fieldUuid = teacher.FieldUUID;
                for (int i = 0; i < tmp.Length; i++)
                {
                    
                    Class classes = classList[rnd.Next(0, classList.Count)];
                    string level = classes.Name.Substring(0, 2);
                    List<Subject> subTempList = new List<Subject>();
                    foreach(Subject sub in subjectList)
                    {
                        if (sub.FieldUUID.Equals(fieldUuid) && sub.Subject_name.Contains(level))
                        {
                            subTempList.Add(sub);
                        }
                    }
                    int index = rnd.Next(0, subTempList.Count);
                    if (subTempList.Count > 0 && tmp[index] == 0)
                    {
                         attendanceList.Add(new Attendance(teacher.Uuid, classes.Uuid, subTempList[index].Uuid));
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            return attendanceList;
        }

        public static void SaveAttendance(List<Attendance> attendanceList)
        {
            String content = "TeacherUUID, ClassUUID, SubjectUUID";
            foreach (Attendance attendance in attendanceList)
            {
                content += "\n" + attendance.TeacherId + ", " + attendance.ClassId + ", " + attendance.SubjectId;
            }
            File.WriteAllText(@"..\..\..\Attendance.csv", content);
        }
    }
}
