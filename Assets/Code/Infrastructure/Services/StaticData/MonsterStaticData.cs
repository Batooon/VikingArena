using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "Monster static data", menuName = "Static Data/Enemy")]
    public class MonsterStaticData : ScriptableObject
    {
        public GameObject Prefab;

        public MonsterTypeId TypeId;

        public int Hp;
        public float MoveSpeed;
        public float Damage;
        public float EffectiveDistance;
        public float Cleavage;
    }
}