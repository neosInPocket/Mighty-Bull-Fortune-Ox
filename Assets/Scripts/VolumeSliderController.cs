using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private VolumeController volumeController;
	
	private void Start()
	{
		slider.value = VolumeController.volume;
	}
	
	public void Save()
	{
		VolumeController.volume = slider.value;
	}
	
	public void SetCurrentVolume(float value)
	{
		 volumeController.SetVolume(value);	
	}
}
