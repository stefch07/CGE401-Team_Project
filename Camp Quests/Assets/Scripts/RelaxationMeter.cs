using UnityEngine;
using UnityEngine.UI;

public class RelaxationMeter : MonoBehaviour
{
    public Slider relaxationBar;
    public static int maxRelax = 100; // maximum value of the relaxation bar

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        // Initialize the relaxation bar
        relaxationBar.value = 0;
        relaxationBar.maxValue = maxRelax;
    }
}
