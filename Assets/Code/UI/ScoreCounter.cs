using Code.Infrastructure.Services.Progress;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        public string Template;
        public TextMeshProUGUI Counter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.Score.Changed += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            Counter.text = string.Format(Template, _worldData.Score.Amount);
        }
    }
}