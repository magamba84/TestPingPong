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
		//Debug.Log($"SatelliteDataSaver_file: save progress to {Application.persistentDataPath}/{projId}.txt \n saved data: {dataJson}");
		FilesUtility.SaveTextFile(dataJson);
	}

	public string LoadProgress()
	{
		//Debug.Log($"SatelliteDataSaver_file: load progress from {Application.persistentDataPath}/{projId}.txt");
		return FilesUtility.LoadTextFile();
	}
}
