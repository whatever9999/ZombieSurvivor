using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour {

    public string curPassword = "1234";
    public string input;
    public bool onTrigger;
    public bool doorOpened;
    public bool keypadScreen;
    public Transform doorHinge;
    public int rotate;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        keypadScreen = false;
        input = "";
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(input == curPassword)
        {
            doorOpened = true;
            Cursor.visible = false;
        }

        if(doorOpened)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, doorHinge.rotation.y + rotate, 0.0f), Time.deltaTime * 250);
            doorHinge.rotation = newRot;
        }
    }

    private void OnGUI()
    {
        if(!doorOpened)
        {
            if (onTrigger)
            {
                int rectW = 200;
                int rectH = 25;

                GUI.Box(new Rect((Screen.width - rectW) / 2, (Screen.height - rectH) / 2, rectW, rectH), "Press 'E' to use keypad");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    keypadScreen = true;
                    onTrigger = false;
                }
            }

            if (keypadScreen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                int rectW = 400;
                int rectH = 550;
                int inputW = 310;
                int inputH = 25;
                GUI.Box(new Rect((Screen.width - rectW) / 2, (Screen.height - rectH) / 2, rectW, rectH), "");
                GUI.Box(new Rect((Screen.width - inputW) / 2, (Screen.height - inputH - rectH / 2) / 2 - 45, inputW, inputH), input);

                //Frst Row
                int buttonW = 100;
                int buttonH = 100;
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 - 5, (Screen.height - buttonH - rectH / 2) / 2 + 25, 100, 100), "1"))
                {
                    input = input + "1";
                }
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 + 100, (Screen.height - buttonH - rectH / 2) / 2 + 25, 100, 100), "2"))
                {
                    input = input + "2";
                }
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 + 205, (Screen.height - buttonH - rectH / 2) / 2 + 25, 100, 100), "3"))
                {
                    input = input + "3";
                }

                //Second Row
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 - 5, (Screen.height - buttonH - rectH / 2) / 2 + 130, 100, 100), "4"))
                {
                    input = input + "4";
                }
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 + 100, (Screen.height - buttonH - rectH / 2) / 2 + 130, 100, 100), "5"))
                {
                    input = input + "5";
                }
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 + 205, (Screen.height - buttonH - rectH / 2) / 2 + 130, 100, 100), "6"))
                {
                    input = input + "6";
                }

                //Third Row
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 - 5, (Screen.height - buttonH - rectH / 2) / 2 + 235, 100, 100), "7"))
                {
                    input = input + "7";
                }
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 + 100, (Screen.height - buttonH - rectH / 2) / 2 + 235, 100, 100), "8"))
                {
                    input = input + "8";
                }
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 + 205, (Screen.height - buttonH - rectH / 2) / 2 + 235, 100, 100), "9"))
                {
                    input = input + "9";
                }

                //Zero
                if (GUI.Button(new Rect((Screen.width - buttonW - rectW / 2) / 2 + 100, (Screen.height - buttonH - rectH / 2) / 2 + 340, 100, 100), "0"))
                {
                    input = input + "0";
                }
            }
        }
    }
}
