using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreContainerController : MonoBehaviour
{
	[SerializeField] private TMP_Text playerCoins;
	[SerializeField] private Button hookButton;
	[SerializeField] private Button lifesButton;
	[SerializeField] private GameObject[] hookPoints;
	[SerializeField] private GameObject[] lifesPoints;
	
	private void Start()
	{
		//SaveSystem.Reset();
	}
	
	public void UpdateStore()
	{
		playerCoins.text = SaveSystem.coins.ToString();
		
		UpdatePoint();
		UpdateButtons();
	}
	
	private void UpdatePoint()
	{
		foreach (var point in lifesPoints)
		{
			point.SetActive(false);
		}
		
		foreach (var point in hookPoints)
		{
			point.SetActive(false);
		}
		
		for (var i = 0; i < SaveSystem.maxLifesAmount; i++)
		{
			lifesPoints[i].SetActive(true);
		}
		
		for (var i = 0; i < SaveSystem.hookDistance; i++)
		{
			hookPoints[i].SetActive(true);
		}
	}
	
	private void UpdateButtons()
	{
		if (SaveSystem.coins - 100 < 0 || SaveSystem.maxLifesAmount == 3)
		{
			lifesButton.interactable = false;
		}
		else
		{
			lifesButton.interactable = true;
		}
		
		if (SaveSystem.coins - 50 < 0 || SaveSystem.hookDistance == 3)
		{
			hookButton.interactable = false;
		}
		else
		{
			hookButton.interactable = true;
		}
	}
	
	public void HookDistance()
	{
		SaveSystem.coins -= 50;
		SaveSystem.hookDistance++;
		
		UpdateStore();
	}
	
	public void LifesAmount()
	{
		SaveSystem.coins -= 100;
		SaveSystem.maxLifesAmount++;
		
		UpdateStore();
	}
}
