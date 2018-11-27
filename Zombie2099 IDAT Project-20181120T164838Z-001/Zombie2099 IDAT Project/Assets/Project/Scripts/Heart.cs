using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Heart : MonoBehaviour {

    public GameObject BigHeart;
    public GameObject SmallHeart;

    AudioSource heartbeat;

    public float secondsBetweenBeats;
    bool keepPlaying = true;
    bool justBeat = false;

    private void Start()
    {
        heartbeat = this.GetComponent<AudioSource>();
        StartCoroutine(Beat());
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
                yield return new WaitForSeconds(0.2f);
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
