using System;
using UnityEngine;

public class CDWindow : MonoBehaviour
{
	public Action CDWindowEnd;
	
	public void InvokeEvent()
	{
		CDWindowEnd?.Invoke();
	}
}
