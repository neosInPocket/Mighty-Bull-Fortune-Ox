using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoundsColliders : MonoBehaviour
{
	[SerializeField] private Transform cameraTransform;
	[SerializeField] private SpriteRenderer leftCollider;
	[SerializeField] private SpriteRenderer rightCollider;
	
	private void Start()
	{
		var screenSize = new Vector2(Screen.width, Screen.height);
		var worldScreenSize = Camera.main.ScreenToWorldPoint(screenSize);
		
		leftCollider.size = new Vector2(leftCollider.size.x, 5 * worldScreenSize.y);
		leftCollider.transform.position = new Vector2(- worldScreenSize.x - leftCollider.size.x / 2, cameraTransform.position.y);
		
		rightCollider.size = new Vector2(rightCollider.size.x, 5 * worldScreenSize.y);
		rightCollider.transform.position = new Vector2(worldScreenSize.x + rightCollider.size.x / 2, cameraTransform.position.y);
	}
}
