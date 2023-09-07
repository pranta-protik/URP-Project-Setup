using UnityEditor;
using UnityEngine;

namespace MyTools.EditorScript
{
    public static class ForceSaveSceneAndProject
    {
        [MenuItem("File/Save Scene And Project %#&s")]
        public static void FunctionForceSaveSceneAndProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved scene and project");
        }
    }
}