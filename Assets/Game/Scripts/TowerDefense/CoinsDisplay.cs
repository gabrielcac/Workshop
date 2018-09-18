using UnityEngine;
using UnityEngine.UI;

namespace Workshop.TowerDefense
{
	public class CoinsDisplay : MonoBehaviour
	{
		public Text textField;

		private void Update()
		{
			textField.text = GameController.Instance.Money.ToString();
		}
	}
}