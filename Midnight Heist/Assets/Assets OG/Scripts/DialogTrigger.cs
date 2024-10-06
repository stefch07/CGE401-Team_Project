using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogManager dialogManager;
    public Canvas dialogCanvas;
    private bool inZone = false;
    
    void Start()
    {
        dialogCanvas.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (inZone && Input.GetKeyDown(KeyCode.E))
        {
            dialogCanvas.gameObject.SetActive(true);
            dialogManager.gameObject.SetActive(true);
            dialogManager.StartCoroutine("Type");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
            dialogCanvas.gameObject.SetActive(false);
            dialogManager.SkipSentences();
        }
    }
}
