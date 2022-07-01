using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuHandler : MonoBehaviour
{
    public GameObject gameMenu;
   
    void Start()
    {
        gameMenu.SetActive(false);
    }

  
   

    public void GameMenuOff()
    {
        gameMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
