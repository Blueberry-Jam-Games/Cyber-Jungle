using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip step1Sound, step2Sound, step3Sound, step4Sound, step5Sound, step6Sound, jumpSound, landingSound, wooshSound, deathSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        
        step1Sound = Resources.Load<AudioClip> ("step1");
        step2Sound = Resources.Load<AudioClip> ("step2");
        step3Sound = Resources.Load<AudioClip> ("step3");
        step4Sound = Resources.Load<AudioClip> ("step4");
        step5Sound = Resources.Load<AudioClip> ("step5");
        step6Sound = Resources.Load<AudioClip> ("step6");
        jumpSound = Resources.Load<AudioClip> ("jump");
        landingSound = Resources.Load<AudioClip> ("land");
        wooshSound = Resources.Load<AudioClip> ("woosh");
        deathSound = Resources.Load<AudioClip> ("death");

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
        case "step4":
            audioSrc.PlayOneShot (step4Sound);
            break;
        case "step5":
            audioSrc.PlayOneShot (step5Sound);
            break;
        case "step6":
            audioSrc.PlayOneShot (step6Sound);
            break;
        case "jump":
            audioSrc.PlayOneShot (jumpSound);
            break;
        case "land":
            audioSrc.PlayOneShot (landingSound);
            break;
        case "woosh":
            audioSrc.PlayOneShot (wooshSound);
            break;   
        case "death":
            audioSrc.PlayOneShot (deathSound);
            break;   
        }
    }

}
