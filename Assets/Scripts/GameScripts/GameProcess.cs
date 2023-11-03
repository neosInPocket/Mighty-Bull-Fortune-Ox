using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
	[SerializeField] private PlatformSpawner platformSpawner;
	[SerializeField] private PlayerController player;
	[SerializeField] private CDWindow cDWindow;
	
	private void Start()
	{
		StartNewGame();
	}
	
	public void StartNewGame()
	{
		player.CoinCollectedEvent += OnCoinCollected;
		player.TakeDamageEvent += OnPlayerDamageTaken;
		platformSpawner.ClearContainers();
		platformSpawner.Initialize();
		player.Initialize();
		
		if (SaveSystem.tutorial)
		{
			SaveSystem.tutorial = false;
			
		}
	}
	
	private void OnCoinCollected(int value)
	{
		
	}
	
	private void OnPlayerDamageTaken(bool value)
	{
		
	}
	
	private void EndGame()
	{
		player.CoinCollectedEvent -= OnCoinCollected;
		player.TakeDamageEvent -= OnPlayerDamageTaken;
	}
	
	private void OnDestroy()
	{
		EndGame();
	}
}
