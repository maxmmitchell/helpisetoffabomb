using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public Slider Master;
    public Slider Music;
    public Slider SFX;

    private void Start()
    {
        // setup sliders based on current mixer levels
        float temp;

        mixer.GetFloat("MasterVolume", out temp);
        Master.value = MixerToSliderValue(temp);

        mixer.GetFloat("MusicVolume", out temp);
        Music.value = MixerToSliderValue(temp);

        mixer.GetFloat("SFXVolume", out temp);
        SFX.value = MixerToSliderValue(temp);
    }

    float MixerToSliderValue(float mixerValue)
    {
        // reverse process used to turn slider value into mixer value
        float sliderValue = mixerValue / 20;
        sliderValue = Mathf.Pow(10, sliderValue);
        return sliderValue;
    }

    public void SetLevel(float sliderValue)
    {
            mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetVoicesLevel(float sliderValue)
    {
        mixer.SetFloat("VoicesVolume", Mathf.Log10(sliderValue) * 20);
    }
}