using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    //public Text oil;
    public static bool lantern = false;
    public float currentOilValue = 100f;
    [SerializeField] public static float currentOil = 100f;
    [SerializeField] private float maxOil = 100f;
    [SerializeField] private float minOil = 0f;
    [SerializeField] private float setOil = 1f;
    [SerializeField] private int absOil;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (currentOilValue != currentOil) {
            currentOil = currentOilValue;
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("Pressed");
            lantern = !lantern;
        }

        if (lantern == true && currentOil > 0) {
            currentOil = Mathf.Max(currentOil - (setOil * Time.deltaTime), 0);
            //oil.text = "" + Mathf.RoundToInt(currentOil);
        }

        currentOilValue = currentOil;

        if (currentOil <= 0) {
            lantern = false;
        }
    }
}

