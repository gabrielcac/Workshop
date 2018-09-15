using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
	private enum StateId { Idle, Walking, Running }

	public float walkingSpeed;

	// O Animator é responsável por controlar as animações do personagem através de uma
	// máquina de estados
	public Animator animator;
	public string moveSpeedFloatProperty = "MoveSpeed";

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 direction = Vector3.zero; //new Vector3(0, 0, 0);
		if (Input.GetKey(KeyCode.UpArrow))
		{
			direction += Vector3.forward; //new Vector3(0, 0, 1);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			direction += Vector3.back; //new Vector3(0, 0, -1);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			direction += Vector3.left; //new Vector3(-1, 0, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			direction += Vector3.right; //new Vector3(1, 0, 0);
		}

		_rigidbody.velocity = walkingSpeed * direction;

		if (direction != Vector3.zero)
		{
			animator.SetFloat(moveSpeedFloatProperty, walkingSpeed);

			// Utilizamos matrizes 4x4 chamadas de Quaternions para rotacionar os
			// objetos
			transform.rotation = Quaternion.LookRotation(direction);
		}
		else
		{
			animator.SetFloat(moveSpeedFloatProperty, 0);
		}
	}
}