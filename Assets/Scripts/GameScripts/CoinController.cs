using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	[SerializeField] private GameObject deathPop;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private int coinPoints;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private float magnetSpeed;
	[SerializeField] private TrailRenderer trailRenderer;
	public int CoinPoints => coinPoints;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	public bool isDead;
	private PlayerMagnet target;
	private Vector2 initialPoint;
	
	private void Start()
	{
		initialPoint = transform.position;
	}
	
	private void Update()
	{
		if (target == null) return;
		
		var direction = (target.transform.position - transform.position).normalized;
		var distance = Vector2.Distance(target.transform.position, transform.position);
		rigid.velocity = direction * magnetSpeed / (distance + 2);
	}
	
	public void PopCoin()
	{
		StartCoroutine(PopCoinCoroutine());
	}
	
	private IEnumerator PopCoinCoroutine()
	{
		spriteRenderer.color = new Color(0, 0, 0, 0);
		trailRenderer.startColor = new Color(0, 0, 0, 0);
		var effect = Instantiate(deathPop, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
	
	private void EnableMagnet(PlayerController player)
	{
		player.TakeDamageEvent += PlayerDamageEvent;
	}
	
	private void DisableMagnet(PlayerController player)
	{
		player.TakeDamageEvent -= PlayerDamageEvent;
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<PlayerMagnet>(out PlayerMagnet playerMagnet))
		{
			EnableMagnet(playerMagnet.Player);
			target = playerMagnet;
		}
	}
	
	private void PlayerDamageEvent(int lifes)
	{
		if (lifes == 0)
		{
			DisableMagnet(target.Player);
			transform.position = initialPoint;
			target = null;
			rigid.velocity = Vector2.zero;
		}
	}
}
