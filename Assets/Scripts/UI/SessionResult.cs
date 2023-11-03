using TMPro;
using UnityEngine;

public class SessionResult : MonoBehaviour
{
	[SerializeField] private TMP_Text coinsText;
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text tryAgainButtonText;
	[SerializeField] private Animator animator;
	[SerializeField] private GameProcess gameProcess;
	
	public void RefreshResultInfo(bool isLose, int coins = 0)
	{
		if (isLose)
		{
			coinsText.text = $"+0";
			resultText.text = "LOSE";
			tryAgainButtonText.text = "TRY AGAIN";
		}
		else
		{
			coinsText.text = $"+{coins}";
			resultText.text = "WIN";
			tryAgainButtonText.text = "NEXT LEVEL";
		}
	}
	
	public void Hide()
	{
		animator.SetTrigger("Hide");
	}
	
	public void RestartGame()
	{
		gameObject.SetActive(false);
		gameProcess.StartNewGame();
	}
}
