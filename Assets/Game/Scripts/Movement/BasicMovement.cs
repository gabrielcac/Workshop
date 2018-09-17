using UnityEngine;

namespace Workshop.Movement
{
	/// <summary>
	/// O objeto é deslocado em 1 posição nos eixos X e Z.
	/// </summary>
	public class BasicMovement : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				transform.position += new Vector3(0, 0, 1);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				transform.position += new Vector3(0, 0, -1);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				transform.position += new Vector3(-1, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				transform.position += new Vector3(1, 0, 0);
			}
		}
	}
}