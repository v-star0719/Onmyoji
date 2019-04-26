using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
	public static Stage instance;

	public Vector3 playerPos;
	public Vector3 bossPos;
	public float radius = 2;

	public int drawGizmosFormationIndex = 0;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
		if (drawGizmosFormationIndex <= 0)
		{
			drawGizmosFormationIndex = 1;
		}

		if (drawGizmosFormationIndex > 6)
		{
			drawGizmosFormationIndex = 6;
		}

		Gizmos.color = Color.green;
		Gizmos.DrawSphere(playerPos, 0.2f);
		DrawPosGizmos(drawGizmosFormationIndex, false);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(bossPos, 0.2f);
		DrawPosGizmos(drawGizmosFormationIndex, true);
	}

	void DrawPosGizmos(int formationSize, bool isMonster)
	{
		float angle = 180f / (formationSize + 1) * Mathf.Deg2Rad;
		if (isMonster)
		{
			angle = -angle;
		}

		var center = isMonster ? bossPos : playerPos;
		for (int i = 1; i <= formationSize; i++)
		{
			Vector3 v = center;
			v.z += radius * Mathf.Sin(angle * i);
			v.x += radius * Mathf.Cos(angle * i);
			Gizmos.DrawSphere(v, 0.2f);
		}
	}

	public Vector3 GetshikigamiPos(int formationSize, int index)
	{
		if (index == 0)
		{
			return playerPos;
		}
		float angle = 180f / formationSize * Mathf.Deg2Rad;
		Vector3 v = playerPos;
		v.z += radius * Mathf.Sin(angle * index);
		v.x += radius * Mathf.Cos(angle * index);
		return v;
	}

	public Vector3 GetMonsterPos(int formationSize, int index)
	{
		if(index == 0)
		{
			return bossPos;
		}
		float angle = -180f / formationSize * Mathf.Deg2Rad;
		Vector3 v = bossPos;
		v.z += radius * Mathf.Sin(angle * index);
		v.x += radius * Mathf.Cos(angle * index);
		return v;
	}
}
