using Assets.Game.Code.Game;
using UnityEditor;
using UnityEngine;

namespace Assets.Game.Code.Editor
{

    [CustomEditor(typeof(ControlPoint))]
    public class ControlPointEditor : UnityEditor.Editor
    {
        [SerializeField] private Enemy _enemy;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ControlPoint controlPoint = (ControlPoint)target;

            if (GUILayout.Button("Add Enemy"))
            {
                CreateAndAddEnemy(controlPoint);
            }
        }

        private void CreateAndAddEnemy(ControlPoint checkpoint)
        {
            GameObject enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy.prefab");
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy prefab not found at Assets/Prefabs/Enemy.prefab");
                return;
            }

            GameObject enemyInstance = (GameObject)PrefabUtility.InstantiatePrefab(enemyPrefab);
            enemyInstance.transform.position = checkpoint.transform.position;

            Enemy enemyComponent = enemyInstance.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                Undo.RecordObject(checkpoint, "Add Enemy");
              //  checkpoint.AddEnemy(enemyComponent);
                EditorUtility.SetDirty(checkpoint);
            }
            else
            {
                Debug.LogError("Enemy prefab does not have an Enemy component");
            }
        }
    }
}