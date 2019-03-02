using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class BuffConfigManager : MonoBehaviour
{
	public static BuffConfigManager instance;
	public List<BuffConfig> configs = new List<BuffConfig>();

	void Awake()
	{
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

	public BuffConfig Get(int id)
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
		string path = Application.streamingAssetsPath + "/buff_configs.json";
		var jsonData = JsonMapper.ToObject(File.ReadAllText(path));
		for(var i = 0; i < jsonData.Count; i++)
		{
			BuffConfig d = new BuffConfig();
			d.ReadFromJson(jsonData[i]);
			configs.Add(d);
		}
	}
}

