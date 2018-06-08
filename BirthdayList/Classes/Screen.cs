using BirthdayList.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BirthdayList
{
    class Screen
    {
        private ScreenAction action;
        private Command command;

        public Screen()
        {
            action = new ScreenAction();
            command = new Command();
        }

        public void Init()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(string.Format("Pessoas que fazem aniversário hoje ({0}):", now.ToString("dd/MM")));
            foreach (var person in action.getTodayBirthdays())
            {
                Console.WriteLine(string.Format("> {0} {1}", person.Name, person.Surname));
            }
        }

        public void ShowCommands()
        {
            foreach (var letter in Enum.GetValues(typeof(Command.Letter)))
            {
                Console.WriteLine(String.Format("{0} - {1}", letter, command.GetDescription((Command.Letter)letter)));
            }
        }
    }
}
