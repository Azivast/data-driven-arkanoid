using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupSet", menuName = "Arkanoid/Power-up set", order = 1)]
public class PowerUpSet : ScriptableObject
{
    [Tooltip("Array of all power-up capsules the given set includes.")]
    public GameObject[] PowerupCapsules;
}
