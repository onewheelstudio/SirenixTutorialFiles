
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEditor.SceneManagement;

    public class HelpfulButtons : OdinEditorWindow
    {
        [MenuItem("Tools/Helpful Buttons")]
        private static void OpenWindow()
        {
            GetWindow<HelpfulButtons>().Show();
        }

        [ButtonGroup]
        private void StartScene()
        {
            LoadScene("Assets/Scenes/AlphaStartScene.unity");
        }

        [ButtonGroup]
        private void GameScene()
        {
            LoadScene("Assets/Scenes/Game Scene.unity");
        }

        [ButtonGroup]
        private void TestScene()
        {
            LoadScene("Assets/Scenes/Testing/Test Scene.unity");
        }

        private void LoadScene(string scenePath)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
        }

        [ButtonGroup]
        private void PatchNotes()
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/Prefabs/Patch Notes.asset");
        }
    }


