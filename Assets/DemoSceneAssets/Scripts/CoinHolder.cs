using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using MyTools.Utils;
using Project.Managers;
using Project.Persistent.SaveSystem;
using UnityEngine;

namespace DemoScene
{
	public class CoinHolder : MonoBehaviour, IDataPersistence
	{
		[Header("All collectable coins must be added")]
		[Header("as a child of this gameobject")]
		[SerializeField] private List<CollectableCoin> _collectableCoinsList;

		[Button, PropertySpace]
		private void FindAllCollectableCoins() => _collectableCoinsList = GetComponentsInChildren<CollectableCoin>().ToList();

		public List<CollectableCoin> GetCollectableCoinsList() => _collectableCoinsList;

		public void LoadData(GameData gameData)
		{
			if (gameData.dictionaryOfCoinHolderDictionary.TryGetValue(SceneUtils.GetActiveSceneIndex(), out var coinHolderDictionary))
			{
				foreach (var keyValuePair in coinHolderDictionary)
				{
					if (keyValuePair.Value == true)
					{
						var collectableCoin = _collectableCoinsList[keyValuePair.Key];
						collectableCoin.IsPickedUp = true;
						collectableCoin.gameObject.SetActive(false);
					}
				}
			}
		}

		public void SaveData(GameData gameData)
		{
			if (gameData.dictionaryOfCoinHolderDictionary.ContainsKey(SceneUtils.GetActiveSceneIndex()))
			{
				gameData.dictionaryOfCoinHolderDictionary.Remove(SceneUtils.GetActiveSceneIndex());
			}

			if (GameManager.Instance.IsGameOver()) return;

			SerializableDictionary<int, bool> coinHolderDictionary = new();

			for (var i = 0; i < _collectableCoinsList.Count; i++)
			{
				if (_collectableCoinsList[i].IsPickedUp)
				{
					coinHolderDictionary.Add(i, true);
				}
				else
				{
					coinHolderDictionary.Add(i, false);
				}
			}

			gameData.dictionaryOfCoinHolderDictionary.Add(SceneUtils.GetActiveSceneIndex(), coinHolderDictionary);
		}
	}
}