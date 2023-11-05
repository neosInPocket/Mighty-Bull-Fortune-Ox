using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Rigidbody2D rigidbody2Dd;
	[SerializeField] private PlayerUpgradesSO playerUpgrades;
	[SerializeField] private ChainGun chainGun; 
	[SerializeField] private ParticleSystem scanSystem; 
	[SerializeField] private GameObject deathEffect;
	private PlatformController currentSavePlatform;
	public Action<int> TakeDamageEvent;
	public Action<int> CoinCollectedEvent;
	private int lifes;
	public int Lifes => lifes;
	private bool isDead;
	private bool isInvincible;
	
	private void Start()
	{
		chainGun.MaxGrappleDistance = playerUpgrades.HookDistances[SaveSystem.hookDistance];
		var mainModule = scanSystem.main;
		mainModule.startSize = chainGun.MaxGrappleDistance * 2;
		chainGun.DisableHook();
	}
	
	public void EnableHook() => chainGun.EnableHook();
	public void DisableHook() => chainGun.DisableHook();
	
	public void Initialize()
	{
		lifes = SaveSystem.maxLifesAmount;
		transform.position = new Vector2(0, -4.16f);
		rigidbody2Dd.velocity = Vector2.zero;
		rigidbody2Dd.angularVelocity = 0;
		isDead = false;
		spriteRenderer.color = new Color(1, 1, 1, 1);
		transform.rotation = Quaternion.Euler(0, 0, 90);
		scanSystem.gameObject.SetActive(true);
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (isDead) return;
		
		if (collider.gameObject.TryGetComponent<CoinController>(out CoinController coin))
		{
			if (coin.isDead) return;
			coin.isDead	= true;
			coin.PopCoin();
			CoinCollectedEvent?.Invoke(coin.CoinPoints);
		}
		
		if (collider.gameObject.TryGetComponent<PlatformSpikes>(out PlatformSpikes platformSpikes))
		{
			if (isInvincible) return;
			TakeDamage();
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.TryGetComponent<PlatformController>(out PlatformController platformController))
		{
			if (!platformController.HasSpikes)
			{
				currentSavePlatform = platformController;
			}
		}
	}
	
	private void TakeDamage()
	{
		lifes--;
		TakeDamageEvent?.Invoke(lifes);
		
		if (lifes != 0)
		{
			isInvincible = true;
			if (currentSavePlatform == null)
			{
				transform.position = new Vector2(0, -4.16f);
				rigidbody2Dd.velocity = Vector2.zero;
				rigidbody2Dd.angularVelocity = 0;
			}
			else
			{
				chainGun.isLaunched = false;
				transform.position = currentSavePlatform.CoinSpawnPosition;
			}
			StartCoroutine(SpriteRendererFade());
		}
		else
		{
			chainGun.isLaunched = false;
			isDead = true;
			StartCoroutine(SpriteDeathEffect());
		}
	}
	
	private IEnumerator SpriteRendererFade()
	{
		var fadeColor = new Color(1, 1, 1, 0);
		var normalColor = new Color(1, 1, 1, 1);
		
		for (int i = 0; i < 11; i++)
		{
			spriteRenderer.color = fadeColor;
			yield return new WaitForSeconds(0.2f);
			spriteRenderer.color = normalColor;
			yield return new WaitForSeconds(0.2f);
		}
		
		isInvincible = false;
	}
	
	private IEnumerator SpriteDeathEffect()
	{
		scanSystem.gameObject.SetActive(false);
		spriteRenderer.color = new Color(1, 1, 1, 0);
		var deathGO = Instantiate(deathEffect, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1);
		Destroy(deathGO.gameObject);
	}
}
