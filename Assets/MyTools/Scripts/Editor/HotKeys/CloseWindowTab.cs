using UnityEditor;
using UnityEngine;

namespace MyTools.EditorScript
{
    public static class CloseWindowTab
    {
        [MenuItem("File/Close Window Tab %w")]
        private static void CloseTab()
        {
            var focusedWindow = EditorWindow.focusedWindow;

            if (focusedWindow != null)
            {
                CloseTab(focusedWindow);
            }
            else
            {
                Debug.LogWarning("No focused window found!");
            }
        }

        private static void CloseTab(EditorWindow editorWindow)
        {
            editorWindow.Close();
        }
    }
}