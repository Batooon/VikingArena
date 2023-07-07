using Code.Logic;
using UnityEngine;

namespace Code.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar Hp;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void UpdateHpBar() =>
            Hp.SetValue(_health.Current, _health.Max);
    }
}