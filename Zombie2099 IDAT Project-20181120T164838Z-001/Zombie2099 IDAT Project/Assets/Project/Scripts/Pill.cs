using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pill : MonoBehaviour
{
    public GameObject pill;
    public bool hadPill = false;

    bool onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        onTrigger = false;
    }

    private void OnGUI()
    {
        if (!hadPill) { 
            if (onTrigger)
            {
                int rectW = 200;
                int rectH = 25;

                GUI.Box(new Rect((Screen.width - rectW) / 2, (Screen.height - rectH) / 2, rectW, rectH), "Press 'E' to take antivirus");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    onTrigger = false;
                    hadPill = true;
                    pill.SetActive(false);
                    float currentVirus = FindObjectOfType<PlayerHealth>().CurrentVirus;
                    currentVirus -= 20;
                    if (currentVirus < 0) {
                        currentVirus = 0;
                    }
                    FindObjectOfType<PlayerHealth>().CurrentVirus = currentVirus;
                }
            }
        }
    }
}