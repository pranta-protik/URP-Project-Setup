using MyTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
	public class Initializer : MonoBehaviour
	{
		[SerializeField, Min(0)] private int _totalSceneCount;
		[SerializeField, Min(0)] private int _firstLevelSceneIndex = (int)SceneIndex.GAME;

		private void Awake()
		{
			DontDestroyOnLoad(this);

			PlayerPrefs.SetInt(ConstUtils.TOTAL_SCENE_COUNT, _totalSceneCount);
			PlayerPrefs.SetInt(ConstUtils.FIRST_LEVEL_SCENE_INDEX, _firstLevelSceneIndex);

			if (PlayerPrefs.GetInt(ConstUtils.FIRST_TIME_PLAYING, 1) == 1)
			{
				SceneUtils.LoadSpecificScene((int)SceneIndex.SPLASH);
				PlayerPrefs.SetInt(ConstUtils.FIRST_TIME_PLAYING, 0);
			}
			else
			{
				SceneUtils.LoadSpecificScene(PlayerPrefs.GetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, (int)SceneIndex.GAME));
			}
		}
	}
}