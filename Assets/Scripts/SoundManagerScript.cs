using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip step1Sound, step2Sound, step3Sound, wooshSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        
        step1Sound = Resources.Load<AudioClip> ("step1");
        step2Sound = Resources.Load<AudioClip> ("step2");
        step3Sound = Resources.Load<AudioClip> ("step3");
        wooshSound = Resources.Load<AudioClip> ("woosh");

        audioSrc = GetComponent<AudioSource> ();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip) {
        switch (clip) {
        case "step1":
            audioSrc.PlayOneShot (step1Sound);
            break;
        case "step2":
            audioSrc.PlayOneShot (step2Sound);
            break;
        case "step3":
            audioSrc.PlayOneShot (step3Sound);
            break;
        case "woosh":
            audioSrc.PlayOneShot (wooshSound);
            break;   
        }
    }

}
