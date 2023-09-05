using MyTools;
using UnityEngine;

namespace Project
{
	public class Initializer : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);

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