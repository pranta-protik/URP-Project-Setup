using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace MyTools
{
    public static class Setup
    {
        public static void CreateDefaultFolders()
        {
            Folders.CreateDefault(
            "_Project",
            "Animation/Animators",
            "Animation/Clips",
            "Art/Fonts",
            "Art/Models",
            "Art/Materials",
            "Art/Sprites",
            "Art/Textures",
            "Prefabs",
            "Scenes",
            "Scripts/UI",
            "ScriptableObjects",
            "Settings"
            );

            AssetDatabase.Refresh();
        }

        public static void ImportFavouriteAssets()
        {
            // Asset store assets
            Assets.ImportAssetFromAssetStore("DOTween HOTween v2.unitypackage", "Demigiant/Editor ExtensionsAnimation");
            Assets.ImportAssetFromAssetStore("Cartoon FX Remaster Free.unitypackage", "Jean Moreno/Particle Systems");
            Assets.ImportAssetFromAssetStore("FREE Casual Game SFX Pack.unitypackage", "Dustyroom/AudioSound FX");
            Assets.ImportAssetFromAssetStore("Selection History.unitypackage", "Staggart Creations/Editor ExtensionsUtilities");

            // Local drive assets
            Assets.ImportAssetFromLocalDrive("Play Mode Save v3.9.1.unitypackage", "");
            Assets.ImportAssetFromLocalDrive("Shapes v4.2.1.unitypackage", "");
            Assets.ImportAssetFromLocalDrive("Cartoon FX Remaster R 1.2.5.unitypackage", "Particle Systems/CartoomFX");
            Assets.ImportAssetFromLocalDrive("Cartoon FX 2 Remaster R 1.2.0.unitypackage", "Particle Systems/CartoomFX");
        }

        public static void InstallFavouritePackages()
        {
            Packages.InstallPackages(new[]{
                "com.unity.2d.sprite",
                "com.unity.cinemachine",
                "com.unity.inputsystem",
                "com.unity.probuilder",
                "com.unity.recorder",
                "com.unity.splines",
                "git+com.unity.nuget.newtonsoft-json",
                "git+https://github.com/KyleBanks/scene-ref-attribute",
            });
        }

        private static class Folders
        {
            public static void CreateDefault(string root, params string[] folders)
            {
                var fullpath = Path.Combine(Application.dataPath, root);
                if (!Directory.Exists(fullpath))
                {
                    Directory.CreateDirectory(fullpath);
                }
                foreach (var folder in folders)
                {
                    CreateSubFolders(fullpath, folder);
                }
            }

            private static void CreateSubFolders(string rootPath, string folderHierarchy)
            {
                var folders = folderHierarchy.Split('/');
                var currentPath = rootPath;
                foreach (var folder in folders)
                {
                    currentPath = Path.Combine(currentPath, folder);
                    if (!Directory.Exists(currentPath))
                    {
                        Directory.CreateDirectory(currentPath);
                    }
                }
            }
        }

        private static class Assets
        {
            public static void ImportAssetFromAssetStore(string asset, string subfolder,
                string rootFolder = "C:/Users/Pranta/AppData/Roaming/Unity/Asset Store-5.x")
            {
                AssetDatabase.ImportPackage(Path.Combine(rootFolder, subfolder, asset), false);
            }

            public static void ImportAssetFromLocalDrive(string asset, string subfolder,
                string rootFolder = "E:/Study/Programming/Unity/Resource Files/Packages")
            {
                AssetDatabase.ImportPackage(Path.Combine(rootFolder, subfolder, asset), false);
            }
        }

        private static class Packages
        {
            private static AddRequest Request;
            private static Queue<string> PackagesToInstall = new();

            public static void InstallPackages(string[] packages)
            {
                foreach (var package in packages)
                {
                    PackagesToInstall.Enqueue(package);
                }

                // Start the installation of the first package
                if (PackagesToInstall.Count > 0)
                {
                    Request = Client.Add(PackagesToInstall.Dequeue());
                    EditorApplication.update += Progress;
                }
            }

            private static async void Progress()
            {
                if (Request.IsCompleted)
                {
                    if (Request.Status == StatusCode.Success)
                    {
                        Debug.Log("Installed: " + Request.Result.packageId);
                    }
                    else if (Request.Status >= StatusCode.Failure)
                    {
                        Debug.Log(Request.Error.message);
                    }

                    EditorApplication.update -= Progress;

                    // If there are more packages to install, start the next one
                    if (PackagesToInstall.Count > 0)
                    {
                        // Add delay before next package install
                        await Task.Delay(1000);
                        Request = Client.Add(PackagesToInstall.Dequeue());
                        EditorApplication.update += Progress;
                    }
                }
            }
        }
    }
}