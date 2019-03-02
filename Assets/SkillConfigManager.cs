using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class SkillConfigManager : MonoBehaviour
{
	public static SkillConfigManager instance;
	public List<SkillConfig> skillDatas = new List<SkillConfig>();
	
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

	public SkillConfig Get(int id)
	{
		foreach (var s in skillDatas)
		{
			if (s.id == id)
			{
				return s;
			}
		}
		
		return null;
	}

	private void LoadFromNative()
	{
		string path  = Application.streamingAssetsPath + "/skill_configs.json";
		var jsonData = JsonMapper.ToObject(File.ReadAllText(path));
		for (var i = 0; i < jsonData.Count; i++)
		{
			SkillConfig d = new SkillConfig();
			d.ReadFromJson(jsonData[i]);
			skillDatas.Add(d);
		}
	}
}
