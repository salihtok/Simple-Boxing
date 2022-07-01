using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    
     public void SetLevel(int level)
     {
        PlayerPrefs.SetInt("levelNumber", level);
        PlayerPrefs.GetInt("levelNumber");
        Debug.Log("Level is: " + PlayerPrefs.GetInt("levelNumber"));
        Time.timeScale = 1;

     }

     
}
