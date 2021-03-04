using ProgramManagerStudent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ProgramManagerStudent
{
    class Field
    {
        private string uuid;
        private string name;

        public string UUID { get { return uuid; } }
        public string Name { get { return name; } }

        public Field(string uuid, string name)
        {
            this.uuid = uuid;
            this.name = name;
        }

        public Field()
        {
        }

        public static List<Field> CreateField()
        {
            List<Field> fieldList = new List<Field>();
            string configContent = File.ReadAllText(@"Configure.json");
            Configure config = JsonSerializer.Deserialize<Configure>(configContent);
            FieldConfigure fieldConfig = config.FieldConfigure;
            
            for (int i = 0; i< fieldConfig.field_set.Length; i++)
            {
                string uuid = Guid.NewGuid().ToString();
                string fieldName = fieldConfig.field_set[i];
                fieldList.Add(new Field(uuid, fieldName));
            }
            return fieldList;
        }
        public static void SaveField(List<Field> fieldList, string filename)
        {
            String content = "UUID,Name";
            foreach (Field field in fieldList)
            {
                content += "\n" + field.UUID + "," + field.Name;
            }
            File.WriteAllText(@"" + filename + "/Field.csv", content);
        }
    }
}
