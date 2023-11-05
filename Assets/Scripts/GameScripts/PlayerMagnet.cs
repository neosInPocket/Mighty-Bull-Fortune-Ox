using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	public PlayerController Player => playerController;
	
	public void Update()
	{
		transform.position = playerController.transform.position;
	}
}
