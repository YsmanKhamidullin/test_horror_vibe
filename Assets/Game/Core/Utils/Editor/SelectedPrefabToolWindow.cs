using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Object = UnityEngine.Object;

namespace Game.Scripts.Utils.Editor
{
    public class SelectedPrefabToolWindow : EditorWindow
    {
        [MenuItem("Tools/Selected Prefabs Tool")]
        public static void ShowWindow()
        {
            GetWindow<SelectedPrefabToolWindow>("Selected Prefab Tool Window");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Remove Missing Components"))
            {
                ProcessSelectedPrefabs(RemoveMissingComponents);
            }

            if (GUILayout.Button("Mark Static Shadow Caster to True"))
            {
                ProcessSelectedPrefabs(SetStaticShadowCasterTrue);
            }

            if (GUILayout.Button("Set Ray Tracing Mode to Off"))
            {
                ProcessSelectedPrefabs(SetRayTracingOff);
            }
        }

        /// <summary>
        /// Processes all selected objects that are prefab assets using the specified action.
        /// </summary>
        /// <param name="action">Action to perform on each prefab.</param>
        private void ProcessSelectedPrefabs(Action<GameObject> action)
        {
            Object[] selectedObjects = Selection.objects;
            foreach (Object obj in selectedObjects)
            {
                if (!PrefabUtility.IsPartOfPrefabAsset(obj)) continue;

                GameObject prefab = obj as GameObject;
                if (prefab == null) continue;

                action(prefab);
            }

            SaveAssets();
        }

        /// <summary>
        /// Removes missing MonoBehaviour components from all children of the prefab.
        /// </summary>
        /// <param name="prefab">The prefab to process.</param>
        private void RemoveMissingComponents(GameObject prefab)
        {
            Transform[] allTransforms = prefab.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allTransforms)
            {
                int removedCount = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(child.gameObject);
                if (removedCount > 0)
                {
                    Debug.Log($"Removed {removedCount} missing scripts from {child.gameObject.name}");
                }
            }
        }

        /// <summary>
        /// Sets staticShadowCaster property to true for each MeshRenderer component.
        /// </summary>
        /// <param name="prefab">The prefab to process.</param>
        private void SetStaticShadowCasterTrue(GameObject prefab)
        {
            MeshRenderer[] meshRenderers = prefab.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.staticShadowCaster = true;
                Debug.Log($"Static shadow caster set to true on {renderer.gameObject.name}");
            }
        }

        /// <summary>
        /// Turns off ray tracing mode for each MeshRenderer component.
        /// </summary>
        /// <param name="prefab">The prefab to process.</param>
        private void SetRayTracingOff(GameObject prefab)
        {
            MeshRenderer[] meshRenderers = prefab.GetComponentsInChildren<MeshRenderer>(true);
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.rayTracingMode = RayTracingMode.Off;
                Debug.Log($"Ray tracing mode set to off on {renderer.gameObject.name}");
            }
        }

        /// <summary>
        /// Saves and refreshes the asset database.
        /// </summary>
        private void SaveAssets()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}