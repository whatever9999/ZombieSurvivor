using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    private bool flashlightEnabled;
    public GameObject lightObj;

    public void Start()
    {
        lightObj.SetActive(false);
    }

    public void Update()
    {
        //equip
        if(Input.GetKeyDown(KeyCode.F))
        {
            flashlightEnabled = !flashlightEnabled;
        }

        if (flashlightEnabled)
        {
            lightObj.SetActive(true);
        } else
        {
            lightObj.SetActive(false);
        }
    }
}
