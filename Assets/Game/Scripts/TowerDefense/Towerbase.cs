using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshRenderer))]
public class Towerbase : MonoBehaviour
{
	public Color mouseOverColor;
	public GameController gameController;
	public Canvas buildingCanvas;

	private MeshRenderer _renderer;
	private Animator _animator;
	private bool _builded;

	private void Awake()
	{
		_renderer = GetComponent<MeshRenderer>();
		_animator = GetComponent<Animator>();
	}

	private void OnMouseOver()
	{
		if(!_builded)
		{
			_renderer.material.color = mouseOverColor;
		}
	}

	private void OnMouseDown()
	{
		if (!_builded)
		{
			buildingCanvas.gameObject.SetActive(true);
		}
	}

	private void OnMouseExit()
	{
		_renderer.material.color = Color.white;
	}

	public void BuildTower()
	{
		if(gameController.Money >= gameController.configuration.towerPrice)
		{
			gameController.Money -= gameController.configuration.towerPrice;
			_animator.SetTrigger("BuildTower");
			_builded = true;
		}
	}

	public void BuildMachinegun()
	{
		if (gameController.Money >= gameController.configuration.machinegunPrice)
		{
			gameController.Money -= gameController.configuration.machinegunPrice;
			_animator.SetTrigger("BuildMachinegun");
			_builded = true;
		}
	}
}
