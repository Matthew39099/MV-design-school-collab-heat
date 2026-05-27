using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource ambienceSource;

    // Ambience
    [SerializeField] private AudioClip furnaceRoom;
    [SerializeField] private AudioClip cavernsArea;
    // Sound Effects
    [SerializeField] private AudioClip furnaceEnter;
    [SerializeField] private AudioClip playerJump;
    [SerializeField] private AudioClip oreMining;
    [SerializeField] private AudioClip picPickup;
    
    void Awake()
    {
        Instance = this;
        //audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundType)
    {
        switch (soundType)
        {
            // Ambience
            case "furnaceRoom":
                if (!ambienceSource.isPlaying)
                {
                    ambienceSource.clip = furnaceRoom;
                    ambienceSource.loop = true;
                    ambienceSource.Play();
                }
                break;

                 case "cavernsArea":
                if (!ambienceSource.isPlaying)
                {
                    ambienceSource.clip = cavernsArea;
                    ambienceSource.loop = true;
                    ambienceSource.Play();
                }
                break;

            // Effects
            case "furnaceEnter":
                sfxSource.PlayOneShot(furnaceEnter);
                break;
            case "playerJump":
                sfxSource.PlayOneShot(playerJump);
                break;
             case "oreMining":
                sfxSource.PlayOneShot(oreMining);
                break;
            case "picPickup":
                sfxSource.PlayOneShot(picPickup);
                break;

            default:
                break;
        }
    }

    public void StopSound(String soundName)
    {
        if (soundName == "furnaceRoom")
        {
            ambienceSource.Stop();
        }
    }
}
