using UnityEngine;

/// <summary>
/// Movimenta a câmera pela cena do jogo
/// </summary>
public class CameraMovement : MonoBehaviour
{
	public Bounds cameraLimits;
	public KeyCode moveForward = KeyCode.W;
	public KeyCode moveBackward = KeyCode.S;
	public KeyCode moveRight = KeyCode.D;
	public KeyCode moveLeft = KeyCode.A;
	public float movingSpeed = 10f;
	public KeyCode rotateLeft = KeyCode.Q;
	public KeyCode rotateRight = KeyCode.E;
	public float rotateSpeed = 15f;


	private void Update()
	{
		if (Input.GetKey(moveForward))
		{
			transform.position += movingSpeed * Time.deltaTime * Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
		}
		if (Input.GetKey(moveBackward))
		{
			transform.position += movingSpeed * Time.deltaTime * Vector3.Scale(new Vector3(1, 0, 1), -transform.forward);
		}
		if (Input.GetKey(moveLeft))
		{
			transform.position += movingSpeed * Time.deltaTime * Vector3.Scale(new Vector3(1, 0, 1), -transform.right);
		}
		if (Input.GetKey(moveRight))
		{
			transform.position += movingSpeed * Time.deltaTime * Vector3.Scale(new Vector3(1, 0, 1), transform.right);
		}

		if(!cameraLimits.Contains(transform.position))
		{
			transform.position = cameraLimits.ClosestPoint(transform.position);
		}

		if(Input.GetKey(rotateLeft))
		{
			transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0), Space.World);
		}
		if (Input.GetKey(rotateRight))
		{
			transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0), Space.World);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(cameraLimits.center, cameraLimits.size);
	}
}
