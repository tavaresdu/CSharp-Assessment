using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayListBO
{
    internal interface ICommandAction
    {
        bool Init();
        bool New();
        bool Find();
        bool ListPeople();
        bool Edit();
        bool Remove();
        bool Exit();
    }
}
