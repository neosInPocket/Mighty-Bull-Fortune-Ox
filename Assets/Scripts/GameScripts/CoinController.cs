using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private int coinPoints;
	public int CoinPoints => coinPoints;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
}
