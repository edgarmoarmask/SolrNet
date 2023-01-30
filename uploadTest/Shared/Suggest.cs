using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace uploadTest.Shared
{

    public class Suggest
    {
        public int numFound { get; set; }
        public Suggestion[] suggestions { get; set; }
    }

    public class Suggestion
    {
        public string term { get; set; }
        public long weight { get; set; }
        public string payload { get; set; }
    }

}
