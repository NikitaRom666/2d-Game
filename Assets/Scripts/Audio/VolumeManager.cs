using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value =
            PlayerPrefs.GetFloat("Volume", 1f);

        SetVolume(volumeSlider.value);

        volumeSlider.onValueChanged
            .AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        if (value <= 0.0001f)
            value = 0.0001f;

        mixer.SetFloat(
            "MasterVolume",
            Mathf.Log10(value) * 20
        );

        PlayerPrefs.SetFloat(
            "Volume",
            value
        );
    }
}