using UnityEngine;
using UnityEngine.UI;

public class GoldBar : MonoBehaviour
{
    public Slider goldBar;
    //public PlayerController playerController;
    public int maxGold = 230;  // maximum value of the gold bar

    void Start()
    {
        // Initialize the progress bar
        goldBar.maxValue = maxGold;
    }

    void Update()
    {
        // Update the progress bar based on the goldCounter in PlayerController
        goldBar.value = PlayerController.goldCounter;
    }
}
