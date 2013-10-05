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

	TransitStatus lookatTransitStatus;
	TransitStatus gotoTransitStatus;

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
		ClickedTumble();
		ClickedDolly();
		ClickedTrack();
		MovementGoto();
		MovementLookat();
		gameObject.transform.LookAt(lookAtPosition);
	}

	void ClickedTumble()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
		{
			InvokeTumble();
		}
	}

	public void InvokeTumble()
	{
		// 左クリック, tumble
		Quaternion rotation = Quaternion.LookRotation(cameraToLookAtVector);
		gameObject.transform.rotation = rotation;
		gameObject.transform.RotateAround(lookAtPosition, Vector3.up, mouseSpeed.x * tumbleSpeed);
		gameObject.transform.RotateAround(lookAtPosition, gameObject.transform.right, -mouseSpeed.y * tumbleSpeed);
	}

	void ClickedDolly()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
		{
			InvokeDolly();
		}
	}

	public void InvokeDolly()
	{
		// 右クリック, dolly
		float move_power =
			(mouseSpeed.x - mouseSpeed.y) * dollySpeed * log10VectorLength;
		gameObject.transform.Translate(0, 0, move_power);
	}

	void ClickedTrack()
	{
		if (
			(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(2)) ||
			(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0) && Input.GetMouseButton(1)))
		{
			InvokeTrack();
		}
	}

	public void InvokeTrack()
	{
		// 中央クリック, track
		var rotate = gameObject.transform.rotation;
		var speed_vec = rotate * (mouseSpeed * trackSpeed);
		gameObject.transform.localPosition -= speed_vec * log10VectorLength;
		lookAtPosition -= speed_vec * log10VectorLength;
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

	void MovementLookat()
	{
		if (lookatTransitStatus != null)
			lookatTransitStatus.Transit(ref lookAtPosition);
	}

	void MovementGoto()
	{
		if (gotoTransitStatus != null)
		{
			var position = transform.position;
			gotoTransitStatus.Transit(ref position);
			transform.position = position;
		}
	}

	public void LookAtHere(Vector3 lookat, float timeto)
	{
		lookatTransitStatus = new TransitStatus(ref lookat, ref lookAtPosition, timeto);
	}

	public void LookAtHere(Vector3 lookat)
	{
		LookAtHere(lookat, transitTime);
	}

	public void GotoHere(Vector3 gotoHere, float timeto)
	{
		var position = transform.position;
		gotoTransitStatus = new TransitStatus(ref gotoHere, ref position, timeto);
	}

	public void GotoHere(Vector3 gotoHere)
	{
		GotoHere(gotoHere, transitTime);
	}
}
