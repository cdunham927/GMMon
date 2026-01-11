using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSolutions", order = 1)]
public class PlayerSolutions : ScriptableObject
{
    public List<Sprite> keys;
    public List<string> hints;
}
