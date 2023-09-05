using MyTools;
using UnityEngine;

namespace Project
{
	public class Initializer : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);

			if (PlayerPrefs.GetInt(ConstUtils.FIRST_TIME_PLAYING, 1) == 1)
			{
				PlayerPrefs.SetInt(ConstUtils.FIRST_TIME_PLAYING, 0);
				SceneUtils.LoadSpecificScene((int)SceneIndex.SPLASH);
			}
			else
			{
				SceneUtils.LoadSpecificScene(PlayerPrefs.GetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, (int)SceneIndex.GAME));
			}
		}
	}
}