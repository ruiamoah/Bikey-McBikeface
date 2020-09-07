using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class haptic : MonoBehaviour
{

    OVRHapticsClip buzz;
    public AudioClip audioFile;

    // Update is called once per frame
    void Update()
    {
        buzz = new OVRHapticsClip(audioFile);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hi");
        OVRHaptics.LeftChannel.Mix(buzz);
        OVRHaptics.RightChannel.Mix(buzz);
    }
}