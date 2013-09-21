using UnityEngine;
using System.Collections;

public class MayaCamera : MonoBehaviour {

	public float dollySpeed		= 1f;
	public float tumbleSpeed	= 1f;
	public float trackSpeed		= 1f;

	Vector3 prevMousePosition;
	Vector3 prevMouseSpeed;
	public Vector3 mouseSpeed;
	public Vector3 mouseAccel;

	// Use this for initialization
	void Start () {
		prevMousePosition = Input.mousePosition;
		mouseSpeed = Vector3.zero;
		mouseAccel = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateMousePhisics();
		Tumble();
		Dolly();
		Track();
	}

	void Tumble()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
		{
			// ���N���b�N, tumble
			
		}
	}

	void Dolly()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
		{
			// �E�N���b�N, dolly
			var dollied_local = Camera.main.transform.localPosition;
			dollied_local.z -= mouseSpeed.y * dollySpeed;
			Camera.main.transform.localPosition = dollied_local;
		}
	}

	void Track()
	{
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(2))
		{
			// �����N���b�N, track
			Camera.main.transform.localPosition -= mouseSpeed;
		}
	}

	void CalculateMousePhisics()
	{
		// �}�E�X�̑��x�����x�����v�Z
		mouseSpeed = Input.mousePosition - prevMousePosition;
		mouseAccel = mouseSpeed - prevMouseSpeed;
		prevMouseSpeed = mouseSpeed;
		prevMousePosition = Input.mousePosition;
	}
}
