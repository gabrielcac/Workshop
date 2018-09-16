using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameConfiguration configuration;

	private int _money;

	public int Money
	{
		get
		{
			return _money;
		}
		set
		{
			_money = value;
		}
	}

	private void Awake()
	{
		_money = configuration.startingMoney;
	}
}