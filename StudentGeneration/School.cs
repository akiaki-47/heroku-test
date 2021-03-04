using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace StudentGeneration
{
    class School
    {
        private List<Student> _students_list;
        public School(Student[] students)
        {
            _students_list = new List<Student>(students);
        }
        public void save(string filename)
        {
            String content = "ID, Fullname\n";
            if(Path.GetExtension(filename) == ".csv")
            {
                foreach (Student student in _students_list)
                {
                    content += student.ID + ", " + student.FullName + "\n";
                }
            } else if (Path.GetExtension(filename) == ".json")
            {
                foreach (Student student in _students_list)
                {
                    content += student.ID + ", " + student.FullName + "\n";
                }
            }
            
            File.WriteAllText(filename, content);
        }
    }
}
