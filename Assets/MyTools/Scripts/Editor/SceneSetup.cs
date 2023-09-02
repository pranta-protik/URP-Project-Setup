using Project;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;
using System.IO;

namespace MyTools
{
    public static class SceneSetup
    {
        public static bool TryOpenScene(string relativePath)
        {
            if (!File.Exists(Path.Combine(Application.dataPath, relativePath)))
            {
                EditorUtils.DisplayDialogBox("Error", $"{relativePath} scene not found!");
                return false;
            }

            if (SceneManager.GetSceneByPath("Assets/" + relativePath).isLoaded)
            {
                return true;
            }

            if (SceneManager.GetActiveScene().isDirty)
            {
                if (EditorUtils.DisplayDialogBoxWithOptions("Warning!", "Do you want to save the changes?\nYour changes will be lost if not saved!"))
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
                var initializerGO = GameObject.Find("Initializer") ?? new GameObject("Initializer");

                initializerGO.GetOrAdd<Initializer>();

                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                ForceSaveSceneAndProject.FunctionForceSaveSceneAndProject();

                Selection.activeGameObject = initializerGO;
            }
        }

        public static void SetupSplashScene(string relativePath)
        {
            if (TryOpenScene(relativePath))
            {
                if (Object.FindObjectsOfType<GameObject>().Length <= 0)
                {
                    SetupAndSaveSplashScene();
                    return;
                }

                if (EditorUtils.DisplayDialogBoxWithOptions("Warning!", "Are you sure you want to setup splash scene?\nAll existing data will be erased!"))
                {
                    SetupAndSaveSplashScene();
                }
            }
        }

        private static void SetupAndSaveSplashScene()
        {
            var splashUIGO = AssetDatabase.LoadAssetAtPath("Assets/SampleSceneAssets/Prefabs/UI/SplashUI.prefab", typeof(GameObject)) as GameObject;

            if (!splashUIGO)
            {
                EditorUtils.DisplayDialogBox("Error", $"Unable to find the SplashUI prefab!");
                return;
            }

            DestroyAll();

            var spawnedGO = Object.Instantiate(splashUIGO);
            spawnedGO.transform.DetachChildren();
            Object.DestroyImmediate(spawnedGO);

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            ForceSaveSceneAndProject.FunctionForceSaveSceneAndProject();
        }

        private static GameObject InstantiateAsPrefab(GameObject prefab, string prefabName)
        {
            if (prefab)
            {
                var spawnedGO = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

                return spawnedGO;
            }

            EditorUtils.DisplayDialogBox("Error", $"Unable to find the {prefabName} prefab!");
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