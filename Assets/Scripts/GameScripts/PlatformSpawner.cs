using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Vector2 platformXSizeBounds;
	[SerializeField] private PlatformController[] platformPrefabs;
	[SerializeField] private Transform lastPlatform;
	[SerializeField] private Transform firstPlatform;
	[SerializeField] private float spawnDelta;
	private float currentYSpawnPosition;
	private float currentYSpawnTrigger;
	private Vector2 screenSize;
	
	private void Start()
	{
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		Initialize();
	}
	
	private void Update()
	{
		if (player.position.y > currentYSpawnTrigger)
		{
			Spawn();
		}
	}
	
	public void Initialize()
	{
		currentYSpawnPosition = lastPlatform.position.y + spawnDelta;
		currentYSpawnTrigger = firstPlatform.transform.position.y;
	}
	
	public void Spawn()
	{
		var platform = Instantiate(
			platformPrefabs[Random.Range(0, platformPrefabs.Length)],
			Vector2.zero,
			Quaternion.identity,
			transform
			);
			
		platform.SetRandomPositionNSize(platformXSizeBounds, screenSize, currentYSpawnPosition);
		currentYSpawnPosition = platform.transform.position.y + spawnDelta;
		currentYSpawnTrigger += spawnDelta;
	}
	
	public void ClearPlatformsContainer()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
	}
}
