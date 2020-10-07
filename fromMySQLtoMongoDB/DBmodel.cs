using System;
using System.Collections.Generic;
using System.Text;

namespace fromMySQLtoMongoDB
{
    public class DBmodel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool is_study { get; set; }
        public DateTime birthday { get; set; }
    }
}
