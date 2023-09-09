using System;
using System.Collections;
using Cinemachine;
using KBCore.Refs;
using MyTools.Utils;
using Project.Managers;
using Project.Persistent.SaveSystem;
using UnityEngine;

namespace Project
{
	public class CharacterSwitcher : Singleton<CharacterSwitcher>, IDataPersistence
	{
		private enum CharacterType
		{
			Human,
			Plane
		}

		[SerializeField] private CharacterType _activeCharacterType = CharacterType.Human;
		[SerializeField, Anywhere] private GameObject _playerHumanGO;
		[SerializeField, Anywhere] private GameObject _playerPlaneGO;
		[SerializeField, Anywhere] private CinemachineVirtualCamera _playerHumanVCam;
		[SerializeField, Anywhere] private CinemachineVirtualCamera _playerPlaneVCam;
		[SerializeField, Anywhere] private CinemachineBrain _cinemachineBrain;

		private float _defaultBlendTime;

		private void OnValidate() => this.ValidateRefs();

		private void Start()
		{
			_defaultBlendTime = _cinemachineBrain.m_DefaultBlend.m_Time;
			_cinemachineBrain.m_DefaultBlend.m_Time = 0f;

			switch (_activeCharacterType)
			{
				case CharacterType.Human:
					SwitchToHumanPlayerCharacter();
					break;

				case CharacterType.Plane:
					SwitchToPlanePlayerCharacter();
					break;
			}
			StartCoroutine(ResetCameraBlendTimeRoutine());
		}

		private IEnumerator ResetCameraBlendTimeRoutine()
		{
			yield return new WaitForEndOfFrame();
			_cinemachineBrain.m_DefaultBlend.m_Time = _defaultBlendTime;
		}

		public void SwitchToHumanPlayerCharacter()
		{
			_activeCharacterType = CharacterType.Human;

			_playerPlaneGO.SetActive(false);
			_playerHumanGO.SetActive(true);

			_playerPlaneVCam.Priority = 5;
			_playerHumanVCam.Priority = 10;
		}

		public void SwitchToPlanePlayerCharacter()
		{
			_activeCharacterType = CharacterType.Plane;

			_playerHumanGO.SetActive(false);
			_playerPlaneGO.SetActive(true);

			_playerHumanVCam.Priority = 5;
			_playerPlaneVCam.Priority = 10;
		}

		public void LoadData(GameData gameData)
		{
			if (gameData.activeCharacterTypeDictionary.TryGetValue(SceneUtils.GetActiveSceneIndex(), out var activeCharacterType))
			{
				if (Enum.TryParse(activeCharacterType, out CharacterType characterType))
				{
					_activeCharacterType = characterType;
				}
			}
		}

		public void SaveData(GameData gameData)
		{
			if (gameData.activeCharacterTypeDictionary.ContainsKey(SceneUtils.GetActiveSceneIndex()))
			{
				gameData.activeCharacterTypeDictionary.Remove(SceneUtils.GetActiveSceneIndex());
			}

			if (GameManager.Instance.IsGameOver()) return;

			gameData.activeCharacterTypeDictionary.Add(SceneUtils.GetActiveSceneIndex(), _activeCharacterType.ToString());
		}
	}
}