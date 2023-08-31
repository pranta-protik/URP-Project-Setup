using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace MyTools
{
    public static class Setup
    {
        public static void CreateDefaultFolders(string[] folders)
        {
            Folders.CreateDefault("_Project", folders);
            AssetDatabase.Refresh();
        }

        public static void ImportAssetStoreAssets(string rootFolder, string[] assets)
        {
            foreach (string asset in assets) Assets.ImportAsset(rootFolder, asset);
        }

        public static void ImportLocalDriveAssets(string rootFolder, string[] assets)
        {
            foreach (string asset in assets) Assets.ImportAsset(rootFolder, asset);
        }

        public static void InstallUnityPackages(string[] packages) => Packages.InstallPackages(packages);
        public static void InstallOpenSources(string[] openSources) => Packages.InstallPackages(openSources);

        private static class Folders
        {
            public static void CreateDefault(string root, string[] folders)
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
            public static void ImportAsset(string rootFolder, string asset)
            {
                AssetDatabase.ImportPackage(Path.Combine(rootFolder, asset), false);
            }
        }

        private static class Packages
        {
            private static AddRequest Request;
            private static Queue<string> PackagesToInstall = new();

            public static void InstallPackages(string[] packages)
            {
                Debug.Log("Installing ... ...");

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

            private static void Progress()
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
                        Request = Client.Add(PackagesToInstall.Dequeue());
                        EditorApplication.update += Progress;
                    }
                    else
                    {
                        Debug.Log("All packages installed");
                    }
                }
            }
        }
    }
}