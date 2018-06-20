using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayListModel.DAO
{
    internal class People
    {
        public List<Person> List { get; set; }
        public int Index { get; set; }

        public People()
        {
            List = new List<Person>();
            Index = 1;
        }
    }
}
