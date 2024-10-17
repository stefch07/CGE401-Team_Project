using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDX : MonoBehaviour
{
    void Start() {
        PlayerController.hasDiedOrWon = true;
    }
}
