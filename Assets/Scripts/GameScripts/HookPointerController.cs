using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointerController : MonoBehaviour
{
	[SerializeField] private float hookSpeed = 0.01f;
	private int directionMultiplier;
	private bool isPointerEnabled;
	private float currentTime;
	
	private void Start()
	{
		currentTime = 0;
		directionMultiplier = 1;
		EnablePointerRotation();
	}
	
	public void EnablePointerRotation()
	{
		isPointerEnabled = true;
	}
	
	public void DisablePointerRotation()
	{
		isPointerEnabled = false;
	}
	
	private void Update()
	{
		if (!isPointerEnabled) return;
		
		float rotation = 90 * Mathf.Sin(currentTime / 8 - Mathf.PI / 2) + 90;
		transform.rotation = Quaternion.Euler(0, 0, rotation);
		currentTime += Time.deltaTime * hookSpeed;
	}
}
