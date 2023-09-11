using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelSet", menuName = "Arkanoid/Level set")]
public class LevelSet : ScriptableObject {
    [Tooltip("Levels the player will traverse through, sorted in ascending order.")] 
    public GameObject[] Levels;
}
