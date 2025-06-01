using UnityEngine;

namespace Assets.Game.Code.Data
{
    [CreateAssetMenu(fileName = "new playerParams", menuName = "ScriptableObjects/Player Parameters")]
    public class PlayerParams : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
    }
}