using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public Sound[] ShootingSounds;
    public Sound[] Stabbing;
    public Sound[] Music;
    public Sound[] GoldTake;
    public Sound[] Menu;
    public Sound[] Blood;
    public Sound ammoPickUp;

    public Sound soldiersShoot;
    public Sound HitlerShoot;
    public Sound ghostsShoot;

    public Sound Explosion;
    public Sound TankFire;
    public Sound tankFireExplosion;

    private bool play_music = false;
    private float trackLength;
    private float timeCounter = 0f;

    private AudioSource currentMusicSource;
    private AudioSource currentEffectsSource;

    private float effects_volume = 1f;
    private float music_volume = 1f;
    private bool isPlayingMusic;
    private static SoundManager S;
    

    public static bool IsPlayingMusic
    {
        get
        {
            return S.isPlayingMusic;
        }
        set
        {
            S.isPlayingMusic = value;
        }
    }

    public static float EffectsVolume{
        get{
            return S.effects_volume;
            }
        set
        {
            S.effects_volume = value;
        }
        
    }

    public static float MusicVolume
    {
        get
        {
            return S.music_volume;
        }
        set
        {
            S.music_volume = value;
        }

    }






    private void Awake()
    {
        if(S==null)
            S = this;

    }


    [System.Serializable]
    public class Sound
    {

        public string ClipName;
        public AudioClip clip;
        [Range(0f,1f)]
        public float volume;
        [Range(0f, 1f)]
        public float pitch;

        public Sound()
        {
            clip = null;
            ClipName = null;
            volume = 1;
            pitch = 0;

        }
        


    }

    public static void PlaySound(string name, ref AudioSource source)
    {
        float volume_multiplier = 1f;

        SoundManager.Sound soundToPlay = null;
        
        if(name == "Music" || name == "Menu")
        {
            volume_multiplier = S.music_volume;
            S.currentMusicSource = source;
        }
        else
        {
            volume_multiplier = S.effects_volume;
            S.currentEffectsSource = source;
        }


        if (name == "Shoot")
        {
            for (int i = 0; i < S.ShootingSounds.Length; i++)
            {
                if (S.ShootingSounds[i].ClipName.Contains(WeaponInfo.CurrentWeapon.name))
                {
                    soundToPlay = S.ShootingSounds[i];

                }
            }
            volume_multiplier = S.effects_volume;
        }
        else if (name == "soldiersShoot")
        {
            soundToPlay = S.soldiersShoot;


        } else if(name == "ghostsShoot")
        {
            soundToPlay = S.ghostsShoot;
        } else if(name == "Blood")
        {
            int i = UnityEngine.Random.Range(0, S.Blood.Length);
            soundToPlay = S.Blood[i];
        }
        else if (name == "Music") // music
        {
            int i = UnityEngine.Random.Range(0, S.Music.Length);
            soundToPlay = S.Music[i];
            S.trackLength = soundToPlay.clip.length;

        } else if (name == "Stabbing")
        {
            int i = UnityEngine.Random.Range(0, S.Stabbing.Length);
            soundToPlay = S.Stabbing[i];
        }

        else if (name == "TakeGold")
        {
            int i = UnityEngine.Random.Range(0, S.GoldTake.Length);
            soundToPlay = S.GoldTake[i];
        }

        else if (name == "Menu") // menu music
        {

            if (S.Menu.Length == 0)
            {
                return;
            }
            int i = UnityEngine.Random.Range(0, S.Menu.Length);
            soundToPlay = S.Menu[i];

        }
        else if (name == "ammoPickUp")
        {
            soundToPlay = S.ammoPickUp;
        }
        else if (name == "Explosion")
        {
                soundToPlay = S.Explosion;
        } else if (name == "HitlerShoot")
        {
            soundToPlay = S.HitlerShoot;
        }
        else if (name == "TankFire")
        {

                soundToPlay = S.TankFire;
        }
        else if (name == "tankFireExplosion")
        {
       
                soundToPlay = S.tankFireExplosion;
        }

        if (soundToPlay == null)
        {
            return;
        }
       
        source.volume = soundToPlay.volume * volume_multiplier;
        source.pitch = soundToPlay.pitch;
        if (source.pitch == 0)
            source.pitch = 1;
        source.PlayOneShot(soundToPlay.clip);
    }

    private void Update()
    {
        if(currentMusicSource !=null)
            currentMusicSource.volume = music_volume;
        if (currentEffectsSource != null)
            currentEffectsSource.volume = effects_volume;
        // Music Playing
        if(play_music == true)
        {
            if(trackLength <= 0)
            {
                PlaySound("Music", ref currentMusicSource);
                
            }
            trackLength -= Time.deltaTime;
        }


    }



    public static void playMusic(ref AudioSource music)
    {
        S.currentMusicSource = music;
        S.play_music = true;
    }

    public static void StopPlaynigMusic()
    {
        S.play_music = false;
    }



    public static void tunoffMusic()
    {
        S.music_volume = 0f;
    }

    public static void turnoffEffects()
    {
        S.effects_volume = 0f;
    }



}
