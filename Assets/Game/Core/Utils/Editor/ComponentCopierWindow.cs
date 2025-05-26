using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Game.Scripts.Utils.Editor
{
    public class ComponentCopierWindow : EditorWindow
    {
        private GameObject sourceObject;
        private GameObject targetObject;

        [MenuItem("Tools/Component Copier")]
        public static void ShowWindow()
        {
            GetWindow<ComponentCopierWindow>("Component Copier");
        }

        private void OnGUI()
        {
            GUILayout.Label("Component Copier", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            sourceObject = (GameObject)EditorGUILayout.ObjectField("Source Object", sourceObject, typeof(GameObject), true);
            targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true);

            EditorGUILayout.Space();

            GUI.enabled = sourceObject != null && targetObject != null;
            if (GUILayout.Button("Copy Components"))
            {
                CopyComponents();
            }
            GUI.enabled = true;
        }

        private void CopyComponents()
        {
            if (sourceObject == null || targetObject == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign both Source and Target objects", "OK");
                return;
            }

            // Get all components from source (excluding Transform)
            var components = sourceObject.GetComponents<Component>()
                .Where(c => !(c is Transform))
                .ToList();

            int copiedCount = 0;
            foreach (var component in components)
            {
                // Skip if component already exists on target
                if (targetObject.GetComponent(component.GetType()) != null)
                {
                    continue;
                }

                // Copy component
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetObject);
                copiedCount++;
            }

            EditorUtility.DisplayDialog("Success", 
                $"Copied {copiedCount} components from {sourceObject.name} to {targetObject.name}", 
                "OK");
        }
    }
} 