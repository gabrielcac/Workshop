using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBaseCanvas : MonoBehaviour, IPointerExitHandler
{
	public void OnPointerExit(PointerEventData eventData)
	{
		gameObject.SetActive(false);
	}
}
