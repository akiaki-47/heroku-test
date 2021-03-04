using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProgramManagerStudent
{
    class Attendance
    {
        private string uuid;
        private string teacherId;
        private string classId;
        private string subjectId;

        public string Teacher { get { return teacherId; } }
        public string Class { get { return classId; } }
        public string Subject { get { return subjectId; } }

        public string UUID { get => uuid; set => uuid = value; }

        public Attendance(string uuid, string teacherId, string classId, string subjectId)
        {
            this.uuid = uuid;
            this.teacherId = teacherId;
            this.classId = classId;
            this.subjectId = subjectId;
        }

        public static List<Attendance> CreateAttendance(List<Teacher> teacherList, List<Subject> subjectList, List<Class> classList)
        {
            List<Attendance> AttendanceList = new List<Attendance>();
            Random rnd = new Random();
            foreach(Teacher teacher in teacherList)
            {
                int[] tmp = new int[rnd.Next(4, 11)];
                string fieldUuid = teacher.Field;
                for (int i = 0; i < tmp.Length; i++)
                {
                    Class classes = classList[rnd.Next(0, classList.Count)];
                    string level = classes.Name.Substring(0, 2);
                    List<Subject> subTempList = new List<Subject>();
                    foreach(Subject sub in subjectList)
                    {
                        if (sub.Field.Equals(fieldUuid) && sub.Subject_name.Contains(level))
                        {
                            subTempList.Add(sub);
                        }
                    }
                    int index = rnd.Next(0, subTempList.Count);
                    if (subTempList.Count > 0 && tmp[index] == 0)
                    {
                        string uuid = Guid.NewGuid().ToString();
                        AttendanceList.Add(new Attendance(uuid, teacher.UUID, classes.UUID, subTempList[index].UUID));
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            return AttendanceList;
        }

        public static void SaveAttendance(List<Attendance> AttendanceList, string filename)
        {
            string content = "UUID,Subject,Class,Teacher";
            foreach (Attendance Attendance in AttendanceList)
            {
                content += "\n" + Attendance.UUID + "," + Attendance.Subject + "," + Attendance.Class + "," + Attendance.Teacher;
            }
            File.WriteAllText(@"" + filename + "/Attendance.csv", content);
        }
    }
}
