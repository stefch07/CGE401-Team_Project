using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouLose : MonoBehaviour
{
    public HealthSystem healthSystem;
    public GameObject losePanel;
    
    private void Start() {
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
        losePanel.SetActive(false);
    }
    
    private void Update() {
        if (healthSystem.gameOver) {
            losePanel.SetActive(true);
        }
        else {
            losePanel.SetActive(false);
        }
    }
}
