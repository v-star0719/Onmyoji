using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveBarItem : MonoBehaviour
{
	public UIWidget barWidget;
	public UILabel nameLabel;
	public UILabel speedLabel;

	public CharacterMoveBarData data;
	private Vector3 barStartPos = Vector3.zero;
	private float barLength = 0;

	public void SetData(CharacterMoveBarData d)
	{
		data = d;
		barStartPos.y += barWidget.height * 0.5f;
		barLength = barWidget.height;
		nameLabel.text = data.characer.Name;
		speedLabel.text = data.characer.speed.ToString("f0");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float p = data.pos / BattleProgress.TRACK_LENGTH;
		var pos = barStartPos;
		pos.y -= p * barLength;
		transform.localPosition = pos;
	}
}
