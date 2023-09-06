using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSet", menuName = "Arkanoid/Power-up set", order = 1)]
public class PowerUpSet : ScriptableObject
{
    [Tooltip("Array of all power-ups the given set includes.")]
    public GameObject[] PowerUps;
}
