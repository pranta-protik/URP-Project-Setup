using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace MyTools
{
    public class ProjectSetupWindow : EditorWindow
    {
        private const string WINDOW_TITLE = "Project Setup";
        private const float WINDOW_WIDTH = 500f;
        private const float WINDOW_HEIGHT = 880f;
        private const float BUTTON_WIDTH = 100f;
        private const float BUTTON_HEIGHT = 32f;
        private const float SCROLL_VIEW_WIDTH = 500f;
        private const float SCROLL_VIEW_HEIGHT = 65f;
        private const float VERTICAL_SPACE = 10;
        private static ProjectSetupWindow _projectSetupWindow;
        private static GUIStyle _titleLabelStyle;
        private static Vector2 _scrollPosForDefaultFolders;
        private static Vector2 _scrollPosForDefaultScenes;
        private static Vector2 _scrollPosForUnityPackages;
        private static Vector2 _scrollPosForOpenSources;
        private static Vector2 _scrollPosForAssetStoreAssets;
        private static Vector2 _scrollPosForLocalDriveAssets;

        private void OnEnable()
        {
            _titleLabelStyle = new GUIStyle
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            _titleLabelStyle.normal.textColor = Color.white;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.Space(VERTICAL_SPACE);

            CreateDefaultFolders();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            CreateDefaultScenes();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            InstallUnityPackages();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            InstallOpenSources();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            ImportAssetStoreAssets();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            ImportLocalDriveAssets();

            EditorGUILayout.EndVertical();

            if (_projectSetupWindow) _projectSetupWindow.Repaint();
        }

        private static void CreateDefaultFolders()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Label("Create Default Folders", _titleLabelStyle);

            _scrollPosForDefaultFolders = EditorGUILayout.BeginScrollView(_scrollPosForDefaultFolders, GUILayout.Width(SCROLL_VIEW_WIDTH), GUILayout.Height(SCROLL_VIEW_HEIGHT));

            EditorGUI.BeginDisabledGroup(true);

            var defaultFoldersListFileData = ReadFromFile("Assets/MyTools/PresetData/DefaultFoldersList.txt");
            var defaultFoldersNameStr = "";

            if (defaultFoldersListFileData != null)
            {
                foreach (var defaultFolderName in defaultFoldersListFileData)
                {
                    defaultFoldersNameStr += defaultFolderName + "\n";
                }
            }

            EditorGUILayout.TextArea(defaultFoldersNameStr.TrimEnd(), GUILayout.ExpandHeight(true));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();


            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                if (defaultFoldersListFileData != null)
                {
                    ProjectSetup.CreateDefaultFolders(defaultFoldersListFileData);
                }
            }

            if (GUILayout.Button("Edit", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
            {
                var path = Application.dataPath + "/MyTools/PresetData/DefaultFoldersList.txt";

                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Debug.LogError(path + " not found!");
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private static void CreateDefaultScenes()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Label("Create Default Scenes", _titleLabelStyle);

            _scrollPosForDefaultScenes = EditorGUILayout.BeginScrollView(_scrollPosForDefaultScenes, GUILayout.Width(SCROLL_VIEW_WIDTH), GUILayout.Height(SCROLL_VIEW_HEIGHT));

            EditorGUI.BeginDisabledGroup(true);

            var defaultScenesListFileData = ReadFromFile("Assets/MyTools/PresetData/DefaultScenesList.txt");
            var defaultScenesNameStr = "";

            if (defaultScenesListFileData != null)
            {
                foreach (var defaultSceneName in defaultScenesListFileData)
                {
                    defaultScenesNameStr += defaultSceneName + "\n";
                }
            }

            EditorGUILayout.TextArea(defaultScenesNameStr.TrimEnd(), GUILayout.ExpandHeight(true));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();


            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                if (defaultScenesListFileData != null)
                {
                    ProjectSetup.CreateDefaultScenes(defaultScenesListFileData);
                }
            }

            if (GUILayout.Button("Edit", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
            {
                var path = Application.dataPath + "/MyTools/PresetData/DefaultScenesList.txt";

                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Debug.LogError(path + " not found!");
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private static void InstallUnityPackages()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Label("Install Unity Packages", _titleLabelStyle);

            _scrollPosForUnityPackages = EditorGUILayout.BeginScrollView(_scrollPosForUnityPackages, GUILayout.Width(SCROLL_VIEW_WIDTH), GUILayout.Height(SCROLL_VIEW_HEIGHT));

            EditorGUI.BeginDisabledGroup(true);

            var unityPackageListFileData = ReadFromFile("Assets/MyTools/PresetData/UnityPackagesList.txt");
            var packageNameStr = "";

            if (unityPackageListFileData != null)
            {
                foreach (var packageName in unityPackageListFileData)
                {
                    packageNameStr += "\"" + packageName + "\"" + "\n";
                }
            }

            EditorGUILayout.TextArea(packageNameStr.TrimEnd(), GUILayout.ExpandHeight(true));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Install", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                if (unityPackageListFileData != null)
                {
                    ProjectSetup.InstallUnityPackages(unityPackageListFileData);
                }
            }

            if (GUILayout.Button("Edit", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
            {
                var path = Application.dataPath + "/MyTools/PresetData/UnityPackagesList.txt";
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Debug.LogError(path + " not found!");
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private static void InstallOpenSources()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Label("Install Open Sources", _titleLabelStyle);

            _scrollPosForOpenSources = EditorGUILayout.BeginScrollView(_scrollPosForOpenSources, GUILayout.Width(SCROLL_VIEW_WIDTH), GUILayout.Height(SCROLL_VIEW_HEIGHT));

            EditorGUI.BeginDisabledGroup(true);

            var openSourcesListFileData = ReadFromFile("Assets/MyTools/PresetData/OpenSourcesList.txt");
            var openSourceNameStr = "";

            if (openSourcesListFileData != null)
            {
                foreach (var openSourceName in openSourcesListFileData)
                {
                    openSourceNameStr += "\"" + openSourceName + "\"" + "\n";
                }
            }

            EditorGUILayout.TextArea(openSourceNameStr.TrimEnd(), GUILayout.ExpandHeight(true));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Install", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                if (openSourcesListFileData != null)
                {
                    ProjectSetup.InstallOpenSources(openSourcesListFileData);
                }
            }

            if (GUILayout.Button("Edit", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
            {
                var path = Application.dataPath + "/MyTools/PresetData/OpenSourcesList.txt";
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Debug.LogError(path + " not found!");
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private static void ImportAssetStoreAssets()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Label("Import Asset Store Assets", _titleLabelStyle);

            EditorGUILayout.BeginHorizontal();

            var assetStoreAssetsRootFileData = ReadFromFile("Assets/MyTools/PresetData/AssetStoreAssetsRoot.txt");
            var assetStoreAssetsRootStr = "";

            if (assetStoreAssetsRootFileData != null && assetStoreAssetsRootFileData.Length > 0)
            {
                assetStoreAssetsRootStr = assetStoreAssetsRootFileData[0];
            }

            GUILayout.Label(assetStoreAssetsRootStr, GUILayout.Height(BUTTON_HEIGHT));

            if (GUILayout.Button("Change Path", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(32)))
            {
                var directoryPath = EditorUtility.OpenFolderPanel("Select Directory", "", "");
                if (directoryPath != "")
                {
                    assetStoreAssetsRootStr = directoryPath;
                    WriteToFile("Assets/MyTools/PresetData/AssetStoreAssetsRoot.txt", assetStoreAssetsRootStr);
                }
            }

            EditorGUILayout.EndHorizontal();

            _scrollPosForAssetStoreAssets = EditorGUILayout.BeginScrollView(_scrollPosForAssetStoreAssets, GUILayout.Width(SCROLL_VIEW_WIDTH), GUILayout.Height(SCROLL_VIEW_HEIGHT));

            EditorGUI.BeginDisabledGroup(true);

            var assetStoreAssetsListFileData = ReadFromFile("Assets/MyTools/PresetData/AssetStoreAssetsList.txt");
            var assetStoreAssetNameStr = "";

            if (assetStoreAssetsListFileData != null)
            {
                foreach (var assetStoreAssetName in assetStoreAssetsListFileData)
                {
                    assetStoreAssetNameStr += assetStoreAssetName + "\n";
                }
            }

            EditorGUILayout.TextArea(assetStoreAssetNameStr.TrimEnd(), GUILayout.ExpandHeight(true));

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Install", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                if (assetStoreAssetsListFileData != null)
                {
                    ProjectSetup.ImportAssetStoreAssets(assetStoreAssetsRootStr, assetStoreAssetsListFileData);
                }
            }

            if (GUILayout.Button("Edit", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
            {
                var path = Application.dataPath + "/MyTools/PresetData/AssetStoreAssetsList.txt";
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Debug.LogError(path + " not found!");
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private static void ImportLocalDriveAssets()
        {
            EditorGUILayout.BeginVertical();

            GUILayout.Label("Import Local Drive Assets", _titleLabelStyle);

            EditorGUILayout.BeginHorizontal();

            var localDriveAssetsRootFileData = ReadFromFile("Assets/MyTools/PresetData/LocalDriveAssetsRoot.txt");
            var localDriveAssetsRootStr = "";

            if (localDriveAssetsRootFileData != null && localDriveAssetsRootFileData.Length > 0)
            {
                localDriveAssetsRootStr = localDriveAssetsRootFileData[0];
            }

            GUILayout.Label(localDriveAssetsRootStr, GUILayout.Height(BUTTON_HEIGHT));

            if (GUILayout.Button("Change Path", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(32)))
            {
                var directoryPath = EditorUtility.OpenFolderPanel("Select Directory", "", "");
                if (directoryPath != "")
                {
                    localDriveAssetsRootStr = directoryPath;
                    WriteToFile("Assets/MyTools/PresetData/LocalDriveAssetsRoot.txt", localDriveAssetsRootStr);
                }
            }

            EditorGUILayout.EndHorizontal();

            _scrollPosForLocalDriveAssets = EditorGUILayout.BeginScrollView(_scrollPosForLocalDriveAssets, GUILayout.Width(SCROLL_VIEW_WIDTH), GUILayout.Height(SCROLL_VIEW_HEIGHT));

            EditorGUI.BeginDisabledGroup(true);

            var localDriveAssetsListFileData = ReadFromFile("Assets/MyTools/PresetData/LocalDriveAssetsList.txt");
            var localDriveAssetNameStr = "";

            if (localDriveAssetsListFileData != null)
            {
                foreach (var localDriveAssetName in localDriveAssetsListFileData)
                {
                    localDriveAssetNameStr += localDriveAssetName + "\n";
                }
            }

            EditorGUILayout.TextArea(localDriveAssetNameStr.TrimEnd(), GUILayout.ExpandHeight(true));

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Install", GUILayout.ExpandWidth(true), GUILayout.Height(BUTTON_HEIGHT)))
            {
                if (localDriveAssetsListFileData != null)
                {
                    ProjectSetup.ImportAssetStoreAssets(localDriveAssetsRootStr, localDriveAssetsListFileData);
                }
            }

            if (GUILayout.Button("Edit", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
            {
                var path = Application.dataPath + "/MyTools/PresetData/LocalDriveAssetsList.txt";
                try
                {
                    System.Diagnostics.Process.Start(path);
                }
                catch
                {
                    Debug.LogError(path + " not found!");
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private static string[] ReadFromFile(string path)
        {
            try
            {
                var reader = new StreamReader(path);
                var outputStr = reader.ReadToEnd();
                reader.Close();

                List<string> outputList = new List<string>();

                // Remove empty lines
                var pattern = @"^\s*$";
                var rg = new Regex(pattern);

                foreach (var output in outputStr.Split(','))
                {
                    if (!rg.IsMatch(output))
                    {
                        outputList.Add(output.Trim());
                    }
                }
                return outputList.ToArray();
            }
            catch
            {
                Debug.LogError(path + " not found!");
                return null;
            }
        }

        private static void WriteToFile(string path, string data)
        {
            try
            {
                var writer = new StreamWriter(path);
                writer.WriteLine(data);
                writer.Close();
            }
            catch
            {
                Debug.LogError(path + " not found!");
            }
        }

        public static void InitWindow()
        {
            _projectSetupWindow = GetWindow<ProjectSetupWindow>(true, WINDOW_TITLE, true);
            _projectSetupWindow.minSize = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
            _projectSetupWindow.Show();
        }
    }
}