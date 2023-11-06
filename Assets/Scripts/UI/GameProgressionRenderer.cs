using System.Collections;	
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressionRenderer : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private Image[] hearts;
	[SerializeField] private Image progressionFill;
	[SerializeField] private Image progressionPlaceholder;
	[SerializeField] private TMP_Text levelText;
 	
	public void RefreshHearts(int heartsAmount)
	{
		foreach (var heart in hearts)
		{
			heart.enabled = false;
		}
		
		for (int i = 0; i < heartsAmount; i++)
		{
			hearts[i].enabled = true;
		}
	}
	
	public void RefreshProgression(float fillValue)
	{
		StopAllCoroutines();
		progressionPlaceholder.fillAmount = fillValue;
		StartCoroutine(SetSmoothProgression(fillValue));
	}
	
	public void ClearProgression()
	{
		progressionPlaceholder.fillAmount = 0;
		progressionFill.fillAmount = 0;
	}
	
	private IEnumerator SetSmoothProgression(float fillValue)
	{
		while (progressionFill.fillAmount < fillValue)
		{
			progressionFill.fillAmount += speed;
			yield return new WaitForFixedUpdate();
		}
	}
	
	public void SetLevelText(int levelNumber)
	{
		levelText.text = "LEVEL " + levelNumber;
	}
}
