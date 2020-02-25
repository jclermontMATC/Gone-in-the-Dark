using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{
    public static float currentHealth = 100;
    void Update(){
        currentHealth = Mathf.Max(currentHealth - 1, 0);
    }
}
