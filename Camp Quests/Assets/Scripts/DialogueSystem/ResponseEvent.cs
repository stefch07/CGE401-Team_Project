using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ResponseEvent
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPickedResponse;
    [SerializeField] private string targetSceneName;
    
    public UnityEvent OnPickedResponse => onPickedResponse;

    public void Trigger()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            Debug.Log($"Attempting to load scene: {targetSceneName}");
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.Log($"Invoking response event without a scene transition.");
            onPickedResponse?.Invoke();
        }
    }
}
