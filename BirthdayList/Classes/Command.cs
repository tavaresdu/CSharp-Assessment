using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayList.Classes
{
    class Command
    {
        public CommandAction action;
        public enum Letter { N, B, L, E, R, S }

        public Command()
        {
            action = new CommandAction();
        }

        public Letter getValue(char c)
        {
            switch (c)
            {
                case 'N':
                    return Letter.N;
                case 'B':
                    return Letter.B;
                case 'L':
                    return Letter.L;
                case 'E':
                    return Letter.E;
                case 'R':
                    return Letter.R;
                case 'S':
                    return Letter.S;
                default:
                    return (Letter)(-1);
            }
        }

        public string GetDescription(Letter cc)
        {
            switch (cc)
            {
                case Letter.N:
                    return "Novo";
                case Letter.B:
                    return "Buscar";
                case Letter.L:
                    return "Listar";
                case Letter.E:
                    return "Editar";
                case Letter.R:
                    return "Remover";
                case Letter.S:
                    return "Sair";
                default:
                    return null;
            }
        }

        public Func<bool> Execute(Letter cc)
        {
            switch (cc)
            {
                case Letter.N:
                    return action.Novo;
                case Letter.B:
                    return action.Buscar;
                case Letter.L:
                    return action.Listar;
                case Letter.E:
                    return action.Editar;
                case Letter.R:
                    return action.Remover;
                case Letter.S:
                    return action.Sair;
                default:
                    return action.Init;
            }
        }
    }
}