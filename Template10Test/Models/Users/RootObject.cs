using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template10Test.Models.Users
{
    public class RootObject
    {
        public List<Follow> follows { get; set; }
        public int _total { get; set; }
    }
}
