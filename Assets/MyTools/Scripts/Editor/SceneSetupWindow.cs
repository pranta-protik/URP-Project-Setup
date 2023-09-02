using UnityEditor;
using UnityEngine;

namespace MyTools
{
    public class SceneSetupWindow : EditorWindow
    {
        private const string WINDOW_TITLE = "Scene Setup";
        private const float WINDOW_WIDTH = 500f;
        private const float WINDOW_HEIGHT = 500f;
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

            EditorGUILayout.EndVertical();

            if (_sceneSetupWindow) _sceneSetupWindow.Repaint();
        }

        public static void InitWindow()
        {
            _sceneSetupWindow = GetWindow<SceneSetupWindow>(true, WINDOW_TITLE, true);
            _sceneSetupWindow.minSize = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
            _sceneSetupWindow.Show();
        }
    }
}