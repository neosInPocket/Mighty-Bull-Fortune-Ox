using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusContainer : MonoBehaviour
{
    public void Play()
    {
    	SceneManager.LoadScene("Game");
    }
}
