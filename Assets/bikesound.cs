using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikesound : MonoBehaviour
{

    OVRHapticsClip buzz;
    public AudioClip audioFile;
     public Transform bike;

    // Update is called once per frame
    void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioFile;
        buzz = new OVRHapticsClip(audioFile);
        //if(bike.hasChanged)
        //{

            audio.Play();

        //}

        OVRHaptics.LeftChannel.Mix(buzz);
        OVRHaptics.RightChannel.Mix(buzz);
    }
}
