using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class PlayerSettings {


    public delegate void VolumeChange(float volume);

    static public event VolumeChange MusicVolumeChanged;
    static public float MusicVolume
    {
        get { return PlayerPrefs.GetFloat("MusicVolume", 1); }
        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            if (MusicVolumeChanged != null)
                MusicVolumeChanged(value);
        }
    }

    static public event VolumeChange SfxVolumeChanged;
    static public float SfxVolume
    {
        get { return PlayerPrefs.GetFloat("SFXVolume", 1); }
        set
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            if (SfxVolumeChanged != null)
                SfxVolumeChanged(value);
        }
    }
}
