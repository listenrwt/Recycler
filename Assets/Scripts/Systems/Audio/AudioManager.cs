using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        //Singleton pattern
        if(instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //Create the audioSource component
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

        }

    }

    //Play the Audio
    public void PlaySound(string name)
    {
        //Search the sound 
        Sound sound = null;
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                sound = s;
                break;
            }
        }

        if(sound != null)
        {
            sound.source.Play();
        }else
        {
            Debug.LogWarning("Invalid Sound!");
        }
        
    }

    //Stop playing the Audio
    public void StopSound(string name)
    {
        //Search the sound 
        Sound sound = null;
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                sound = s;
                break;
            }
        }

        if (sound != null)
        {
            sound.source.Stop();
        }else
        {
            Debug.LogWarning("Invalid Sound!");
        }
       
    }
}
