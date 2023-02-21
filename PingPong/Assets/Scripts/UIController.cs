using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIMode
{
	Intro,
	Game,
	Pause
}
public class UIController : MonoBehaviour
{
	[SerializeField] private List<Button> colorButtons;
	[SerializeField] private BallColorer ballColorer;
	[SerializeField] private Button startButton;
	[SerializeField] private Button pauseButton;
	[SerializeField] private Button resumeButton;
	[SerializeField] private Text highScoreText;
	[SerializeField] private Text highScoreCount;
	[SerializeField] private Text currentScoreCount;

	private UIMode mode = UIMode.Intro;
	private GameModel currentGameModel;

	public event Action<int> BallSelected;
	public event Action<bool> Paused;
	public event Action GameStarted;

	public void SelectColor(int index)
	{
		ballColorer.SetColorInstant(colorButtons[index].colors.highlightedColor);
		BallSelected?.Invoke(index);
	}

	public void SetGameModel(GameModel model) 
	{
		currentGameModel = model;
	}

	public void StartGame()
	{
		GameStarted?.Invoke();
	}

	public void Pause()
	{
		Paused?.Invoke(true);
	}

	public void Resume()
	{
		Paused?.Invoke(false);
	}

	public void SetScore(int score)
	{
		currentScoreCount.text = score.ToString();
	}

	private void SetHighScore(int highScore)
	{
		highScoreCount.text = highScore.ToString();
	}
	private void SetEnabledColors(List<int> enabledColors)
	{
		foreach (var cB in colorButtons)
			cB.gameObject.SetActive(true);

		for (int i = 0; i < colorButtons.Count; i++)
		{
			colorButtons[i].interactable = enabledColors.Contains(i);
		}
	}

	public void SetUIMode(UIMode mode)
	{
		this.mode = mode;
		if (mode == UIMode.Intro)
		{
			startButton.gameObject.SetActive(true);
			pauseButton.gameObject.SetActive(false);
			resumeButton.gameObject.SetActive(false);
			highScoreText.gameObject.SetActive(true);
			highScoreCount.gameObject.SetActive(true);
			currentScoreCount.gameObject.SetActive(false);
			ballColorer.gameObject.SetActive(true);
			SetEnabledColors(currentGameModel.openBalls);
			SetHighScore(currentGameModel.highScore);
		}
		else if (mode == UIMode.Pause)
		{
			startButton.gameObject.SetActive(true);
			pauseButton.gameObject.SetActive(false);
			resumeButton.gameObject.SetActive(true);
			highScoreText.gameObject.SetActive(true);
			highScoreCount.gameObject.SetActive(true);
			currentScoreCount.gameObject.SetActive(false);
			ballColorer.gameObject.SetActive(true);
			SetEnabledColors(currentGameModel.openBalls);
			SetHighScore(currentGameModel.highScore);
		}
		else if (mode == UIMode.Game)
		{
			startButton.gameObject.SetActive(false);
			pauseButton.gameObject.SetActive(true);
			resumeButton.gameObject.SetActive(false);
			highScoreText.gameObject.SetActive(false);
			highScoreCount.gameObject.SetActive(false);
			currentScoreCount.gameObject.SetActive(true);
			ballColorer.gameObject.SetActive(false);
			foreach (var cB in colorButtons)
				cB.gameObject.SetActive(false);

		}
	}

}
