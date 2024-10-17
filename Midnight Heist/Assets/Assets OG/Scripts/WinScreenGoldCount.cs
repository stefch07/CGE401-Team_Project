using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreenGoldCount : MonoBehaviour
{
    public TMP_Text textbox;
    
    void Update()
    {
        textbox.text = PlayerController.goldCounter + "G/230G";
    }
}
