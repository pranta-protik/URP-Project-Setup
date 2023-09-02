using UnityEditor;

namespace MyTools
{
    public static class SetupMenu
    {
        [MenuItem("Tools/Setup/Project Setup")]
        public static void ProjectSetup() => ProjectSetupWindow.InitWindow();

        [MenuItem("Tools/Setup/Scene Setup")]
        public static void SceneSetup() => SceneSetupWindow.InitWindow();
    }
}