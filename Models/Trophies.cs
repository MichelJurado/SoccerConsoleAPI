using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedJSON2.Models
{


    public class Rootobject2
    {
        
        public Parameters2 parameters { get; set; }
        
        
        
        public Response2[] response { get; set; }
    }

    public class Parameters2
    {
        public string player { get; set; }
    }

    

    public class Response2
    {
        public string league { get; set; }
        public string country { get; set; }
        public string season { get; set; }
        public string place { get; set; }
    }


}
