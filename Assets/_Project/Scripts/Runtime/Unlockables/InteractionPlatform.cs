using KBCore.Refs;
using MyTools.Utils;
using Project.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
	public class InteractionPlatform : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField, Anywhere] private GameObject _interactableGO;
		[SerializeField, Anywhere] private Image _loadingBarFill;

		[Header("Porgression Settings")]
		[SerializeField] private float _preparationDuration = 2f;

		private IInteractable _interactable;
		private Transform _interactor;
		private CountdownTimer _preparationTimer;

		private void Awake()
		{
			_interactable = _interactableGO.GetComponent<IInteractable>();
			_preparationTimer = new CountdownTimer(_preparationDuration);
		}

		private void Start()
		{
			_preparationTimer.OnTimerStop += () =>
			{
				_interactable.Interact(_interactor);
				_loadingBarFill.fillAmount = 0f;
			};

			_loadingBarFill.fillAmount = 0f;
		}

		private void Update()
		{
			_preparationTimer.Tick(Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.gameObject.CompareTag(ConstUtils.TAG_PLAYER)) return;

			_interactor = other.transform;
			_preparationTimer.Reset();
			_preparationTimer.Start();
		}

		private void OnTriggerStay(Collider other)
		{
			if (!other.gameObject.CompareTag(ConstUtils.TAG_PLAYER)) return;

			_loadingBarFill.fillAmount = _preparationTimer.Progress;
		}

		private void OnTriggerExit(Collider other)
		{
			if (!other.gameObject.CompareTag(ConstUtils.TAG_PLAYER)) return;

			_interactor = null;
			_preparationTimer.Pause();
			_loadingBarFill.fillAmount = 0f;
		}
	}
}