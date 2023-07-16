using System;
using Code.Logic;

namespace Code.Hero
{
    public static class HeroEventsBus
    {
        public static event Action Died;
        public static event Action<IHealth> HealthChanged;
        public static event Action Hit;

        public static void FireHeroDiedEvent() => 
            Died?.Invoke();

        public static void FireHealthChanged(IHealth health) => 
            HealthChanged?.Invoke(health);

        public static void FireGotHit() => 
            Hit?.Invoke();
    }
}