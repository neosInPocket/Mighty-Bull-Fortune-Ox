using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player", fileName = "PlayerSettings")]
public class PlayerUpgradesSO : ScriptableObject
{
	[SerializeField] private float[] hookDistances;
	[SerializeField] private float[] hookSpeed;
	[SerializeField] private float[] pointerSpeed;
	
	public float[] HookDistances => hookDistances;
	public float[] HookSpeed => hookSpeed;
	public float[] PointerSpeed => pointerSpeed;
}
