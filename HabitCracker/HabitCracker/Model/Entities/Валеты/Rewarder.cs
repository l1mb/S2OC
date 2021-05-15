using System;
using System.Linq;

namespace HabitCracker.Model.Entities.Валеты
{
    public static class Rewarder
    {
        public static Tuple<decimal, string> GiveReward(int streak)
        {
            Random r = new Random();
            decimal reward;
            string message;
            if (Enumerable.Range(2, 3).Contains(streak))
            {
                reward = (decimal)(1 + r.NextDouble())
                         ;
                message = "keep going";
            }
            else if (Enumerable.Range(4, 5).Contains(streak))
            {
                reward = (decimal)(2 + r.NextDouble());
                message = "yeah, rock it";
            }
            else if (Enumerable.Range(6, 7).Contains(streak))
            {
                reward = (decimal)(3 + r.NextDouble());
                message = "wow, amazing";
            }
            else if (Enumerable.Range(7, 100).Contains(streak))
            {
                reward = (decimal)(4 + r.NextDouble());
                message = "Умничка, США в шоке :):):):)";
            }
            else
            {
                reward = 0;
                message = "grats, you are at the very beginning";
            }
            return new(reward, message);
        }
    }
}