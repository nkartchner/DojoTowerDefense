using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
	// Left mouse button: Move forward with rotation
	// Right mouse button: Move backward with rotation
	// Both buttons: Rotate without movement

	// Limits
	float startFOV = 60;
	float minFOV = 15f;
	float maxFOV = 90f;
	float maxDistance = 900f; // from 0,0,0

	// Movement (left/right mouse)
	float minMoveSpeed = 250f;
	float maxMoveSpeed = 700f;
	float moveRampTime = 5f;

	// Rotation (both mouse buttons)
	float rotateSpeed = 6f;

	float zoomSpeed = 3f;

	Camera cam;
	float movementTime = 0;
	float movementSpeed = 10;
	Vector3 lastMousePosition;

	void Awake()
	{
		cam = GetComponent<Camera>();
		cam.fieldOfView = startFOV;
	}

	void LateUpdate()
	{
		bool b0 = Input.GetMouseButton(0);
		bool b1 = Input.GetMouseButton(1);

		// Movement (Left: forward, Right: backward)
		Vector3 translate = Vector3.zero;
		if (b0 != b1)
		{
			float direction = (b0) ? 1 : -1;
			translate = transform.forward.normalized * direction * Time.deltaTime * movementSpeed;
			translate.y = 0f;
			translate = Vector3.ClampMagnitude(
			   new Vector3(transform.position.x, 0f, transform.position.z) + translate,
			   maxDistance);
			translate.y = transform.position.y;
			transform.position = translate;

			movementTime += Time.deltaTime / moveRampTime;
			movementSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, movementTime * movementTime);
		}
		else
		{
			movementTime = 0;
			movementSpeed = minMoveSpeed;
		}

		// Rotation (either button)
		if (b0 || b1)
		{
			// if the game window is separate from the editor window and the editor
			// window is active then you go to right-click on the game window the
			// rotation jumps if  we don't ignore the mouseDelta for that frame.
			Vector3 mouseDelta;
			if (lastMousePosition.x >= 0
				&& lastMousePosition.y >= 0
				&& lastMousePosition.x <= Screen.width
				&& lastMousePosition.y <= Screen.height)
				mouseDelta = Input.mousePosition - lastMousePosition;
			else
				mouseDelta = Vector3.zero;

			Vector3 rotation = Vector3.up * Time.deltaTime * rotateSpeed * mouseDelta.x;
			rotation += Vector3.left * Time.deltaTime * rotateSpeed * mouseDelta.y;
			transform.Rotate(rotation, Space.Self);

			// Make sure z rotation stays locked
			rotation = transform.rotation.eulerAngles;
			rotation.z = 0;
			transform.rotation = Quaternion.Euler(rotation);
		}
		lastMousePosition = Input.mousePosition;

		// Zoom (wheel)
		cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - Input.mouseScrollDelta.y * zoomSpeed, minFOV, maxFOV);
	}
}

