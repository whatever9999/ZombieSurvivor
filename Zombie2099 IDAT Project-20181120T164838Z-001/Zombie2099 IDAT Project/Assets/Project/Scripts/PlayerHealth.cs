using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public event System.Action OnVirusTakenOver;

    public float CurrentVirus { get; set; }
    public float StartingVirus { get; set; }

    public Slider virusBar;

	// Use this for initialization
	void Start () {
        StartingVirus = 0f;
        CurrentVirus = StartingVirus;

        virusBar.value = CurrentVirus;
	}
	
	// Update is called once per frame
	void Update () {
        CurrentVirus += 1 * (Time.deltaTime / 1);
        virusBar.value = CurrentVirus;
        if (CurrentVirus >= 100)
        {
            BecomeZombie();
        }
    }

    void BecomeZombie()
    {
        CurrentVirus = 100f;
        virusBar.value = CurrentVirus;
        if (OnVirusTakenOver != null)
        {
            OnVirusTakenOver();
        }
    }
}
