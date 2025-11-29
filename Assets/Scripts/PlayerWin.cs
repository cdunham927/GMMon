using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class PlayerWin : MonoBehaviour
{
    public TMP_Text pText;

    public void SetText(string s)
    {
        pText.text = s;
    }
}
