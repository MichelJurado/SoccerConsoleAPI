using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedJSON2.Models
{

    public class Rootobject1
    {
        public string get { get; set; }
        public Parameters parameters { get; set; }
        public object[] errors { get; set; }
        public int results { get; set; }
        public Paging paging { get; set; }
        public Response1[] response { get; set; }
    }

    public class Parameters1
    {
        public string team { get; set; }
    }

    public class Paging1
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class Response1
    {
        public Team1 team { get; set; }
        public Player1[] players { get; set; }
    }

    public class Team1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }

    public class Player1
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public int? number { get; set; }
        public string position { get; set; }
        public string photo { get; set; }
    }


}
