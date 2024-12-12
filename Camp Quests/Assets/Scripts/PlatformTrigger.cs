using System.Collections;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public DialogManager dialogManager;  
    public string[] dialogueSentences;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            dialogManager.sentences = dialogueSentences;

            dialogManager.dialogPanel.SetActive(true);

            dialogManager.OnEnable();
        }
    }
}
