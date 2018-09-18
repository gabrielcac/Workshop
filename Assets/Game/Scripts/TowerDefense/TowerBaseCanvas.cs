using UnityEngine;
using UnityEngine.EventSystems;

namespace Workshop.TowerDefense
{
	public class TowerBaseCanvas : MonoBehaviour, IPointerExitHandler
	{
		public void OnPointerExit(PointerEventData eventData)
		{
			gameObject.SetActive(false);
		}
	}
}