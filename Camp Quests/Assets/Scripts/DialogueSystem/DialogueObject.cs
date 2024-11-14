using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue; // Prevents code the outside from writing to dialogue, instead being read only

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
