using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody2d;
	public Rigidbody2D Rigidbody => rigidbody2d;
	public Action OnPlatformTouched;
	
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		var platform = collision.gameObject.GetComponent<PlatformController>();
		var playerCollider = collision.otherCollider.gameObject.GetComponent<PlayerController>();
		
		if (platform && playerCollider)
		{
			OnPlatformTouched?.Invoke();
		}
	}
}
