using UnityEditor;

namespace MyTools
{
    public static class UtilsMenu
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders() => Setup.CreateDefaultFolders();

        [MenuItem("Tools/Setup/Import Favourite Assets")]
        public static void ImportFavouriteAssets() => Setup.ImportFavouriteAssets();

        [MenuItem("Tools/Setup/Install Favourite Packages")]
        public static void InstallFavouritePackages() => Setup.InstallFavouritePackages();
    }
}