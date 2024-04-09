using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        Debug.Log(levelSystem.GetLevelNnumber());
        levelSystem.AddExperience(50);
        Debug.Log(levelSystem.GetLevelNnumber());
        levelSystem.AddExperience(60);
        Debug.Log(levelSystem.GetLevelNnumber());
    }
}
