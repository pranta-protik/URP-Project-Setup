using UnityEditor;

namespace Project
{
	[CustomEditor(typeof(LevelLoader))]
	public class LevelLoaderEditor : Editor
	{
		private SerializedProperty _totalSceneCountProperty;
		private SerializedProperty _firstLevelSceneIndexProperty;
		private SerializedProperty _dontDestroyOnLoadProperty;

		private void OnEnable()
		{
			_totalSceneCountProperty = serializedObject.FindProperty("_totalSceneCount");
			_firstLevelSceneIndexProperty = serializedObject.FindProperty("_firstLevelSceneIndex");
			_dontDestroyOnLoadProperty = serializedObject.FindProperty("_dontDestroyOnLoad");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUI.BeginDisabledGroup(true);

			_dontDestroyOnLoadProperty.boolValue = true;

			EditorGUILayout.PropertyField(_dontDestroyOnLoadProperty);

			_totalSceneCountProperty.intValue = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes).Length;

			EditorGUILayout.PropertyField(_totalSceneCountProperty);

			EditorGUI.EndDisabledGroup();

			EditorGUILayout.PropertyField(_firstLevelSceneIndexProperty);

			serializedObject.ApplyModifiedPropertiesWithoutUndo();
		}
	}
}