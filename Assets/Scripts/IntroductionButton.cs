using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionButton : MonoBehaviour
{
    public void Continue()
    {
        GameManager.instance.Continue();
    }
}
