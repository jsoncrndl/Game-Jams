using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        audioMixer.SetFloat("soundsVolume", 0);
        audioMixer.SetFloat("musicVolume", -80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
