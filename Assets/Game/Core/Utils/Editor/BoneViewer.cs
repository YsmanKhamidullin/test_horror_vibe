using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Utils.Editor
{
    public class BoneViewer : EditorWindow
    {
        // The currently selected SkinnedMeshRenderer in the scene.
        private SkinnedMeshRenderer selectedRenderer;

        // The size of the sphere drawn at each bone's position.
        private const float sphereSize = 0.05f;

        [MenuItem("Tools/_Windows/Bone Viewer")]
        public static void ShowWindow()
        {
            BoneViewer window = GetWindow<BoneViewer>("Bone Viewer");
            window.Show();
        }

        private void OnEnable()
        {
            // Listen to selection changes in the editor.
            Selection.selectionChanged += OnSelectionChanged;
            SceneView.duringSceneGui += OnSceneGUI;
            UpdateSelectedRenderer();
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= OnSelectionChanged;
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSelectionChanged()
        {
            UpdateSelectedRenderer();
            Repaint();
        }

        private void UpdateSelectedRenderer()
        {
            // Try to set selectedRenderer based on current selection.
            if (Selection.activeGameObject)
            {
                selectedRenderer = Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>();
            }
            else
            {
                selectedRenderer = null;
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("Bone Viewer Tool", EditorStyles.boldLabel);
            if (selectedRenderer == null)
            {
                EditorGUILayout.HelpBox("Select a GameObject with a SkinnedMeshRenderer component.", MessageType.Info);
            }
            else
            {
                EditorGUILayout.LabelField("Selected SkinnedMeshRenderer", selectedRenderer.name);
                if (selectedRenderer.bones != null)
                {
                    EditorGUILayout.LabelField("Bone Count", selectedRenderer.bones.Length.ToString());
                }
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Repaint Scene"))
            {
                SceneView.RepaintAll();
            }
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (selectedRenderer == null || selectedRenderer.bones == null)
                return;

            // Set the drawing color
            Handles.color = Color.green;
            foreach (Transform bone in selectedRenderer.bones)
            {
                if (bone == null)
                    continue;

                // Draw a small sphere at the bone's position.
                Handles.SphereHandleCap(0, bone.position, Quaternion.identity, sphereSize, EventType.Repaint);

                // Optionally, draw a line to the bone's parent transform if valid.
                if (bone.parent != null)
                {
                    Handles.DrawLine(bone.position, bone.parent.position);
                }

                // Optionally, display the bone's name.
                Handles.Label(bone.position + Vector3.up * 0.1f, bone.name);
            }
        }
    }
}
