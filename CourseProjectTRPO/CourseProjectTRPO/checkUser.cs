using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectTRPO
{
    public class checkUser
    {
        //some add
        public string Surname { get; set; } 
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }

        public checkUser()
        {

            Surname = string.Empty;
            Name = string.Empty;
            Patronymic = string.Empty;
            Post = string.Empty;
        }
        public checkUser(string post, string surname, string name, string patronymic)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Post = post;
        }
    }
}
