using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;

   private void LateUpdate()
    {
        if (freezeXZAxis)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
            transform.rotation = targetRotation;
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
