using UnityEngine;

namespace Workshop.TowerDefense
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(MeshRenderer))]
	public class Towerbase : MonoBehaviour
	{
		public Color mouseOverColor;
		public Canvas buildingCanvas;

		private MeshRenderer _renderer;
		private Animator _animator;
		private bool _builded;
		private bool _mouseOver;

		private void Awake()
		{
			_renderer = GetComponent<MeshRenderer>();
			_animator = GetComponent<Animator>();
		}

		private void Update()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray, 60f);
			_mouseOver = false;
			for(int i = 0, j = hits.Length; i < j; i++)
			{
				if(hits[i].collider.GetComponent<Towerbase>()  == this)
				{
					_mouseOver = true;
					if (!_builded)
					{
						_renderer.material.color = mouseOverColor;
					}
				}
			}

			if(_mouseOver && Input.GetKeyDown(KeyCode.Mouse0))
			{
				if (!_builded)
				{
					buildingCanvas.gameObject.SetActive(true);
				}
			}

			if(!_mouseOver)
			{
				_renderer.material.color = Color.white;
			}
		}

		public void BuildTower()
		{
			if (GameController.Instance.Money >= GameController.Instance.configuration.towerPrice)
			{
				GameController.Instance.Money -= GameController.Instance.configuration.towerPrice;
				_animator.SetTrigger("BuildTower");
				_builded = true;
			}
		}

		public void BuildMachinegun()
		{
			if (GameController.Instance.Money >= GameController.Instance.configuration.machinegunPrice)
			{
				GameController.Instance.Money -= GameController.Instance.configuration.machinegunPrice;
				_animator.SetTrigger("BuildMachinegun");
				_builded = true;
			}
		}
	}
}