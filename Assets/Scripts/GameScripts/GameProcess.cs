using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProcess : MonoBehaviour
{
	[SerializeField] private PlatformSpawner platformSpawner;
	[SerializeField] private PlayerController player;
	[SerializeField] private CDWindow cDWindow;
	[SerializeField] private TutorScreen tutorScreen;
	[SerializeField] private SessionResult sessionResult;
	[SerializeField] private GameProgressionRenderer progressionRenderer;
	private int currentProgression;
	private int maxProgression;
	private int currentLevel;
	private int currentLevelMaxCoins;
	
	private int GetMaxProgressionFunction()
	{
		return (int)(5 * Mathf.Log(currentLevel) + 5);
	}
	
	private int GetMaxLevelCoins()
	{
		return (int)(10 * Mathf.Log(currentLevel) + 20);
	}
	
	private void Start()
	{
		StartNewGame();
	}
	
	public void StartNewGame()
	{
		currentLevel = SaveSystem.level;
		player.CoinCollectedEvent += OnCoinCollected;
		player.TakeDamageEvent += OnPlayerDamageTaken;
		platformSpawner.ClearContainers();
		platformSpawner.Initialize();
		player.Initialize();
		currentProgression = 0;
		currentLevelMaxCoins = GetMaxLevelCoins();
		maxProgression = GetMaxProgressionFunction();
		progressionRenderer.SetLevelText(currentLevel);
		progressionRenderer.RefreshHearts(player.Lifes);
		progressionRenderer.ClearProgression();
		
		if (SaveSystem.tutorial)
		{
			SaveSystem.tutorial = false;
			tutorScreen.End += OnTutorialEnd;
			tutorScreen.gameObject.SetActive(true);
		}
		else
		{
			OnTutorialEnd();
		}
	}
	
	private void OnTutorialEnd()
	{
		tutorScreen.End -= OnTutorialEnd;
		cDWindow.CDWindowEnd += CDEndHandler;
		cDWindow.gameObject.SetActive(true);
	}
	
	
	private void CDEndHandler()
	{
		cDWindow.CDWindowEnd -= CDEndHandler;
		player.EnableHook();
	}
	
	private void OnCoinCollected(int value)
	{
		if (value + currentProgression >= maxProgression)
		{
			currentProgression = maxProgression;
			sessionResult.gameObject.SetActive(true);
			sessionResult.RefreshResultInfo(false, currentLevelMaxCoins);
			UnsubscribeFromPlayer();
			player.DisableHook();
			SaveSystem.level++;
			SaveSystem.coins += currentLevelMaxCoins;
		}
		else
		{
			currentProgression += value;
		}
		
		
		
		progressionRenderer.RefreshProgression((float)currentProgression / (float)maxProgression);
	}
	
	private void OnPlayerDamageTaken(int currentLifes)
	{
		if (currentLifes <= 0)
		{
			sessionResult.gameObject.SetActive(true);
			sessionResult.RefreshResultInfo(true);
			UnsubscribeFromPlayer();
			player.DisableHook();
		}
		
		progressionRenderer.RefreshHearts(currentLifes);
	}
	
	private void UnsubscribeFromPlayer()
	{
		player.CoinCollectedEvent -= OnCoinCollected;
		player.TakeDamageEvent -= OnPlayerDamageTaken;
	}
	
	private void OnDestroy()
	{
		UnsubscribeFromPlayer();
	}
	
	public void BackToMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
