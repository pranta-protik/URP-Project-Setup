using UnityEditor;
using UnityEngine;

namespace MyTools
{
    public class SceneSetupWindow : EditorWindow
    {
        private const string WINDOW_TITLE = "Scene Setup";
        private const float WINDOW_WIDTH = 500f;
        private const float WINDOW_HEIGHT = 500f;
        private const float BUTTON_HEIGHT = 32f;
        private const float VERTICAL_SPACE = 10;
        private static SceneSetupWindow _sceneSetupWindow;
        private static GUIStyle _titleLabelStyle;

        private void OnEnable()
        {
            _titleLabelStyle = new GUIStyle
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            _titleLabelStyle.normal.textColor = Color.white;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.Space(VERTICAL_SPACE);

            SetupPersistentScene();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            SetupSplashScene();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.EndVertical();

            if (_sceneSetupWindow) _sceneSetupWindow.Repaint();
        }

        private static void SetupPersistentScene()
        {
            GUILayout.Label("Setup Persistent Scene", _titleLabelStyle);

            EditorGUILayout.BeginHorizontal();

            var relativePath = "_Project/Scenes/Persistent.unity";

            if (GUILayout.Button("Setup Scene", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                SceneSetup.SetupPersistentScene(relativePath);
            }

            if (GUILayout.Button("Open Scene", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                SceneSetup.TryOpenScene(relativePath);
            }

            EditorGUILayout.EndHorizontal();
        }

        private static void SetupSplashScene()
        {
            GUILayout.Label("Setup Splash Scene", _titleLabelStyle);

            EditorGUILayout.BeginHorizontal();

            var relativePath = "_Project/Scenes/Splash.unity";

            if (GUILayout.Button("Setup Scene", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                SceneSetup.SetupSplashScene(relativePath);
            }

            if (GUILayout.Button("Open Scene", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                SceneSetup.TryOpenScene(relativePath);
            }

            EditorGUILayout.EndHorizontal();
        }

        public static void InitWindow()
        {
            _sceneSetupWindow = GetWindow<SceneSetupWindow>(true, WINDOW_TITLE, true);
            _sceneSetupWindow.minSize = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
            _sceneSetupWindow.Show();
        }
    }
}