using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectTRPO
{
    public class checkUser
    {
        public string Surname { get; set; } 
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }
        public int PostId { get; set; }

        public checkUser(string post, string surname, string name, string patronymic, int id)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Post = post;
            PostId = id;
        }
    }
}
