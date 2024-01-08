using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioSLider : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMusicVolumeLevel(float sliderValue){
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXLevel(float sliderValue){
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }
}
