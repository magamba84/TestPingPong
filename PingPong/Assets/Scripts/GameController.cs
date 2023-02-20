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
		saveService.SaveProgress(currentGameModel);
	}

	public void StartGame()
	{
		currentScore = 0;
		ball = Instantiate(ballInstances[currentGameModel.currentBall], transform);
		ball.transform.position = ballStartPlace.position;
		aiController.Init(ball);
		playerController.Init(ball);

		UI.SetUIMode(UIMode.Game);

		playerController.gameObject.SetActive(true);
		aiController.gameObject.SetActive(true);
	}

	public void IncreaseScore()
	{
		currentScore++;
		UI.SetScore(currentScore);
		if (currentScore > currentGameModel.highScore)
		{
			UI.SetHighScore(currentScore);
			currentGameModel.highScore = currentScore;
			saveService.SaveProgress(currentGameModel);
		}
	}

	public void SetPause(bool pause)
	{
		UI.SetUIMode(pause ? UIMode.Pause : UIMode.Game);
		playerController.gameObject.SetActive(!pause);
		aiController.gameObject.SetActive(!pause);
	}
}
