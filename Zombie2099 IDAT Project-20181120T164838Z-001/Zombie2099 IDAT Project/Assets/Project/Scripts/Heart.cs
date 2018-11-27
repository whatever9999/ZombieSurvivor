using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Heart : MonoBehaviour {

    public GameObject BigHeart;
    public GameObject SmallHeart;

    AudioSource heartbeat;

    public float secondsBetweenBeats;
    public bool keepPlaying = true;
    bool justBeat = false;

    private void Start()
    {
        heartbeat = this.GetComponent<AudioSource>();
        StartCoroutine(Beat());
    }

    private void Update()
    {
        int numOfCloseGuards = 0;
        Guard[] guards = FindObjectsOfType<Guard>();

        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i].isClose)
            {
                numOfCloseGuards += 1;
            }
        }
        if (numOfCloseGuards == 0)
        {
            FindObjectOfType<Heart>().secondsBetweenBeats += Time.deltaTime;
            if (FindObjectOfType<Heart>().secondsBetweenBeats > 1f) FindObjectOfType<Heart>().secondsBetweenBeats = 1f;
        }
    }

    IEnumerator Beat()
    {
        while (keepPlaying)
        {
            if (!justBeat)
            {
                SmallHeart.SetActive(false);
                BigHeart.SetActive(true);
                heartbeat.Play();
                justBeat = true;
                yield return new WaitForSeconds(secondsBetweenBeats / 5);
            }
            else
            {
                BigHeart.SetActive(false);
                SmallHeart.SetActive(true);
                justBeat = false;
                yield return new WaitForSeconds(secondsBetweenBeats);
            }
        }
    }
}
