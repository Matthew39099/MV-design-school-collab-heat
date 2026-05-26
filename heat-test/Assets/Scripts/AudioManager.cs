using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolume();
    }

    private void Start()
    {
        // Set slider to saved value
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        }
    }

    // Called by UI slider
    public void SetMasterVolume(float sliderValue)
    {
        // Convert to logarithmic scale
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);

        // Save volume
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    private void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
}