using UnityEngine;

namespace Assets.Game.Code.Data
{
    [CreateAssetMenu(fileName = "new cameraParams", menuName = "ScriptableObjects/Camera Parameters")]
    public class CameraParams : ScriptableObject
    {
        [field: SerializeField] public float Height { get; private set; }
        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public float RotationLagSpeed { get; private set; }
        [field: SerializeField] public float PositionSmoothSpeed { get; private set; }
        [field: SerializeField] public bool CameraShake { get; private set; }
    }
}