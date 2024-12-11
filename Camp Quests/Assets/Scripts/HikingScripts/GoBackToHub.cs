using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class GoBackToHub : MonoBehaviour
{
    public void LoadHubWorld()
    {
        SceneManager.LoadScene("HubWorld");
    }
}
