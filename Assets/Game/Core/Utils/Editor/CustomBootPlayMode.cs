#if UNITY_EDITOR
using Cysharp.Threading.Tasks;
using Game.Core.Architecture;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CustomBootPlayMode : Editor
{
    [InitializeOnEnterPlayMode]
    static void LoadSceneOnPlay()
    {
        EditorApplication.playModeStateChanged += LoadCustomScene;
    }

    private static void LoadCustomScene(PlayModeStateChange state)
    {
        EditorApplication.playModeStateChanged -= LoadCustomScene;
        var sceneName = SceneManager.GetActiveScene().name;
        if (state == PlayModeStateChange.EnteredPlayMode && sceneName != "Boot")
        {
            Project.Initialize().Forget();
        }
    }
}

#endif