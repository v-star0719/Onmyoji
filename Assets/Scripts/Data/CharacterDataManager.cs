using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class CharacterDataManager : MonoBehaviour
{
	public static CharacterDataManager instance;
	public List<CharacterData> characterDatas = new List<CharacterData>();

	void Awake()
	{
		instance = this;
		LoadFromNative();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public CharacterData Get(int id)
	{
		foreach(var s in characterDatas)
		{
			if(s.id == id)
			{
				return s;
			}
		}

		return null;
	}

	private void LoadFromNative()
	{
		string path = Application.streamingAssetsPath + "/character_datas.json";
		var jsonData = JsonMapper.ToObject(File.ReadAllText(path));
		for(var i = 0; i < jsonData.Count; i++)
		{
			CharacterData d = new CharacterData();
			d.ReadFromJson(jsonData[i]);
			characterDatas.Add(d);
		}
	}
}
