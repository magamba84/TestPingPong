using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour, IPausable
{
	[SerializeField] private Transform ballStartPlace;
	[SerializeField] private List<GameObject> ballInstances;

	private void Start()
	{
		/*Debug.Log(1);
		FilesUtility.LoadTextFile();
		Debug.Log(2);
		FilesUtility.SaveTextFile("mama mila ramu");
		Debug.Log(3);
		var s = FilesUtility.LoadTextFile();
		Debug.Log(s);*/
	}

	public void StartGame() 
	{ 
	
	}

	public void IncreaseScore() 
	{ 
	
	}

	public void SetPause(bool pause)
	{ 
		
	}
}
