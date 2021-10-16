using System;
using UnityEngine;
/// <summary>
/// Thiss class is used to handle all audio functionality
/// </summary>
public class AudioManager : MonoBehaviour {

    public Sound[] Sounds;

    public static AudioManager Instance;

    void Awake() {

        if (Instance == null) { //Avoids duplicate AudioManagers
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        foreach (Sound s in Sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        Play("Music_Battle");
    }

    public void Play(string name) {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name + " not found in AudioManager!");
            return;
        }
        s.source.Play();
    }
}
