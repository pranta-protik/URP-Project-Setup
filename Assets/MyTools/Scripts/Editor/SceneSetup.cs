using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;
using System.IO;
using MyTools.Settings;
using MyTools.EditorScript.HotKeys;

namespace MyTools.EditorScript
{
    public static class SceneSetup
    {
        public static bool TryOpenScene(string relativePath, bool forceOpen = false)
        {
            if (!File.Exists(Path.Combine(Application.dataPath, relativePath)))
            {
                EditorUtils.DisplayDialogBox("Error!", $"{relativePath} scene not found.");
                return false;
            }

            if (SceneManager.GetSceneByPath("Assets/" + relativePath).isLoaded && !forceOpen)
            {
                return true;
            }

            if (SceneManager.GetActiveScene().isDirty)
            {
                if (EditorUtils.DisplayDialogBoxWithOptions("Warning!", "Do you want to save the changes?\nYour changes will be lost if not saved."))
                {
                    ForceSaveSceneAndProject.FunctionForceSaveSceneAndProject();
                    EditorSceneManager.OpenScene("Assets/" + relativePath);
                    return true;
                }
            }

            EditorSceneManager.OpenScene("Assets/" + relativePath);
            return true;
        }

        public static void SetupPersistentScene(string relativePath)
        {
            if (TryOpenScene(relativePath))
            {
                var initializerPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Project/Prefabs/Persistent/Initializer.prefab", typeof(GameObject)) as GameObject;
                if (!GameObject.Find("Initializer"))
                {
                    InstantiateAsPrefab(initializerPrefab, "Initializer");
                }

                var levelLoaderPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Project/Prefabs/Persistent/LevelLoader.prefab", typeof(GameObject)) as GameObject;
                if (!GameObject.Find("LevelLoader"))
                {
                    InstantiateAsPrefab(levelLoaderPrefab, "LevelLoader");
                }

                var dataPersistenceManagerPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Project/Prefabs/Persistent/DataPersistenceManager.prefab", typeof(GameObject)) as GameObject;
                if (!GameObject.Find("DataPersistenceManager"))
                {
                    InstantiateAsPrefab(dataPersistenceManagerPrefab, "DataPersistenceManager");
                }

                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                ForceSaveSceneAndProject.FunctionForceSaveSceneAndProject();
            }
        }

        public static void SetupSplashScene(string relativePath)
        {
            if (TryOpenScene(relativePath))
            {
                var prefabPath = "Assets/DemoSceneAssets/Prefabs/DemoScenes/SplashScene.prefab";

                if (Object.FindObjectsOfType<GameObject>().Length <= 0)
                {
                    SetupAndSaveUsingPrefab(prefabPath, "SplashScene");
                    return;
                }

                if (EditorUtils.DisplayDialogBoxWithOptions("Warning!", "Are you sure you want to setup splash scene?\nAll existing data will be erased."))
                {
                    SetupAndSaveUsingPrefab(prefabPath, "SplashScene");
                }
            }
        }

        public static void SetupUIScene(string relativePath)
        {
            if (TryOpenScene(relativePath))
            {
                var prefabPath = "Assets/DemoSceneAssets/Prefabs/DemoScenes/UIScene.prefab";

                if (Object.FindObjectsOfType<GameObject>().Length <= 0)
                {
                    SetupAndSaveUsingPrefab(prefabPath, "UIScene");
                    return;
                }

                if (EditorUtils.DisplayDialogBoxWithOptions("Warning!", "Are you sure you want to setup UI scene?\nAll existing data will be erased."))
                {
                    SetupAndSaveUsingPrefab(prefabPath, "UIScene");
                }
            }
        }

        public static void SetupGameScene(string relativePath)
        {
            if (TryOpenScene(relativePath))
            {
                var prefabPath = "Assets/DemoSceneAssets/Prefabs/DemoScenes/DemoScene.prefab";
                var lightingSettingsPath = "Assets/DemoSceneAssets/Settings/DemoSceneLightingSettings.asset";

                if (Object.FindObjectsOfType<GameObject>().Length <= 0)
                {
                    SetupAndSaveUsingPrefab(prefabPath, "DemoScene");
                    SetupLightingSettings(lightingSettingsPath, relativePath);

                    return;
                }

                if (EditorUtils.DisplayDialogBoxWithOptions("Warning!", "Are you sure you want to setup Game scene?\nAll existing data will be erased."))
                {
                    SetupAndSaveUsingPrefab(prefabPath, "DemoScene");
                    SetupLightingSettings(lightingSettingsPath, relativePath);
                }
            }
        }

        private static void SetupAndSaveUsingPrefab(string prefabPath, string prefabName)
        {
            var prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)) as GameObject;

            if (!prefab)
            {
                EditorUtils.DisplayDialogBox("Error!", $"Unable to find the {prefabName} prefab.");
                return;
            }

            DestroyAll();

            var spawnedPrefab = InstantiateAsPrefab(prefab, prefabName);
            PrefabUtility.UnpackPrefabInstance(spawnedPrefab, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
            spawnedPrefab.transform.DetachChildren();
            Object.DestroyImmediate(spawnedPrefab);

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            ForceSaveSceneAndProject.FunctionForceSaveSceneAndProject();
        }

        private static void SetupLightingSettings(string settingsPath, string scenePath)
        {
            var settings = (LightingSettingsSO)AssetDatabase.LoadAssetAtPath(settingsPath, typeof(LightingSettingsSO));

            RenderSettings.skybox = settings.skyboxMat;

            foreach (var light in Object.FindObjectsOfType<Light>())
            {
                if (light.type == LightType.Directional)
                {
                    RenderSettings.sun = light;
                    break;
                }
            }

            RenderSettings.ambientMode = settings.ambientMode;
            RenderSettings.ambientLight = settings.ambientColor;

            if (EditorUtils.DisplayDialogBoxWithOptions("Generate Lightmap", "Do you want to generate lightmap for this scene?"))
            {
                Lightmapping.BakeAsync();

                Lightmapping.bakeCompleted += () =>
                {
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    ForceSaveSceneAndProject.FunctionForceSaveSceneAndProject();

                    TryOpenScene(scenePath, true);
                };
            }
            else
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                ForceSaveSceneAndProject.FunctionForceSaveSceneAndProject();

                TryOpenScene(scenePath, true);
            }
        }

        private static GameObject InstantiateAsPrefab(GameObject prefab, string prefabName)
        {
            if (prefab)
            {
                var spawnedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

                return spawnedPrefab;
            }

            EditorUtils.DisplayDialogBox("Error!", $"Unable to find the {prefabName} prefab.");
            return null;
        }

        private static void DestroyAll()
        {
            foreach (var go in Object.FindObjectsOfType<GameObject>())
            {
                Object.DestroyImmediate(go);
            }
        }
    }
}