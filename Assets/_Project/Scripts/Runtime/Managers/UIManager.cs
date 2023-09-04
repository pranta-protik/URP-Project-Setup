using MyTools;
using UnityEngine.SceneManagement;

namespace Project
{
	public class UIManager : Singleton<UIManager>
	{
		private void Start()
		{
			if (!SceneManager.GetSceneByBuildIndex((int)SceneIndex.UI).isLoaded)
			{
				SceneUtils.LoadSpecificScene((int)SceneIndex.UI, LoadSceneMode.Additive).completed += (_) =>
				{
					SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)SceneIndex.GAME));
				};
			}
		}
	}
}