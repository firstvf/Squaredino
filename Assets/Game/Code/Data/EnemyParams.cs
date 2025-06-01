using UnityEngine;

namespace Assets.Game.Code.Data
{
    [CreateAssetMenu(fileName = "new enemyParams", menuName = "ScriptableObjects/Enemy Parameters")]
    public class EnemyParams : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
    }
}