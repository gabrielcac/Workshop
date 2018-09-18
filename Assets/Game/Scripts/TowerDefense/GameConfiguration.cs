using UnityEngine;

namespace Workshop.TowerDefense
{
	[CreateAssetMenu(fileName = "GameConfiguration", menuName = "Workshop/Tower Defense/Game Configuration", order = 2)]
	public class GameConfiguration : ScriptableObject
	{
		public int startingMoney;
		public int towerPrice;
		public int machinegunPrice;
	}
}