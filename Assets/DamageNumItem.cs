using UnityEngine;
using System.Collections;

public class DamageNumItem : MonoBehaviour
{
	public Characer characer;
	private Vector3 offset;

	public UILabel numLabel;

	private float timer = 1;
	public bool isFinished;

	public void SetData(Characer c, int number)
	{
		characer = c;
		offset = new Vector3(1f, 2, 0);
		numLabel.text = number.ToString();
		isFinished = false;
	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (isFinished)
		{
			return;
		}

		if (characer.isDestoryed)
		{
			isFinished = true;
			return;
		}

		var pos = Camera.main.WorldToScreenPoint(characer.transform.position + offset);
		pos = UICamera.list[0].cachedCamera.ScreenToWorldPoint(pos);
		pos.z = 0;
		transform.position = pos;

		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			isFinished = true;
		}
	}
}
