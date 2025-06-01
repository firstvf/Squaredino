using Assets.Game.Code.Game.Level;
using UnityEditor;
using UnityEngine;

namespace Assets.Game.Code.Editor
{
    [CustomEditor(typeof(ControlPoint))]
    public class ControlPointEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ControlPoint controlPoint = (ControlPoint)target;

            if (GUILayout.Button("Add Enemy"))
            {
                CreateAndAddEnemy(controlPoint);
            }
        }

        private void CreateAndAddEnemy(ControlPoint controlPoint)
        {
            var enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Game/Prefabs/Bot.prefab");

            if (enemyPrefab == null)
            {
                Debug.LogError("No enemy prefab at path\tAssets/Game/Prefabs/Bot.prefab");
                return;
            }

            var enemyInstance = (GameObject)PrefabUtility.InstantiatePrefab(enemyPrefab, controlPoint.transform);
            enemyInstance.transform.position = controlPoint.transform.position;
            Undo.RecordObject(controlPoint, "Add Enemy");
            EditorUtility.SetDirty(controlPoint);
        }
    }
}