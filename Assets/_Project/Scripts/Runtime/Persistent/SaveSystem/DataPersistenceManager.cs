using System.Collections.Generic;
using System.Linq;
using MyTools.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Persistent.SaveSystem
{
	public class DataPersistenceManager : MonoBehaviour
	{
		public static DataPersistenceManager Instance { get; private set; }

		[Header("File Storage Config")]
		[SerializeField] private string _fileName;
		[SerializeField] private bool _useEncryption;

		private GameData _gameData;
		private List<IDataPersistence> _dataPersistenceObjectsList;
		private FileDataHandler _fileDataHandler;

		private void Awake()
		{
			if (Instance != null)
			{
				DebugUtils.LogWarning($"There's more than one instance of DataPersistenceManager in the scene! Destroying the new instance!");
				Destroy(gameObject);
				return;
			}

			Instance = this;
			DontDestroyOnLoad(gameObject);

			_fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
		}

		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			_dataPersistenceObjectsList = FindAllDataPersistenceObjects();
			LoadGame();
		}

		public void NewGame()
		{
			_gameData = new GameData();
		}

		public void LoadGame()
		{
			// Load any saved data from a file using the file data handler
			_gameData = _fileDataHandler.Load();

			// If no data can be loaded, initialize to a new game
			if (_gameData == null)
			{
				DebugUtils.Log("No data was found. Initializing data to defaults.");
				NewGame();
			}

			// Push the loaded data to all other scripts that need it
			foreach (var dataPersistenceObject in _dataPersistenceObjectsList)
			{
				dataPersistenceObject.LoadData(_gameData);
			}
		}

		public void SaveGame()
		{
			// Pass the data to other scripts so they can update it
			foreach (var dataPersistenceObject in _dataPersistenceObjectsList)
			{
				dataPersistenceObject.SaveData(_gameData);
			}

			// Save that data to a file using the data handler
			_fileDataHandler.Save(_gameData);
		}

		private void OnApplicationQuit()
		{
			SaveGame();
		}

		private List<IDataPersistence> FindAllDataPersistenceObjects()
		{
			IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

			return new List<IDataPersistence>(dataPersistenceObjects);
		}
	}
}