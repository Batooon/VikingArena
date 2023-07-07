using System;

namespace Code.Infrastructure.Services.Progress
{
    [Serializable]
    public class Score
    {
        public int Amount;
        public event Action Changed;

        public void Increase(int amount)
        {
            Amount += amount;
            Changed?.Invoke();
        }
    }
}