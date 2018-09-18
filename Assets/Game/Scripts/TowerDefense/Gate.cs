using UnityEngine;

namespace Workshop.TowerDefense
{
	public class Gate : MonoBehaviour
	{
		[SerializeField]
		private float _life;

		public float Life
		{
			get
			{
				return _life;
			}
		}

		public void Damage(float damage)
		{
			_life = Mathf.Max(0, _life - damage);
		}
	}
}