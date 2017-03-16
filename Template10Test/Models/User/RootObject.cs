using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template10Test.Models.User
{
    public class RootObject
    {
        public int _Id { get; set; }
        public string Bio { get; set; }
        public string Created_at { get; set; }
        public string Display_name { get; set; }
        public string Email { get; set; }
        public bool Email_verified { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public Notifications Notifications { get; set; }
        public bool Partnered { get; set; }
        public bool Twitter_connected { get; set; }
        public string Type { get; set; }
        public string Updated_at { get; set; }
    }
}
