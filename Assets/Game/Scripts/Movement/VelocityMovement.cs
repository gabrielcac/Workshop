using UnityEngine;

namespace Workshop.Movement
{
	/// <summary>
	/// O objeto é deslocado ao longo dos eixos X e Z, mas de forma contínua com a velocidade
	/// configurada pelo usuário.
	/// </summary>
	public class VelocityMovement : MonoBehaviour
	{
		public float velocity;

		private void Update()
		{
			// A função GetKey irá retornar true em todos os frames enquanto a tecla for pressionada,
			// diferente da função GetKeyDown que retorna true somente no primeiro frame.
			if (Input.GetKey(KeyCode.UpArrow))
			{
				// A propriedade Time.deltaTime retorna o tempo (em segundos) que se passou desde
				// o último frame.
				transform.position += velocity * Time.deltaTime * new Vector3(0, 0, 1);
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				transform.position += velocity * Time.deltaTime * new Vector3(0, 0, -1);
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.position += velocity * Time.deltaTime * new Vector3(-1, 0, 0);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.position += velocity * Time.deltaTime * new Vector3(1, 0, 0);
			}
		}
	}
}