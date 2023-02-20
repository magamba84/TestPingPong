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

public class GameController : MonoBehaviour, IPausable
{
	[SerializeField] private UIController UI;
	[SerializeField] private Transform ballStartPlace;
	[SerializeField] private List<GameObject> ballInstances;

	[SerializeField] private SaveService saveService;

	[SerializeField] private PlayerController playerController;

	private GameObject ball;

	private GameModel currentGameModel = null;

	private int currentScore = 0;

	private void Start()
	{
		UI.SetUIMode(UIMode.Intro);

		UI.GameStarted -= StartGame;
		UI.GameStarted += StartGame;
		UI.Paused -= SetPause;
		UI.Paused += SetPause;
		UI.BallSelected -= SetBall;
		UI.BallSelected += SetBall;

		currentGameModel = saveService.LoadProgress<GameModel>();
		if (currentGameModel == null)
			InitDefaultGameModel();

		UI.SetEnabledColors(currentGameModel.openBalls);
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
	}

	public void StartGame()
	{
		currentScore = 0;
		ball = Instantiate(ballInstances[currentGameModel.currentBall], transform);
		ball.transform.position = ballStartPlace.position;
		playerController.Init(ball);
		UI.SetUIMode(UIMode.Game);
	}

	public void IncreaseScore()
	{
		currentScore++;
		if (currentScore > currentGameModel.highScore)
		{
			currentGameModel.highScore = currentScore;
			saveService.SaveProgress(currentGameModel);
		}
	}

	public void SetPause(bool pause)
	{
		UI.SetUIMode(pause ? UIMode.Pause : UIMode.Game);
	}
}
