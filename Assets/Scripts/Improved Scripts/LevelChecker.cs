using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    public int level;
    public void Start()
    {
        level = PlayerPrefs.GetInt("levelNumber");
    }

  
}
