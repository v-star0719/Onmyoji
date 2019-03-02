using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class CharacterConfigManager : MonoBehaviour
{
	public static CharacterConfigManager instance;
	public List<CharacterConfig> configs = new List<CharacterConfig>();

	void Awake()
	{
		instance = this;
		LoadFromNative();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public CharacterConfig Get(int id)
	{
		foreach(var s in configs)
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
		string path = Application.streamingAssetsPath + "/character_configs.json";
		var jsonData = JsonMapper.ToObject(File.ReadAllText(path));
		for(var i = 0; i < jsonData.Count; i++)
		{
			CharacterConfig d = new CharacterConfig();
			d.ReadFromJson(jsonData[i]);
			configs.Add(d);
		}
	}
}