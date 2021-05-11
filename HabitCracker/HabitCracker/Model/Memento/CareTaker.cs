using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HabitCracker.Model.Memento;

namespace HabitCracker.Model.Memento
{
    internal class CareTaker
    {
        public Stack<Memento.WeekMemento> UndoMementoes = new();
        public Stack<Memento.WeekMemento> RedoMementoes = new();

        public WeekMemento GetCurrent()
        {
            if (UndoMementoes.Count() != 0)
            {
                return UndoMementoes.Peek();
            }
            else return null;
        }

        public WeekMemento GetPrevious()
        {
            if (UndoMementoes.Count() != 0)
            {
                RedoMementoes.Push(UndoMementoes.Pop());
            }
            else
            {
                MessageBox.Show("Не лезь бля");
            }

            return GetCurrent();
        }

        public WeekMemento GetNext()
        {
            if (RedoMementoes.Count() != 0)
            {
                UndoMementoes.Push(RedoMementoes.Pop());
            }
            else
            {
                MessageBox.Show("Не лезь бля");
            }

            return GetCurrent();
        }

        public void Save(WeekMemento memento)
        {
            UndoMementoes.Push(memento);
        }

        public void ReverseUndoMementoes()
        {
            var tempStack = new Stack<WeekMemento>();
            while (UndoMementoes.Count != 0)
            {
                tempStack.Push(UndoMementoes.Pop());
            }

            UndoMementoes = tempStack;
        }
    }
}