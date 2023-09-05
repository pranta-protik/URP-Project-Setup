using MyTools;
using UnityEngine;

namespace Project
{
	public class LevelLoader : Singleton<LevelLoader>
	{
		[SerializeField, Min(0)] private int _totalSceneCount;
		[SerializeField, Min(0)] private int _firstLevelSceneIndex = (int)SceneIndex.GAME;

		public int GetNextSceneIndex()
		{
			var nextSceneIndex = PlayerPrefs.GetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, (int)SceneIndex.GAME);
			var inGameLevelCount = PlayerPrefs.GetInt(ConstUtils.IN_GAME_LEVEL_COUNT, 1);

			if (nextSceneIndex >= _totalSceneCount - 1)
			{
				nextSceneIndex = _firstLevelSceneIndex;
			}
			else
			{
				nextSceneIndex++;
			}

			PlayerPrefs.SetInt(ConstUtils.LAST_PLAYED_SCENE_INDEX, nextSceneIndex);
			PlayerPrefs.SetInt(ConstUtils.IN_GAME_LEVEL_COUNT, inGameLevelCount + 1);

			return nextSceneIndex;
		}
	}
}