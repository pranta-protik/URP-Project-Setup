using UnityEngine.SceneManagement;

namespace MyTools
{
    public static class SceneUtils
    {
        public static void LoadSpecificScene(int sceneIndex) => SceneManager.LoadSceneAsync(sceneIndex);
        public static void LoadSpecificScene(int sceneIndex, LoadSceneMode loadSceneMode) => SceneManager.LoadSceneAsync(sceneIndex, loadSceneMode);
        public static void ReloadScene() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}