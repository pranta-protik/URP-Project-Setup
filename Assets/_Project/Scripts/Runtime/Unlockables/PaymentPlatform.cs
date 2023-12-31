using Sirenix.OdinInspector;
using KBCore.Refs;
using MyTools.Utils;
using Project.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Unlockables
{
	public class PaymentPlatform : ValidatedMonoBehaviour
	{
		[TabGroup("References")][SerializeField, Anywhere] private GameObject _unlockableGO;
		[TabGroup("References")][SerializeField, Anywhere] private Image _loadingBarFill;
		[TabGroup("References")][SerializeField, Anywhere] private Image _progressBarFill;
		[TabGroup("References")][SerializeField, Child] private TextMeshProUGUI _progressText;

		[TabGroup("Progression Settings")][SerializeField] private float _preparationDuration = 2f;
		[TabGroup("Progression Settings")][SerializeField] private int _installmentValue = 1;
		[TabGroup("Progression Settings")][SerializeField] private float _installmentInterval = 0.2f;

		private IUnlockable _unlockable;
		private CountdownTimer _preparationTimer;
		private CountdownTimer _installmentTimer;
		private bool _isPreparingForPayment;
		private bool _isPaymentOngoing;

		private void Awake()
		{
			_unlockable = _unlockableGO.GetComponent<IUnlockable>();

			_preparationTimer = new CountdownTimer(_preparationDuration);
			_installmentTimer = new CountdownTimer(_installmentInterval);
		}

		private void Start()
		{
			_preparationTimer.OnTimerStop += () =>
			{
				_isPreparingForPayment = false;
				_isPaymentOngoing = true;
				_installmentTimer.Start();
			};

			_installmentTimer.OnTimerStop += () =>
			{
				if (!_isPreparingForPayment && _isPaymentOngoing)
				{
					if (_unlockable.CanDeposit())
					{
						DOPayment();
						_installmentTimer.Start();
					}
					else
					{
						_isPaymentOngoing = false;
					}
				}
			};

			_loadingBarFill.fillAmount = 0f;

			UpdateProgressBar();
		}

		private void Update()
		{
			_preparationTimer.Tick(Time.deltaTime);
			_installmentTimer.Tick(Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.gameObject.CompareTag(ConstUtils.TAG_PLAYER)) return;
			if (!_unlockable.CanDeposit()) return;

			_isPreparingForPayment = true;
			_isPaymentOngoing = false;
			_preparationTimer.Reset();
			_preparationTimer.Start();
		}

		private void OnTriggerStay(Collider other)
		{
			if (!other.gameObject.CompareTag(ConstUtils.TAG_PLAYER)) return;
			if (!_unlockable.CanDeposit()) return;

			_loadingBarFill.fillAmount = _preparationTimer.Progress;
		}

		private void OnTriggerExit(Collider other)
		{
			if (!other.gameObject.CompareTag(ConstUtils.TAG_PLAYER)) return;

			_isPreparingForPayment = false;
			_isPaymentOngoing = false;
			_preparationTimer.Pause();
			_installmentTimer.Pause();

			_loadingBarFill.fillAmount = 0f;
		}

		private void DOPayment()
		{
			_unlockable.Deposit(_installmentValue);
			UpdateProgressBar();
		}

		private void UpdateProgressBar()
		{
			_progressBarFill.fillAmount = _unlockable.GetDepositedAmountNormalized();

			var progressPercent = (int)(_unlockable.GetDepositedAmountNormalized() * 100f);
			_progressText.text = $"{progressPercent}%";
		}
	}
}