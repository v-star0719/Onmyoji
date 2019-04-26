using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

class PassiveSkillConfigManager : MonoBehaviour
{
	public static PassiveSkillConfigManager instance;
	public List<PassiveSkillConfig> configs = new List<PassiveSkillConfig>();

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

	public PassiveSkillConfig Get(int id)
	{
		foreach(var s in configs)
		{
			if(s.id == id)
			{
				return s;
			}
		}

		Debug.LogError("PassiveSkillConfig not found " + id);
		return null;
	}

	private void LoadFromNative()
	{
		string path = Application.streamingAssetsPath + "/passive_skill_configs.json";
		var jsonData = JsonMapper.ToObject(File.ReadAllText(path));
		for(var i = 0; i < jsonData.Count; i++)
		{
			PassiveSkillConfig d = new PassiveSkillConfig();
			d.ReadFromJson(jsonData[i]);
			configs.Add(d);
		}
	}
}
