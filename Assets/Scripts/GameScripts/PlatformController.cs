using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	
	public void SetRandomPositionNSize(Vector2 platformXSizeBounds, Vector2 screenSize, float currentYSpawnPosition)
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
	}
}
