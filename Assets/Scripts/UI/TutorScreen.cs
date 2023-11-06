using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutorScreen : MonoBehaviour
{
	[SerializeField] private ChainGun chainGun; 
	[SerializeField] private GameObject playerArrow;
	[SerializeField] private Animator animator;
	[SerializeField] private TMP_Text characterText;
	public Action End;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += Image1;
		characterText.text = "Welcome to Mighty Bull: Fortune Ox!";
	}
	
	private void Image1(Finger finger)
	{
		Touch.onFingerDown -= Image1;
		Touch.onFingerDown += Image2;
		
		characterText.text = "Here is your hook ball";
		playerArrow.SetActive(true);
	}
	
	private void Image2(Finger finger)
	{
		Touch.onFingerDown -= Image2;
		Touch.onFingerDown += Image3;
		
		playerArrow.SetActive(false);
		chainGun.SimulateGrappleHook();
		characterText.text = "Control it by aiming at obstacles and shooting the grappling rope like this";
	}
	
	private void Image3(Finger finger)
	{
		Touch.onFingerDown -= Image3;
		Touch.onFingerDown += Image4;
		
		chainGun.DisableSimulate();
		characterText.text = "You can hook to platform by the distance of pulsating field around your ball";
	}
	
	private void Image4(Finger finger)
	{
		Touch.onFingerDown -= Image4;
		Touch.onFingerDown += Image5;
		characterText.text = "As you move up the level you will find coins, thanks to which you will be able to complete the level";
	}
	
	private void Image5(Finger finger)
	{
		Touch.onFingerDown -= Image5;
		Touch.onFingerDown += Image6;
		characterText.text = "Good luck!";
	}
	
	private void Image6(Finger finger)
	{
		Touch.onFingerDown -= Image6;
		
		End?.Invoke();
		gameObject.SetActive(false);
	}
}
