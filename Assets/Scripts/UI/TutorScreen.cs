using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutorScreen : MonoBehaviour
{
	[SerializeField] private ChainGrapple chainGrapple; 
	[SerializeField] private GameObject playerArrow;
	[SerializeField] private Animator animator;
	[SerializeField] private TMP_Text characterText;
	public Action End;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += Image1;
		characterText.text = "Welcome to my game!";
	}
	
	private void Image1(Finger finger)
	{
		Touch.onFingerDown -= Image1;
		Touch.onFingerDown += Image2;
		
		characterText.text = "Here is your hook ball.";
		playerArrow.SetActive(true);
	}
	
	private void Image2(Finger finger)
	{
		Touch.onFingerDown -= Image2;
		Touch.onFingerDown += Image3;
		
		playerArrow.SetActive(false);
		characterText.text = "Control it by aiming at obstacles and shooting the grappling rope like this";
	}
	
	private void Image3(Finger finger)
	{
		Touch.onFingerDown -= Image3;
		Touch.onFingerDown += Image4;
	}
	
	private void Image4(Finger finger)
	{
		Touch.onFingerDown -= Image4;
		Touch.onFingerDown += Image5;
	}
	
	private void Image5(Finger finger)
	{
		Touch.onFingerDown -= Image5;
		
		End?.Invoke();
		gameObject.SetActive(false);
	}
}
