using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInfoItem : UIDynamicListItemCtrlBase
{
	public CharacterData data;
	public UILabel nameLabel;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void SetData(int index, object dataList)
	{
		List<CharacterData> list = dataList as List<CharacterData>;
		data = list[index];
		nameLabel.text = data.name;
	}
}
