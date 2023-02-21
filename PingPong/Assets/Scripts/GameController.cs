using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameModel
{
	public int highScore;
	public int currentBall;
	public List<int> openBalls;
}

public class GameController : MonoBehaviour
{
	[SerializeField] private UIController UI;
	[SerializeField] private Transform ballStartPlace;
	[SerializeField] private List<GameObject> ballInstances;

	[SerializeField] private SaveService saveService;

	[SerializeField] private PlayerController playerController;
	[SerializeField] private AIController aiController;

	[SerializeField] private List<int> ballsUnlockScore = new List<int> { 0, 1, 10, 100 };

	private GameObject ball;

	private GameModel currentGameModel = null;

	private int currentScore = 0;

	private void Start()
	{
		UI.GameStarted -= StartGame;
		UI.GameStarted += StartGame;
		UI.Paused -= SetPause;
		UI.Paused += SetPause;
		UI.BallSelected -= SetBall;
		UI.BallSelected += SetBall;

		currentGameModel = saveService.LoadProgress<GameModel>();
		if (currentGameModel == null)
			InitDefaultGameModel();

		UI.SetGameModel(currentGameModel);
		UI.SetUIMode(UIMode.Intro);
	}

	private void InitDefaultGameModel()
	{
		currentGameModel = new GameModel();
		currentGameModel.currentBall = 0;
		currentGameModel.highScore = 0;
		currentGameModel.openBalls = new List<int> { 0, 1 };
		saveService.SaveProgress(currentGameModel);
	}

	public void SetBall(int index)
	{
		currentGameModel.currentBall = index;
		saveService.SaveProgress(currentGameModel);
	}

	public void StartGame()
	{
		currentScore = 0;
		UI.SetScore(currentScore);

		if (ball != null)
			Destroy(ball);

		ball = Instantiate(ballInstances[currentGameModel.currentBall], transform);
		ball.transform.position = ballStartPlace.position;
		ball.GetComponent<BallController>().StartPlay(playerController.gameObject, aiController.gameObject);
		aiController.Init(ball);
		playerController.Init(ball);

		UI.SetUIMode(UIMode.Game);

		playerController.gameObject.SetActive(true);
		aiController.gameObject.SetActive(true);

		playerController.HitBall -= IncreaseScore;
		playerController.HitBall += IncreaseScore;
	}

	public void IncreaseScore()
	{
		currentScore++;
		UI.SetScore(currentScore);
		if (currentScore > currentGameModel.highScore)
		{
			currentGameModel.highScore = currentScore;

			CheckUnlockConditions();
			saveService.SaveProgress(currentGameModel);
		}
	}

	private void CheckUnlockConditions() 
	{
		for (int i = 0; i < ballsUnlockScore.Count; i++)
		{
			if (currentGameModel.openBalls.Contains(i))
				continue;

			if (ballsUnlockScore[i] <= currentGameModel.highScore)
			{
				currentGameModel.openBalls.Add(i);
			}
		}

	}

	public void SetPause(bool pause)
	{
		UI.SetUIMode(pause ? UIMode.Pause : UIMode.Game);
		playerController.gameObject.SetActive(!pause);
		aiController.gameObject.SetActive(!pause);
		ball.gameObject.SetActive(!pause);
	}
}
