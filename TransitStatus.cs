using UnityEngine;
using System.Collections;

public class TransitStatus
{
	bool flag;
	Vector3 to;
	Vector3 from;
	float timeTo;

	float time;
	public float Time { get { return time; } }

	public TransitStatus()
	{
		flag = false;
		to = Vector3.zero;
		from = Vector3.zero;
		timeTo = 0f;
		time = 0f;
	}

	void Init(ref Vector3 to, ref Vector3 from, float timeTo)
	{
		if (!flag)
		{
			flag = true;
			this.to = to;
			this.from = from;
			time = 0f;
			this.timeTo = timeTo;
		}
	}

	Vector3 Transit()
	{
		float value = Mathf.Sin((time / timeTo) * (Mathf.PI * 2));
		Vector3 here = Vector3.Slerp(from, to, value);
		time += UnityEngine.Time.deltaTime;
		return here;
	}

	bool JudgeFlag()
	{
		if (time >= timeTo)
			flag = false;
		return flag;
	}

	public bool Transit(ref Vector3 to, ref Vector3 from, float timeTo, out Vector3 position)
	{
		Init(ref to, ref from, timeTo);
		position = Transit();
		return JudgeFlag();
	}
}