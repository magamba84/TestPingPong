using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveService : MonoBehaviour
{
	public void SaveProgress(object model)
	{
		string dataJson = JsonConvert.SerializeObject(model);
		SaveProgress(dataJson);
	}

	public T LoadProgress<T>()
	{
		var dataJson = LoadProgress();
		return JsonConvert.DeserializeObject<T>(dataJson);
	}

	public void SaveProgress(string dataJson)
	{
		FilesUtility.SaveTextFile(dataJson);
	}

	public string LoadProgress()
	{
		
		return FilesUtility.LoadTextFile();
	}
}
