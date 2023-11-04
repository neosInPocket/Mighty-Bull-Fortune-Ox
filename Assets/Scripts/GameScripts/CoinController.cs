using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	[SerializeField] private GameObject deathPop;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private int coinPoints;
	public int CoinPoints => coinPoints;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	public bool isDead;
	
	
	public void PopCoin()
	{
		StartCoroutine(PopCoinCoroutine());
	}
	
	private IEnumerator PopCoinCoroutine()
	{
		spriteRenderer.color = new Color(0, 0, 0, 0);
		var effect = Instantiate(deathPop, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}
