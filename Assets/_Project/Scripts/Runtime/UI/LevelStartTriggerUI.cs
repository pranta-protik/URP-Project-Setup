using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
	public class LevelStartTriggerUI : MonoBehaviour, IPointerDownHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			if (GameManager.Instance.CurrentGameState == GameManager.GameState.Waiting) GameManager.Instance.LevelStarted();
		}
	}
}