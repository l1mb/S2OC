using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HabitCracker.Model.Memento
{
    internal class CareTaker
    {
        public Stack<Memento.WeekMemento> UndoMementoes = new();
        public Stack<Memento.WeekMemento> RedoMementoes = new();

        public WeekMemento GetCurrent()
        {
            return UndoMementoes.Count() != 0 ? UndoMementoes.Peek() : null;
        }

        public WeekMemento GetPrevious()
        {
            if (UndoMementoes.Count() != 0)
            {
                RedoMementoes.Push(UndoMementoes.Pop());
            }
            else
            {
                MessageBox.Show("Прошлого не существует");
            }

            return GetCurrent();
        }

        public WeekMemento GetNext()
        {
            if (RedoMementoes != null && RedoMementoes.Count() != 0)
            {
                UndoMementoes.Push(RedoMementoes.Pop());
            }
            else
            {
                MessageBox.Show("Живи настоящим");
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