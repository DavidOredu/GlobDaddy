using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : SingletonDontDestroy<SoundManager> { 
    public Sound[] sounds;
    

    UIManager uIManager;
    // Start is called before the first frame update
   public override void Awake()
    {
        base.Awake();

        


        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        uIManager = UIManager.instance;
    }

    public void PlaySound(string name)
    {
       Sound s =  Array.Find(sounds, sound => sound.SoundName == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (uIManager.GameIsPaused)
        {
            // play pause sound
            s.source.Pause();
        }
        s.source.Play();
        
    }

    public void PlayDelayedSound(string name, float delayTime)
    {
        Sound s = Array.Find(sounds, sound => sound.SoundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (uIManager.GameIsPaused)
        {
            // play pause sound
            s.source.Pause();
        }
        s.source.PlayDelayed(delayTime);
    }
}
