using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioMixer mixer;

    private void Start()
    {
        mixer.SetFloat("MusicVolume", PlayerSettings.MusicVolume.LinearToDecibel());
        mixer.SetFloat("SFXVolume", PlayerSettings.SfxVolume.LinearToDecibel());

        PlayerSettings.MusicVolumeChanged += delegate (float volume)
            {
                mixer.SetFloat("MusicVolume", volume.LinearToDecibel());
            };

        PlayerSettings.SfxVolumeChanged += delegate (float volume)
        {
            mixer.SetFloat("SFXVolume", volume.LinearToDecibel());
        };
    }

}
