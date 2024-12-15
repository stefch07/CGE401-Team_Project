using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDelete : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!DialogManagerKayak.hasSeen) {
            Destroy(gameObject);
        }
    }
}
