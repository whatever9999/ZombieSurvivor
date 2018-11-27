using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{

    public GameObject noteImage;
    
    bool onTrigger;

    private void Start()
    {
        noteImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        noteImage.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        if (onTrigger)
        {
            int rectW = 200;
            int rectH = 25;

            GUI.Box(new Rect((Screen.width - rectW) / 2, (Screen.height - rectH) / 2, rectW, rectH), "Press 'E' to read note");

            if (Input.GetKeyDown(KeyCode.E))
            {
                onTrigger = false;
                noteImage.SetActive(true);
            }
        }
    }
}