using MyTools.Utils;
using UnityEditor;

namespace MyTools.EditorScript.HotKeys
{
    public static class ForceSaveSceneAndProject
    {
        [MenuItem("File/Save Scene And Project %#&s")]
        public static void FunctionForceSaveSceneAndProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
            DebugUtils.Log("Saved scene and project");
        }
    }
}