using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
	[SerializeField] private PlatformSpawner platformSpawner;
	
	private void Start()
	{
		StartNewGame();
	}
	
	public void StartNewGame()
	{
		platformSpawner.ClearPlatformsContainer();
		platformSpawner.Initialize();
	}
}
