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

	public TransitStatus(ref Vector3 to, ref Vector3 from, float timeTo)
	{
		flag = true;
		this.to = to;
		this.from = from;
		time = 0f;
		this.timeTo = timeTo;
	}

	Vector3 Transit()
	{
		float value = Mathf.Sin((time / timeTo) * (Mathf.PI * 0.5f));
		Vector3 here = Vector3.Slerp(from, to, value);
		time += UnityEngine.Time.deltaTime;
		return here;
	}

	bool JudgeTiming()
	{
		if (time >= timeTo)
			return false;
		return true;
	}

	public void Transit(ref Vector3 position)
	{
		if (flag)
		{
			position = Transit();
			flag = JudgeTiming();
		}
	}
}