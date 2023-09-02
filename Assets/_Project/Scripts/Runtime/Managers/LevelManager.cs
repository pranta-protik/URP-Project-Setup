using System;
using MyTools;
using UnityEngine;

namespace Project
{
	public class LevelManager : Singleton<LevelManager>
	{
		private int _totalSceneCount;
		private int _firstLevelSceneIndex;

		protected override void OnAwake()
		{
			base.OnAwake();

			_totalSceneCount = PlayerPrefs.GetInt(ConstUtils.TOTAL_SCENE_COUNT, Enum.GetNames(typeof(SceneIndex)).Length);
			_firstLevelSceneIndex = PlayerPrefs.GetInt(ConstUtils.FIRST_LEVEL_SCENE_INDEX, (int)SceneIndex.GAME);
		}

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