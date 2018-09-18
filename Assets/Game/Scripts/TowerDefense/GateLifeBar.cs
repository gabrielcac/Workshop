using UnityEngine;
using UnityEngine.UI;

namespace Workshop.TowerDefense
{
	public class GateLifeBar : MonoBehaviour
	{
		public Gate gate;
		public Image bar;

		private RectTransform _barRectTransform;
		private float _initialBarWidth;
		private float _initialGateLife;

		private void Start()
		{
			_barRectTransform = bar.GetComponent<RectTransform>();
			_initialBarWidth = _barRectTransform.sizeDelta.x;
			_initialGateLife = gate.Life;
		}

		private void Update()
		{
			_barRectTransform.sizeDelta = new Vector2((gate.Life / _initialGateLife) * _initialBarWidth, _barRectTransform.sizeDelta.y);
		}
	}
}