using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text.Json;


namespace StudentGeneration
{
    class Student
    {
        protected string _id;
        protected string _fullname;
        public string ID { get { return _id; } }
        public string FullName { get { return _fullname; } }

        protected Student(string ID, string FullName)
        {
            _id = ID;
            _fullname = FullName;
        }

        public static Student[] Create(uint number_student)
        {
            Student[] result = new Student[number_student];
            string content = File.ReadAllText(@"..\..\..\Configure.json");
            Configure config = JsonSerializer.Deserialize<Configure>(content);

            Random rnd = new Random();
            for (uint i = 0; i < number_student; i++)
            {
                NameConfig _ = config.NameConfig;
                int last_name_index = rnd.Next(_.last_name_set.Length);
                int first_name_index = rnd.Next(_.first_name_set.Length);
                int middle_name_index = rnd.Next(_.middle_name_set.Length);
                string full_name = _.last_name_set[last_name_index] + " ";
                full_name += _.middle_name_set[middle_name_index] + " ";
                full_name += _.first_name_set[first_name_index];
                result[i] = new Student(i.ToString(), full_name);
            }
            return result;
        }
        public void print()
        {
            Console.WriteLine(_id+1 + " " + _fullname);
        }
    }
}
