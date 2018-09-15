using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraBasedCharacterMovement : MonoBehaviour
{
	private enum StateId { Idle, Walking, Running }

	public float walkingSpeed;
	public Camera gameCamera;
	
	public Animator animator;
	public string moveSpeedFloatProperty = "MoveSpeed";

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 direction = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			direction += gameCamera.transform.forward;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			direction += -gameCamera.transform.forward;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			direction += -gameCamera.transform.right;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			direction += gameCamera.transform.right;
		}

		direction.y = 0;

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