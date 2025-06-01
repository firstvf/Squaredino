using UnityEngine;

namespace Assets.Game.Code.Data
{
    [CreateAssetMenu(fileName = "new bulletParams", menuName = "ScriptableObjects/Bullet Parameters")]
    public class BulletParams : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int MinDamage { get; private set; }
        [field: SerializeField] public int MaxDamage { get; private set; }
        [field: SerializeField] public float Lifetime { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }        
    }
}