using UnityEditor;

namespace Project
{
    [CustomEditor(typeof(Initializer))]
    public class InitializerEditor : Editor
    {
        private SerializedProperty _totalSceneCountProperty;
        private SerializedProperty _firstLevelSceneIndexProperty;

        private void OnEnable()
        {
            _totalSceneCountProperty = serializedObject.FindProperty("_totalSceneCount");
            _firstLevelSceneIndexProperty = serializedObject.FindProperty("_firstLevelSceneIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _totalSceneCountProperty.intValue = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes).Length;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_totalSceneCountProperty);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(_firstLevelSceneIndexProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
