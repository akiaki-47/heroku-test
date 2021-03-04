using System;
using System.Collections.Generic;
using System.Text;

namespace StudentGeneration
{
    class Configure
    {
        public NameConfig NameConfig { get; set; }
    }
    class NameConfig
    {
        public string[] last_name_set { get; set; }
        public string[] middle_name_set { get; set; }
        public string[] first_name_set { get; set; }
    }
}
