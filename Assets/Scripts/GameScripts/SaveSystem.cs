using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
	public static int hookDistance
	{
		get
		{
			 return PlayerPrefs.GetInt("hookDistance", 0);
		}
		
		set
		{
			m_hookDistance = value;
			PlayerPrefs.SetInt("hookDistance", m_hookDistance);
		}
	}
	private static int m_hookDistance;
	
	public static int maxLifesAmount
	{
		get
		{
			return PlayerPrefs.GetInt("maxLifesAmount", 1);
		}
		
		set
		{
			m_maxLifesAmount = value;
			PlayerPrefs.SetInt("maxLifesAmount", m_maxLifesAmount);
		}
	}
	private static int m_maxLifesAmount;
	
	public static bool tutorial 
	{
		get
		{
			var tutorInt = PlayerPrefs.GetInt("tutor", 1);
			if (tutorInt == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		set
		{
			if (value)
			{
				m_tutorial = 1;
			}
			else
			{
				m_tutorial = 0;
			}
			PlayerPrefs.SetInt("tutor", m_tutorial);
		}
	}
	private static int m_tutorial;
	public static int level
	{
		get
		{
			 return PlayerPrefs.GetInt("level", 1);
		}
		
		set
		{
			m_level = value;
			PlayerPrefs.SetInt("level", m_level);
		}
	}
	private static int m_level;
	
	public static int coins
	{
		get
		{
			return PlayerPrefs.GetInt("coins", 100);
		}
		
		set
		{
			m_coins = value;
			PlayerPrefs.SetInt("coins", m_coins);
		}
	}
	private static int m_coins;
	
	public static void Reset()
	{
		coins = 100;
		maxLifesAmount = 1;
		hookDistance = 0;
		level = 1;
		tutorial = true;
	}
}
