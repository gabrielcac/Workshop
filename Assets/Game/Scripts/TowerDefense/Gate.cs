using UnityEngine;

namespace Workshop.TowerDefense
{
	public class Gate : MonoBehaviour
	{
		[SerializeField]
		private float _life;

		public void Damage(float damage)
		{
			_life = Mathf.Max(0, _life - damage);

			Debug.Log("O protão sofreu " + damage + " e está com " + _life + " pontos de vida.");
		}
	}
}