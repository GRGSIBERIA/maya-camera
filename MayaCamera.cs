using UnityEngine;
using System.Collections;

public class MayaCamera : MonoBehaviour {

	Vector3 lookAtPosition;

	public float dollySpeed		= 0.1f;
	public float tumbleSpeed	= 1f;
	public float trackSpeed		= 1f;

	Vector3 prevMousePosition;
	Vector3 prevMouseSpeed;
	public Vector3 mouseSpeed;
	public Vector3 mouseAccel;

	// Use this for initialization
	void Start () {
		lookAtPosition = Vector3.zero;
		prevMousePosition = Input.mousePosition;
		mouseSpeed = Vector3.zero;
		mouseAccel = Vector3.zero;
		Camera.main.transform.LookAt(lookAtPosition);
	}
	
	// Update is called once per frame
	void Update () {
		CalculateMousePhisics();
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
			Vector3 inverse_vector = lookAtPosition - Camera.main.transform.position;
			Quaternion rotation = Quaternion.LookRotation(inverse_vector);
			Camera.main.transform.rotation = rotation;
			Camera.main.transform.RotateAround(lookAtPosition, Vector3.up,					 mouseSpeed.x);
			Camera.main.transform.RotateAround(lookAtPosition, Camera.main.transform.right, -mouseSpeed.y);
		}
	}

	void Dolly()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
		{
			// 右クリック, dolly
			var dollied_local = Camera.main.transform.localPosition;
			dollied_local.z -= (mouseSpeed.x + mouseSpeed.y) * dollySpeed;
			Camera.main.transform.localPosition = dollied_local;
		}
	}

	void Track()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(2))
		{
			// 中央クリック, track
			var speed_vec = mouseSpeed * trackSpeed;
			Camera.main.transform.localPosition -= speed_vec;
			lookAtPosition -= speed_vec;
		}
	}

	void CalculateMousePhisics()
	{
		// マウスの速度加速度等を計算
		mouseSpeed = Input.mousePosition - prevMousePosition;
		mouseAccel = mouseSpeed - prevMouseSpeed;
		prevMouseSpeed = mouseSpeed;
		prevMousePosition = Input.mousePosition;
	}
}
