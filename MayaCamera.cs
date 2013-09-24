using UnityEngine;
using System.Collections;

public class MayaCamera : MonoBehaviour {

	Vector3 lookAtPosition;

	public Vector3 defaultLookAtPosition = Vector3.zero;

	public float tumbleSpeed	= 20f;
	public float dollySpeed		= 0.2f;
	public float trackSpeed		= 0.2f;

	public float transitTime = 1.0f;

	public Vector3 mouseSpeed		{ get; private set; }
	public Vector3 mouseAccel		{ get; private set; }
	public Vector3 mousePosition	{ get; private set; }

	Vector3 prevMousePosition;
	Vector3 prevMouseSpeed;
	Vector3 cameraToLookAtVector;
	float log10VectorLength;

	// Use this for initialization
	void Start () {
		lookAtPosition = defaultLookAtPosition;
		mousePosition = Input.mousePosition;
		prevMousePosition = mousePosition;
		mouseSpeed = Vector3.zero;
		mouseAccel = Vector3.zero;
		gameObject.transform.LookAt(lookAtPosition);
	}
	
	// Update is called once per frame
	void Update () {
		CalculateMousePhisics();
		CalculateCameraPhisics();
		Tumble();
		Dolly();
		Track();
		gameObject.transform.LookAt(lookAtPosition);
	}

	void Tumble()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
		{
			// 左クリック, tumble
			Quaternion rotation = Quaternion.LookRotation(cameraToLookAtVector);
			gameObject.transform.rotation = rotation;
			gameObject.transform.RotateAround(lookAtPosition, Vector3.up,					 mouseSpeed.x * tumbleSpeed);
			gameObject.transform.RotateAround(lookAtPosition, gameObject.transform.right, -mouseSpeed.y * tumbleSpeed);
		}
	}

	void Dolly()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
		{
			// 右クリック, dolly
			float move_power = 
				(mouseSpeed.x - mouseSpeed.y) * dollySpeed * log10VectorLength;
			gameObject.transform.Translate(0, 0, move_power);
		}
	}

	void Track()
	{
		if (
			(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(2)) ||
			(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0) && Input.GetMouseButton(1)))
		{
			// 中央クリック, track
			var rotate = gameObject.transform.rotation;
			var speed_vec = rotate * (mouseSpeed * trackSpeed);
			gameObject.transform.localPosition -= speed_vec * log10VectorLength;
			lookAtPosition -= speed_vec * log10VectorLength;
		}
	}

	void CalculateCameraPhisics()
	{
		cameraToLookAtVector = lookAtPosition - gameObject.transform.position;
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

	

	TransitStatus lookatTransitStatus = new TransitStatus();
	TransitStatus gotoTransitStatus = new TransitStatus();

	public bool LookAtHere(Vector3 lookat)
	{
		return lookatTransitStatus.Transit(ref lookat, ref lookAtPosition, transitTime, out lookAtPosition);
	}

	public bool GotoHere(Vector3 gotoHere)
	{
		var position = transform.position;
		bool flag = gotoTransitStatus.Transit(ref gotoHere, ref position, transitTime, out position);
		transform.position = position;
		return flag;
	}
}
