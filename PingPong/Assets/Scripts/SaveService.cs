using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveService : MonoBehaviour
{
	public void SaveProgress(string projId, object model)
	{
		string dataJson = JsonConvert.SerializeObject(model);
		SaveProgress(projId, dataJson);
	}

	public T LoadProgress<T>(string projId)
	{
		var dataJson = LoadProgress(projId);
		return JsonConvert.DeserializeObject<T>(dataJson);
	}

	public void SaveProgress(string projId, string dataJson)
	{
		Debug.Log($"SatelliteDataSaver_file: save progress to {Application.persistentDataPath}/{projId}.txt \n saved data: {dataJson}");
		FilesUtility.SaveTextFile(dataJson);
	}

	public string LoadProgress(string projId)
	{
		Debug.Log($"SatelliteDataSaver_file: load progress from {Application.persistentDataPath}/{projId}.txt");
		return FilesUtility.LoadTextFile();
	}
}
