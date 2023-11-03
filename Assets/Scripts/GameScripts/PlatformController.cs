using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	[SerializeField] private bool isPreSpawned = false;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private SpriteRenderer[] spikes;
	[SerializeField] private float coinSpawnDelta;
	[SerializeField] private CoinController rareCoin; 
	[SerializeField] private CoinController simpleCoin; 
	[SerializeField] private float coinSpawnChance;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	private bool isSpikesSpawned;
	private Vector2 coinSpawnPosition;
	public Vector2 CoinSpawnPosition => coinSpawnPosition;
	public bool HasSpikes => spikes[0].gameObject.activeSelf;
	
	private void Start()
	{
		if (isPreSpawned)
		{
			SpawnSpikes();
		}
	}
	
	public void SetRandomPositionNSize(Vector2 platformXSizeBounds, Vector2 screenSize, float currentYSpawnPosition, Transform coinContainer)
	{
		float platformAspect = SpriteRenderer.size.x / SpriteRenderer.size.y;
		float platformXSize = Random.Range(platformXSizeBounds.x, platformXSizeBounds.y);
		
		SpriteRenderer.size = new Vector2(
			platformXSize,
			platformXSize / platformAspect
		);
		
		float randomX1Position = Random.Range(- screenSize.x - SpriteRenderer.size.x / 4, - screenSize.x + SpriteRenderer.size.x);
		float randomX2Position = Random.Range(screenSize.x + SpriteRenderer.size.x / 4, screenSize.x - SpriteRenderer.size.x);
		var rnd = Random.Range(0, 2);
		
		if (rnd == 0)
		{
			transform.position = new Vector2(randomX1Position, currentYSpawnPosition);
		}
		else
		{
			transform.position = new Vector2(randomX2Position, currentYSpawnPosition);
		}
		
		SpawnSpikes();
		SpawnCoin(coinContainer);
	}
	
	private void SpawnSpikes()
	{
		var rnd = Random.Range(0, 2);
		if (rnd == 0)
		{
			isSpikesSpawned = true;
			foreach (var spike in spikes)
			{
				spike.gameObject.SetActive(true);
			}
			
			spikes[0].size = new Vector2(spriteRenderer.size.x, spikes[0].size.y);
			spikes[0].transform.position = new Vector2(transform.position.x, transform.position.y + spriteRenderer.size.y / 2 + spikes[0].size.y / 2 * 0.24f);
			
			spikes[1].size = new Vector2(spriteRenderer.size.x, spikes[1].size.y);
			spikes[1].transform.position = new Vector2(transform.position.x, transform.position.y - spriteRenderer.size.y / 2 - spikes[1].size.y / 2 * 0.24f);
			
			spikes[2].size = new Vector2(spriteRenderer.size.y, spikes[2].size.x);
			spikes[2].transform.position = new Vector2(transform.position.x - spriteRenderer.size.x / 2 - spikes[0].size.y * 0.24f / 2, transform.position.y);
			
			spikes[3].size = new Vector2(spriteRenderer.size.y, spikes[3].size.x);
			spikes[3].transform.position = new Vector2(transform.position.x + spriteRenderer.size.x / 2 + spikes[3].size.y * 0.24f / 2, transform.position.y);
		}
	}
	
	private void SpawnCoin(Transform coinContainer)
	{
		var random = Random.Range(0, 1f);
		if (random < coinSpawnChance)
		{
			CoinController coinPrefab;
			
			if (isSpikesSpawned)
			{
				coinPrefab = rareCoin;
			}
			else
			{
				coinPrefab = simpleCoin;
			}
			
			float xSpawnPosition = 0;
			if (transform.position.x < 0)
			{
				xSpawnPosition = transform.position.x + spriteRenderer.size.x / 2 - coinPrefab.SpriteRenderer.size.x / 2;
			} 
			else
			{
				xSpawnPosition = transform.position.x - spriteRenderer.size.x / 2 + coinPrefab.SpriteRenderer.size.x / 2;
			}
			
			float ySpawnPosition = transform.position.y + spriteRenderer.size.y / 2 + spikes[0].size.y / 2 + coinPrefab.SpriteRenderer.size.y / 2 + coinSpawnDelta;
			var spawnPos = new Vector2(xSpawnPosition, ySpawnPosition);
			coinSpawnPosition = spawnPos;
			Instantiate(coinPrefab, spawnPos, Quaternion.identity, coinContainer);
		}
	}
}
