using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
	[SerializeField] private AudioSource music;
	
	
	public static float volume
	{
		get
		{
			return PlayerPrefs.GetFloat("volume", 1f);
		}
		
		set
		{
			m_volume = value;
			PlayerPrefs.SetFloat("volume", m_volume);
		}
	}
	private static float m_volume;
	
	public void SetVolume(float value)
	{
		music.volume = value;
	}
	
	private void Start()
	{
		music.volume = volume;
	}
}
