using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProgramManagerStudent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ProgramManagerStudent
{
    class Program
    {
        private static Dictionary<string, Array> schoolDict = new Dictionary<string, Array>();

        public static Dictionary<string, Array> SchoolDict
        {
            get
            {
                return schoolDict;
            }
        }
        public static string CLIChecker(string[] args)
        {
            String result = "";
            String help_string = @"./schoolDatabaseGenerator.exe -h
Help:
./schoolDatabaseGenerator.exe <<school_name>>: Generate a school database with number students in range 
    500 to 3000, and the largest number rooms is 100 
./schoolDatabaseGenerator.exe <<school_name>> -s <<number_students>>: Generate a school database 
    with <<number_students>> in range 500 to 3000 and the largest number rooms is 100 
./schoolDatabaseGenerator.exe <<school_name>> -r <<number_rooms>>: Generate a school database 
    with <<number_rooms>> and number students in range 500 to 3000.
./schoolDatabaseGenerator.exe <<school_name>> -s <<number_students>> -r <<number_rooms>>: 
    Generate a school database with <<number_students>> and <<number_rooms>>.";
            if (args.Length == 0)
            {
                result = help_string;
            } //end if no command argument
            else if (args.Length == 1 && args[0] == "-h")
            {
                result = help_string;
            }
            else if (args.Length > 0 && (args[0] == "-s" || args[0] == "-r"))
            {
                result += "Error: Lack of the school name\n";
            }
            else if (args.Length == 3 && args[1] == "-s")
            {
                int noOfStudents = int.Parse(args[2]);
                if (noOfStudents < 500 || noOfStudents > 3000)
                {
                    result += "Error: You must input number of students in range 500 to 3000\n";
                }
            }//end if user input number less than 500 and greater than 3000
            else if (args.Length == 3 && args[1] == "-r" && Regex.IsMatch(args[2], "\\d"))
            {
                int noOfRooms = int.Parse(args[2]);
                if (noOfRooms < 1 || noOfRooms > 100)
                {
                    result += "Error: You must input number of rooms in range 1 to 100\n";
                }
            }// end if user input out of range Room (1 - 100)
            else if (args.Length == 5 && args[1] == "-s" && args[3] == "-r" && Regex.IsMatch(args[4], "\\d"))
            {
                int noOfStudents = int.Parse(args[2]);
                if (noOfStudents < 500 || noOfStudents > 3000)
                {
                    result += "Error: You must input number of students in range 500 to 3000\n";
                }
                else
                {
                int noOfRooms = int.Parse(args[4]);
                    double avgStudentInClass = (double)noOfStudents / noOfRooms;
                    int minNoOfClasses = (int)Math.Floor(noOfStudents * 1.0 / 50);
                    int maxNoOfClasses = (int)Math.Floor(noOfStudents * 1.0 / 30);
                    if (avgStudentInClass > 50)
                    {
                        result += "Error: The number of rooms is not enough for student to study. " +
                            "You should input a number in range " + minNoOfClasses + " to " + maxNoOfClasses + "\n";
                    }else if(avgStudentInClass < 30)
                    {
                        result += "Error: The number of rooms is too much. There will be empty rooms. " +
                            "You should input a number in range " + minNoOfClasses + " to " + maxNoOfClasses + "\n";
                    }
                }
            }
            else
            {
                result += "Error: Invalid command\n";
            }
            return result;
        }// end of CLIChecker

        public static void SaveData(string filename, Dictionary<string, Array> datas)
        {
            try
            {
                String content = JsonConvert.SerializeObject(datas, Formatting.Indented);
                File.WriteAllText(filename, content);
            } //end try
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("SchoolDatabaseGenerator _ DirectoryNotFound: " + ex);
            } //end catch DirectoryNotFoundException
        }
        public static void AddDataToDictonary(List<Student> listStudent, List<Class> listClass, List<Room> listRoom, List<Level> listLevel, 
            List<Subject> listSubject, List<Teacher> listTeacher, List<Field> listField, List<Attendance> listAttendance, List<Grade> listGrade)
        {
            schoolDict.Add("Students", listStudent.ToArray());
            schoolDict.Add("Classes", listClass.ToArray());
            schoolDict.Add("Rooms", listRoom.ToArray());
            schoolDict.Add("Levels", listLevel.ToArray());
            schoolDict.Add("Subjects", listSubject.ToArray());
            schoolDict.Add("Teachers", listTeacher.ToArray());
            schoolDict.Add("Fields", listField.ToArray());
            schoolDict.Add("Attendances", listAttendance.ToArray());
            schoolDict.Add("Grades", listGrade.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                string result = CLIChecker(args);
                if (result.Contains("Error"))
                {
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine(result);
                }

                if (string.IsNullOrEmpty(result))
                {
                    List<Room> roomList = null;
                    uint numbOfStudentInSchool = 0;
                    uint numbOfRoom = 0;
                    if (args.Length >= 3 && args.Length <= 5)
                    {
                        if (args[1] == "-s")
                        {
                            numbOfStudentInSchool = uint.Parse(args[2]);
                            if (args.Length == 5)
                            {
                                if(args[3] == "-r")
                                {
                                    numbOfRoom = uint.Parse(args[4]);
                                    roomList = Room.CreateRoom(numbOfRoom, numbOfStudentInSchool);
                                }
                            }else if(args.Length == 3)
                            {
                                roomList = Room.CreateRoomByStudentNum(numbOfStudentInSchool);
                                numbOfRoom = (uint)roomList.Count;
                            }
                        }
                        if (args[1] == "-r")
                        {
                            numbOfRoom = uint.Parse(args[2]);
                            roomList = Room.CreateRoomByRoomNum(numbOfRoom);
                            numbOfStudentInSchool = Room.GetAllStudent(roomList);
                        }
                        Console.WriteLine("Succesful: You have a new school database with " + numbOfStudentInSchool + " students and " + numbOfRoom + " rooms");
                    } //end if command argument has more than 3 params
                    List<Level> levelList = Level.CreateLevel(numbOfRoom);
                    List<Class> classList = Class.CreateClass(roomList, levelList);
                    List<Student> studentList = Student.CreateStudent(classList, roomList);
                    List<Field> fieldList = Field.CreateField();
                    List<Subject> subjectList = Subject.CreateSubject(levelList, fieldList);
                    List<Teacher> teacherList = Teacher.CreateTeacher(classList, subjectList);
                    List<Attendance> attendanceList = Attendance.CreateAttendance(teacherList, subjectList, classList);
                    List<Grade> gradeList = Grade.CreateGrade(subjectList, studentList, levelList, classList);

                    Room.SaveRooms(roomList);
                    Level.SaveLevel(levelList);
                    Class.SaveClasses(classList);
                    Field.SaveField(fieldList);
                    Subject.SaveSubject(subjectList);
                    Teacher.SaveTeachers(teacherList);
                    Attendance.SaveAttendance(attendanceList);
                    AddDataToDictonary(studentList, classList, roomList, levelList, subjectList, teacherList, fieldList, attendanceList, gradeList);
                    SaveData(@"../../../" + args[0] + ".json", SchoolDict);
                    //Console.WriteLine("Succesful: You have a new school database with " + numbOfStudentInShool
                    //    + " students and " + classList.Count + " room(s)");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("ArgumentOutOfRange: " + e);
            }
            
            //Random rnd = new Random();
            //uint numbOfStudentInSchool = (uint)rnd.Next(500, 3001);
            ////Tạo Room
            //List<Room> roomList = Room.CreateRoom(numbOfStudentInSchool);
            //Room.SaveRooms(roomList);
            //Console.WriteLine("Created Room!!");

            //Tạo Level
            //uint numbOfRoom = (uint)roomList.Count();
            //List<Level> levelList = Level.CreateLevel(numbOfRoom);
            //Level.SaveLevel(levelList);
            //Console.WriteLine("Created Level!!");

            //Tao Class
            //List<Class> classList = Class.CreateClass(roomList, levelList);
            //Class.SaveClasses(classList);
            //Console.WriteLine("Created Class!!");

            //Tạo Field
            //List<Field> fieldList = Field.CreateField();
            //Field.SaveField(fieldList);
            //Console.WriteLine("Created Field!!");

            //Tạo Subject
            //List<Subject> subjectList = Subject.CreateSubject(levelList, fieldList);
            //Subject.SaveSubject(subjectList);
            //Console.WriteLine("Created Subject!!");

            //Tạo Student
            //List<Student> studentList = Student.CreateStudent(classList, roomList);
            //Student.SaveStudents(studentList);
            //Console.WriteLine("Created Student!!");

            //Tạo Grade
            //List<Grade> gradeList = Grade.CreateGrade(subjectList, studentList, levelList, classList);
            //Grade.SaveGrades(gradeList);
            //Console.WriteLine("Created Grade!!");

            //Tạo Teacher
            //List<Teacher> teacherList = Teacher.CreateTeacher(classList, subjectList);
            //Teacher.SaveTeachers(teacherList);
            //Console.WriteLine("Created Teacher!!");

            //Tạo Attendance
            //List<Attendance> attendanceList = Attendance.CreateAttendance(teacherList, subjectList, classList);
            //Attendance.SaveAttendance(attendanceList);
            //Console.WriteLine("Created Attendance!!");
            Console.ReadLine();
        }
        
    }
}