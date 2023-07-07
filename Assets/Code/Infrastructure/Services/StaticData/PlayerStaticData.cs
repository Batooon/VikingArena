using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "Player static data", menuName = "Static Data/Player", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        public GameObject Prefab;

        public int Hp;
        public int Damage;
    }
}