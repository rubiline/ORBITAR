using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<SFXEntry> effects;
    public List<MusicEntry> music;
    public string musicPath;
    public string effectsPath;

    private AudioSource sfxSquareTrack;
    private AudioSource sfxNoiseTrack;
    private AudioSource sfxWaveTrack;
    private AudioSource squareTrack;
    private AudioSource squareTrack2;
    private AudioSource waveTrack;
    private AudioSource noiseTrack;

    private Dictionary<string, SFXEntry> effectList;
    private Dictionary<string, MusicEntry> musicList;

    void Awake()
    {
        //singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        //assign tracks
        sfxSquareTrack = this.gameObject.AddComponent<AudioSource>();
        sfxNoiseTrack = this.gameObject.AddComponent<AudioSource>();
        sfxWaveTrack = this.gameObject.AddComponent<AudioSource>();
        squareTrack = this.gameObject.AddComponent<AudioSource>();
        squareTrack2 = this.gameObject.AddComponent<AudioSource>();
        waveTrack = this.gameObject.AddComponent<AudioSource>();
        noiseTrack = this.gameObject.AddComponent<AudioSource>();
        squareTrack.loop = true;
        squareTrack2.loop = true;
        waveTrack.loop = true;
        noiseTrack.loop = true;
        //populate lists
        effectList = new Dictionary<string, SFXEntry>();
        foreach(SFXEntry fx in effects)
        {
            effectList.Add(fx.name, fx);
        }
        musicList = new Dictionary<string, MusicEntry>();
        foreach(MusicEntry m in music)
        {
            musicList.Add(m.name, m);
        }
    }

    void Update()
    {
        if (sfxSquareTrack.isPlaying) squareTrack2.volume = 0;
        else squareTrack2.volume = 1;
        if (sfxWaveTrack.isPlaying) waveTrack.volume = 0;
        else waveTrack.volume = 1;
        if (sfxNoiseTrack.isPlaying) noiseTrack.volume = 0;
        else noiseTrack.volume = 1;
    }

    public void PlaySFX(string effect)
    {
        try
        {
            SFXEntry targetSound = effectList[effect];
            switch(targetSound.channel)
            {
                case EffectChannel.square:
                    sfxSquareTrack.Stop();
                    sfxSquareTrack.clip = targetSound.clip;
                    sfxSquareTrack.Play();
                    break;
                case EffectChannel.wave:
                    sfxWaveTrack.Stop();
                    sfxWaveTrack.clip = targetSound.clip;
                    sfxWaveTrack.Play();
                    break;
                case EffectChannel.noise:
                    sfxNoiseTrack.Stop();
                    sfxNoiseTrack.clip = targetSound.clip;
                    sfxNoiseTrack.Play();
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }
    }
    
    //TODO: looping effects

    public void PlayMusic(string song)
    {
        try
        {
            MusicEntry targetSong = musicList[song];
            squareTrack.Stop();
            squareTrack2.Stop();
            waveTrack.Stop();
            noiseTrack.Stop();
            squareTrack.clip = Resources.Load<AudioClip>(musicPath + "/" + targetSong.name + "square");
            squareTrack2.clip = Resources.Load<AudioClip>(musicPath + "/" + targetSong.name + "square2");
            waveTrack.clip = Resources.Load<AudioClip>(musicPath + "/" + targetSong.name + "wave");
            noiseTrack.clip = Resources.Load<AudioClip>(musicPath + "/" + targetSong.name + "noise");
            squareTrack.Play();
            squareTrack2.Play();
            waveTrack.Play();
            noiseTrack.Play();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }
    }

    [System.Serializable]
    public struct SFXEntry
    {
        public string name;
        public AudioClip clip;
        public EffectChannel channel;
        public bool looping;
    }

    [System.Serializable]
    public struct MusicEntry
    {
        public string name;
        public bool hasIntro;
    }

    public enum EffectChannel { square, wave, noise }
}
