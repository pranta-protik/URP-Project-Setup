using UnityEditor;

namespace MyTools
{
    public static class UtilsMenu
    {
        [MenuItem("Tools/Setup/Project Setup")]
        public static void ProjectSetupWindow() => SetupWindow.InitWindow();
    }
}