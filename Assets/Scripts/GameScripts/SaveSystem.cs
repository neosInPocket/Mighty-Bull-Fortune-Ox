using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
	public static int hookSpeed => PlayerPrefs.GetInt("hookSpeed", 0);
	public static int hookDistance => PlayerPrefs.GetInt("hookDistance", 0);
	
	public static void SaveChanges()
	{
		PlayerPrefs.SetInt("hookSpeed", hookSpeed);
		PlayerPrefs.SetInt("hookDistance", hookDistance);
	}
}
