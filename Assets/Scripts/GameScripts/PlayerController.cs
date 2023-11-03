using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerUpgradesSO playerUpgrades;
	[SerializeField] private ChainGun chainGun; 
	[SerializeField] private ParticleSystem scanSystem; 
	public Action<bool> TakeDamageEvent;
	public Action<int> CoinCollectedEvent;
	private int lifes;
	public int Lifes => lifes;
	
	private void Start()
	{
		chainGun.MaxGrappleDistance = playerUpgrades.HookDistances[SaveSystem.hookDistance];
		var mainModule = scanSystem.main;
		mainModule.startSize = chainGun.MaxGrappleDistance * 2;
	}
	
	public void Initialize()
	{
		lifes = SaveSystem.maxLifesAmount;
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<PlatformSpikes>(out PlatformSpikes platformSpikes))
		{
			TakeDamage();
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.TryGetComponent<CoinController>(out CoinController coin))
		{
			CoinCollectedEvent?.Invoke(coin.CoinPoints);
		}
	}
	
	private void TakeDamage()
	{
		if (lifes - 1 == 0)
		{
			lifes = 0;
			TakeDamageEvent?.Invoke(true);
		}
		else
		{
			lifes--;
			TakeDamageEvent?.Invoke(false);
		}
	}
}
