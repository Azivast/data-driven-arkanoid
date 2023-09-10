using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelSet", menuName = "Arkanoid/Level set")]
public class LevelSet : ScriptableObject {
    public GameObject[] Levels;
}
