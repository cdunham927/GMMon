using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerOut : MonoBehaviour
{
    public float[] scores = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
    public int curPlace = 10;
    public int round;
    [HideInInspector]
    public int eliminatedPlayer;
}
