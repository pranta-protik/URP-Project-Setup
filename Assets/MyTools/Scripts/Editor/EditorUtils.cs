using UnityEditor;

namespace MyTools
{
    public static class EditorUtils
    {
        public static void DisplayDialogBox(string title, string message) => EditorUtility.DisplayDialog(title, message, "OK");

        public static bool DisplayDialogBoxWithOptions(string title, string message) => EditorUtility.DisplayDialog(title, message, "Yes", "No");
    }
}