using System.Collections.Generic;
using System.Linq;
using MyTools.Utils;
using Project.IS;
using Project.Managers;
using Project.Persistent.SaveSystem;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DemoScene
{
	public class CoinHolder : MonoBehaviour, IDataPersistence
	{
		[Header("All collectable coins must be added\nas a child of this gameobject."), Space]
		[SerializeField] private List<CollectableCoin> _collectableCoinsList;

#if UNITY_EDITOR
		[ContextMenu("Find Collectable Coins")]
		private void FindCollectableCoins()
		{
			_collectableCoinsList = GetComponentsInChildren<CollectableCoin>().ToList();
			EditorUtility.SetDirty(gameObject);
		}
#endif

		public List<CollectableCoin> GetCollectableCoinsList()
		{
			return _collectableCoinsList;
		}

		public void LoadData(GameData gameData)
		{
			if (gameData.dictionaryOfcoinHolderDictionary.TryGetValue(SceneUtils.GetActiveSceneIndex(), out var coinHolderDictionary))
			{
				foreach (var keyValuePair in coinHolderDictionary)
				{
					if (keyValuePair.Value == true)
					{
						var collectableCoin = _collectableCoinsList[keyValuePair.Key];
						collectableCoin.IsPickedUp = true;
						collectableCoin.gameObject.SetActive(false);

						InventorySystem.Instance.Add(collectableCoin.ItemData);
					}
				}
			}
		}

		public void SaveData(GameData gameData)
		{
			if (gameData.dictionaryOfcoinHolderDictionary.ContainsKey(SceneUtils.GetActiveSceneIndex()))
			{
				gameData.dictionaryOfcoinHolderDictionary.Remove(SceneUtils.GetActiveSceneIndex());
			}

			if (GameManager.Instance.CurrentGameState != GameManager.GameState.GameOver)
			{
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

				gameData.dictionaryOfcoinHolderDictionary.Add(SceneUtils.GetActiveSceneIndex(), coinHolderDictionary);
			}
		}
	}
}