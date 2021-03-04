using System;
using System.Collections.Generic;

namespace LAB3
{
    class School
    {
        public Student[] Student { get; set; }

        public Class[] Class { get; set; }

        public Room[] Room { get; set; }

        public Level[] Level { get; set; }

        public Subject[] Subject { get; set; }

        public Field[] Field { get; set; }

        public Teacher[] Teacher { get; set; }

        public Attendance[] Attendance { get; set; }

        public Grade[] Grade { get; set; }
    }
    public class Student
    {
        private bool gender;
        private string uuid;
        private string name;
        private DateTime birthday;
        private string classID;
        /// <summary>
        /// get id
        /// </summary>
        public string UUID { get => uuid; set => uuid = value; }
        /// <summary>
        /// get Fullname
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// get birthday
        /// </summary>
        public DateTime Birthday { get => birthday; set => birthday = value; }


        /// <summary>
        /// get ClassID
        /// </summary>
        public string Class { get => classID; set => classID = value; }

        /// <summary>
        /// Get Gender
        /// </summary>
        public bool Gender { get => gender; set => gender = value; }


        /// <summary>
        /// Constructor of student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullname"></param>
        /// <param name="birthday"></param>
        /// <param name="isMale"></param>
        /// <param name="classID"></param>
        public Student(string uuid, string fullname, DateTime birthday, bool gender, string classID)
        {
            this.UUID = uuid;
            this.Name = fullname;
            this.Birthday = birthday;
            this.Gender = gender;
            this.Class = classID;
        }

        public Student()
        {
        }
    }
    public class Class
        {
        private string uuid;
        private string level;
        private string room;
        private string name;

        /// <summary>
        /// Create constructor of Class
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="level"></param>
        /// <param name="room"></param>
        /// <param name="name"></param>
        

        public Class()
        {
        }
        public Class(string uuid, string level, string room, string name)
        {
            this.uuid = uuid;
            this.level = level;
            this.room = room;
            this.name = name;
        }
        public string Uuid { get => uuid; set => uuid = value; }
        public string Level { get => level; set => level = value; }
        public string Room { get => room; set => room = value; }
        public string Name { get => name; set => name = value; }
    }
    public class Room
    {
        /// <summary>
        /// generate uuid, no, numbOfStudent
        /// </summary>
        private string uuid;
        private uint no;
        private uint numbOfStudent;
        private string classes;

        /// <value>uuid</value>
        public string UUID { get => uuid; set => uuid = value; }


        /// <value>getNo</value>
        public uint No { get => no; set => no = value; }

        /// <value>Get NumbOfStudent </value>
        public uint NumbOfStudent { get { return numbOfStudent; } set { numbOfStudent = value; } }

        public string Class { get => classes; set => classes = value; }

        /// <summary>
        /// Generate Constructor Room
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="no"></param>
        /// <param name="numbOfStudent"></param>
        public Room(string uuid, uint no, uint numbOfStudent)
        {
            this.uuid = uuid;
            this.no = no;
            this.numbOfStudent = numbOfStudent;
        }

        public Room(string classID)
        {
            Class = classID;
        }
        /// <summary>
        /// Generate Room without parameter
        /// </summary> 
        public Room()
        {
        }
    }
    public class Level
    {
        /// <summary>
        /// Generate method create a level.
        /// </summary>
        private string uuid;
        private string name;
        private uint numbOfClass;

        /// <value>Get uuid</value>
        public string UUID { get => uuid; set => uuid = value; }
        /// <value>Get Name</value>
        public string Name { get => name; set => name = value; }
        /// <value>Get NumbOfClass</value>
        public uint NumbOfClass { get => numbOfClass; set => numbOfClass = value; }


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
    }
    public class Grade
    {
        private string uuid;
        private string subject;
        private string student;
        private double gradeOfStudent;

        public Grade(string uuid, string subject, string student, double gradeOfStudent)
        {
            this.UUID = uuid;
            this.subject = subject;
            this.student = student;
            this.gradeOfStudent = gradeOfStudent;
        }

        public string UUID { get => uuid; set => uuid = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Student { get => student; set => student = value; }
        public double Point { get => gradeOfStudent; set => gradeOfStudent = value; }
    }
    public class Attendance
    {
        private string uuid;
        private string teacher;
        private string classes;
        private string subject;

        public string UUID { get => uuid; set => uuid = value; }
        public string Teacher { get => teacher; set => teacher = value; }
        public string Class { get => classes; set => classes = value; }
        public string Subject { get => subject; set => subject = value; }

        public Attendance(string uuid, string teacher, string classes, string subject)
        {
            this.uuid = uuid;
            this.teacher = teacher;
            this.classes = classes;
            this.subject = subject;
        }
    }
    public class Subject
    {
        private string uuid;
        private string level;
        private string field;
        private string subject_name;

        public string Field { get { return field; } }
        public string Level { get { return level; } }
        public string UUID { get { return uuid; } }
        public string Subject_name { get { return subject_name; } }
        public Subject(string uuid, string level, string field, string subject_name)
        {
            this.uuid = uuid;
            this.level = level;
            this.field = field;
            this.subject_name = subject_name;
        }
    }
    public class Field
    {
        private string uuid;
        private string name;

        public string Uuid { get => uuid; set => uuid = value; }
        public string Name { get => name; set => name = value; }

        public Field(string uuid, string name)
        {
            this.uuid = uuid;
            this.name = name;
        }

        public Field()
        {
        }
    }
    public class Teacher
    {
        private string uuid;
        private string name;
        private bool gender;
        private string field;

        public string UUID { get { return uuid; } }
        public string Name { get { return name; } }
        public bool Gender { get { return gender; } }
        public string Field { get { return field; } }

        public Teacher(string uuid, string name, bool gender, string field)
        {
            this.uuid = uuid;
            this.name = name;
            this.gender = gender;
            this.field = field;
        }
    }
}