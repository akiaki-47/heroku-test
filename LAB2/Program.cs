using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProgramManagerStudent;

namespace LAB2
{
    class Program
    {
        private static List<string> schoolDict = new List<string>();

        public static List<string> SchoolDict { get { return schoolDict; } }

        public static Dictionary<string, string> CLIChecker(string[] args)
        {
            Dictionary<string, string> commandDict = new Dictionary<string, string>();
            String result = "";
            String help_string = @"Help:
        ./exportJson2Database -j <<json_file>> -d <<database_name>>";
            if (args.Length == 0 || (args.Length == 1 && args[0] == "-h"))
            {
                result = help_string;
            }
            else if (args.Length > 0)
            {
                if (args.Length == 1 && args[0] == "-j")
                {
                    result = "Error: Lack of the file name and database name";
                }
                else if (args[0] == "-j" && args[1] == "-d")
                {
                    result = "Error: Lack of the file name.";
                }
                else if (args[0] == "-j" && string.IsNullOrEmpty(args[1]))
                {
                    result = "Error: Invalid file name.";
                }
                else if (args[2] == "-d" && string.IsNullOrEmpty(args[3]))
                {
                    result = "Error: Invalid database name.";
                }
            }
            else if (args.Length == 3)
            {
                if (args[0] == "-j" && args[2] == "-d")
                {
                    result = "Error: Lack of the database name.";
                }
            }
            else
            {
                result = "Error: Invalid command.";
            }
            commandDict.Add("Result", result);
            commandDict.Add("File name", args[1]);
            commandDict.Add("Database name", args[3]);
            return commandDict;
        }

        static void Main(string[] args)
        {
            Dictionary<string, string> cmdDic = CLIChecker(args);
            if (cmdDic["Result"].Contains("Error"))
            {
                Console.WriteLine(cmdDic["Result"]);
            }
            else
            {
                Console.WriteLine(cmdDic["Result"]);
            }
            string fileName = cmdDic["File name"];
            string databaseName = cmdDic["Database name"];
            DBUtils util = new DBUtils();
            SqlConnection conn = util.getConnection();
            conn.Open();
            if (conn != null)
            {
                bool isExisted = false;
                string data;
                isExisted = util.checkDBExisted(databaseName);
                if (!isExisted)
                {
                    util.createDB(databaseName);
                }
                isExisted = util.checkTableExisted(databaseName, "Room");
                if (!isExisted)
                {
                    data = "UUID varchar(255) NOT NULL, No int NOT NULL PRIMARY KEY(UUID)";
                    util.createTable(databaseName, "Room", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Level");
                if (!isExisted)
                {
                    data = "UUID varchar(255) NOT NULL, Name varchar(10) NOT NULL PRIMARY KEY(UUID)";
                    util.createTable(databaseName, "Level", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Class");
                if (!isExisted)
                {
                    data = "UUID varchar(255) NOT NULL, Name nvarchar(100) NOT NULL, Room varchar(255) NOT NULL, Level varchar(255) NOT NULL PRIMARY KEY(UUID)";
                    util.createTable(databaseName, "Class", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Student");
                if (!isExisted)
                {
                    data = "UUID varchar(255) NOT NULL, Name nvarchar(100) NOT NULL, Birthday date NOT NULL, Gender bit NOT NULL, Class varchar(255) NOT NULL PRIMARY KEY(UUID)";
                    util.createTable(databaseName, "Student", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Field");
                if (!isExisted)
                {
                    data = "UUID varchar(255) NOT NULL, Name varchar(50) NOT NULL PRIMARY KEY(UUID)";
                    util.createTable(databaseName, "Field", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Teacher");
                if (!isExisted)
                {
                    data = "UUID varchar(255) NOT NULL, Name varchar(50)  NOT NULL, Gender bit NOT NULL, Field varchar(255) NOT NULL PRIMARY KEY(UUID)";
                    util.createTable(databaseName, "Teacher", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Subject");
                if (!isExisted)
                {
                    data = "UUID varchar(255) NOT NULL, Level varchar(255) NOT NULL, Field varchar(255) NOT NULL PRIMARY KEY(UUID)";
                    util.createTable(databaseName, "Subject", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Attendance");
                if (!isExisted)
                {
                    data = "Teacher varchar(255) NOT NULL, Class varchar(255) NOT NULL, Subject varchar(255) NOT NULL";
                    util.createTable(databaseName, "Attendance", data);
                }

                isExisted = util.checkTableExisted(databaseName, "Grade");
                if (!isExisted)
                {
                    data = "Subject varchar(255) NOT NULL, Student varchar(255) NOT NULL, Point int NOT NULL PRIMARY KEY(Subject, Student)";
                    util.createTable(databaseName, "Grade", data);
                }
                util.removeTableData(databaseName, "Room");
                util.removeTableData(databaseName, "Level");
                util.removeTableData(databaseName, "Class");
                util.removeTableData(databaseName, "Attendance");
                util.removeTableData(databaseName, "Field");
                util.removeTableData(databaseName, "Grade");
                util.removeTableData(databaseName, "Teacher");
                util.removeTableData(databaseName, "Student");
                util.removeTableData(databaseName, "Subject");

                Room.SaveToDB(fileName, databaseName);
                Level.SaveToDB(fileName, databaseName);
                Class.SaveToDB(fileName, databaseName);
                Attendance.SaveToDB(fileName, databaseName);
                Field.SaveToDB(fileName, databaseName);
                Grade.SaveToDB(fileName, databaseName);
                Teacher.SaveToDB(fileName, databaseName);
                Student.SaveToDB(fileName, databaseName);
                Subject.SaveToDB(fileName, databaseName);

                util.addFK(databaseName, "Class", "[Level]", "Level", "UUID", "FK_Class_Level");
                util.addFK(databaseName, "Student", "Class", "Class", "UUID", "FK_Class_Student");
                
                util.addFK(databaseName, "Subject", "[Level]", "Level", "UUID", "FK_Subject_Level");
                util.addFK(databaseName, "Subject", "Field", "Field", "UUID", "FK_Subject_Field");

                util.addFK(databaseName, "Attendance", "Class", "Class", "UUID", "FK_Class_Attendance");
                util.addFK(databaseName, "Attendance", "Subject", "Subject", "UUID", "FK_Subject_Attendance");
                util.addFK(databaseName, "Attendance", "Teacher", "Teacher", "UUID", "FK_Teacher_Attendance");

                util.addFK(databaseName, "Teacher", "Field", "Field", "UUID", "FK_Field_Teacher");

                util.addFK(databaseName, "Grade", "Subject", "Subject", "UUID", "FK_Subject_Grade");
                util.addFK(databaseName, "Grade", "Student", "Student", "UUID", "FK_Student_Grade");

                //ALTER TABLE[dbo].[Class] ADD CONSTRAINT[FK_Class_Level] FOREIGN KEY([Level])
                //REFERENCES[dbo].[Level]([UUID])
                //sql = "ALTER TABLE " + databaseName + ".dbo.Class ADD CONSTRAINT FK_Class_Room FOREIGN KEY (Room) REFERENCES " + databaseName + ".dbo.Room(UUID)";
                //command = new SqlCommand(sql, conn);
                //command.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("Succesful: You've exported " + args[1] + " to " + args[3] + " in SQLSERVER");
            }
        }
    }
}
