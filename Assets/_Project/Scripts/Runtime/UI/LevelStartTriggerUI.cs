using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
	public class LevelStartTriggerUI : MonoBehaviour, IPointerDownHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			GameManager.Instance.LevelStart();
			gameObject.SetActive(false);
		}
	}
}