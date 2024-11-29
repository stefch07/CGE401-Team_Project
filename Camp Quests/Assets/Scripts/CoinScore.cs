using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    public Text coinText;
    public static int coinCount = 0;
    
    private void Update() {
        coinText.text = "× " + coinCount;
    }
    
    public void Inc() {
        ++coinCount;
    }
}
