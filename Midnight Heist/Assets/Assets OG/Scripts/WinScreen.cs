using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public GameObject panelF;
    public GameObject panelD;
    public GameObject panelC;
    public GameObject panelB;
    public GameObject panelA;
    public GameObject panelS;
    
    private void Start() {
        panelF.SetActive(false);
        panelD.SetActive(false);
        panelC.SetActive(false);
        panelB.SetActive(false);
        panelA.SetActive(false);
        panelS.SetActive(false);
    }
    
    private void Update() {
        if (PlayerController.goldCounter == 0) {
            panelF.SetActive(true);
        }
        else if (PlayerController.goldCounter < 50) {
            panelD.SetActive(true);
        }
        else if (PlayerController.goldCounter < 100) {
            panelC.SetActive(true);
        }
        else if (PlayerController.goldCounter < 150) {
            panelB.SetActive(true);
        }
        else if (PlayerController.goldCounter < 218) {
            panelA.SetActive(true);
        }
        else {
            panelS.SetActive(true);
        }
    }
}
