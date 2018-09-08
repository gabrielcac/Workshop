using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMovement : MonoBehaviour
{
	public float velocity;

	private Rigidbody _rigidbody;

	/// <summary>
	/// A função Awake é uma das primeiras a ser chamada após o objeto ser instanciado, e pode
	/// ser usada para inicialização do script.
	/// Outras funções que podem ser usadas para inicialização dependendo da situação são
	/// OnEnable e Start.
	/// </summary>
	private void Awake()
	{
		// A função GetComponent serve para encontrar outros componentes no mesmo GameObject.
		// As função GetComponentInParent e GetComponentInChildren funcionam de forma semelhante,
		// mas permitem que se encontre componentes ao longo da hierarquia.
		_rigidbody = GetComponent<Rigidbody>();
	}

	/// <summary>
	/// Diferente da função Update, chamada na renderização de cada frame, a função FixedUpdate
	/// é chamada com menos frequência, a cada loop da engine de física.
	/// </summary>
	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			_rigidbody.AddForce( velocity * Time.deltaTime * new Vector3(0, 0, 1), ForceMode.VelocityChange);
			//_rigidbody.MovePosition(transform.position + velocity * Time.deltaTime * new Vector3(0, 0, 1));
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			_rigidbody.AddForce(velocity * Time.deltaTime * new Vector3(0, 0, -1), ForceMode.VelocityChange);
			//_rigidbody.MovePosition(transform.position + velocity * Time.deltaTime * new Vector3(0, 0, -1));
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			_rigidbody.AddForce(velocity * Time.deltaTime * new Vector3(-1, 0, 0), ForceMode.VelocityChange);
			//_rigidbody.MovePosition(transform.position + velocity * Time.deltaTime * new Vector3(-1, 0, 0));
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			_rigidbody.AddForce(velocity * Time.deltaTime * new Vector3(1, 0, 0), ForceMode.VelocityChange);
			//_rigidbody.MovePosition(transform.position + velocity * Time.deltaTime * new Vector3(1, 0, 0));
		}
	}
}