using UnityEngine;
using System.Collections;

public class MayaCamera : MonoBehaviour {

	Vector3 lookAtPosition;

	public float tumbleSpeed	= 1f;
	public float dollySpeed		= 1f;
	public float trackSpeed		= 0.2f;
	public Vector3 mouseSpeed;
	public Vector3 mouseAccel;

	Vector3 mousePosition;
	Vector3 prevMousePosition;
	Vector3 prevMouseSpeed;
	Vector3 cameraToLookAtVector;
	float log10VectorLength;
	

	// Use this for initialization
	void Start () {
		lookAtPosition = Vector3.zero;
		mousePosition = Input.mousePosition;
		prevMousePosition = mousePosition;
		mouseSpeed = Vector3.zero;
		mouseAccel = Vector3.zero;
		Camera.main.transform.LookAt(lookAtPosition);
	}
	
	// Update is called once per frame
	void Update () {
		CalculateMousePhisics();
		CalculateCameraPhisics();
		Tumble();
		Dolly();
		Track();
		Camera.main.transform.LookAt(lookAtPosition);
	}

	void Tumble()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
		{
			// 左クリック, tumble
			Quaternion rotation = Quaternion.LookRotation(cameraToLookAtVector);
			Camera.main.transform.rotation = rotation;
			Camera.main.transform.RotateAround(lookAtPosition, Vector3.up,					 mouseSpeed.x * tumbleSpeed);
			Camera.main.transform.RotateAround(lookAtPosition, Camera.main.transform.right, -mouseSpeed.y * tumbleSpeed);
		}
	}

	void Dolly()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
		{
			// 右クリック, dolly
			float move_power = 
				(mouseSpeed.x - mouseSpeed.y) * dollySpeed * log10VectorLength;
			Camera.main.transform.Translate(0, 0, move_power);
		}
	}

	void Track()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(2))
		{
			// 中央クリック, track
			var rotate = Camera.main.transform.rotation;
			var speed_vec = rotate * (mouseSpeed * trackSpeed);
			Camera.main.transform.localPosition -= speed_vec * log10VectorLength;
			lookAtPosition -= speed_vec * log10VectorLength;
		}
	}

	void CalculateCameraPhisics()
	{
		cameraToLookAtVector = lookAtPosition - Camera.main.transform.position;
		log10VectorLength = Mathf.Log10(cameraToLookAtVector.sqrMagnitude) + 1f;
	}

	void CalculateMousePhisics()
	{
		// マウスの速度加速度等を計算
		prevMousePosition	= mousePosition;
		mousePosition		= Input.mousePosition;
		prevMouseSpeed		= mouseSpeed;
		mouseSpeed = (mousePosition - prevMousePosition) * Time.deltaTime;
		mouseAccel = (mouseSpeed    - prevMouseSpeed)    * Time.deltaTime;
	}
}
