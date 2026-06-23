using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource; // Per la musica di sottofondo
    public AudioSource soundEffectsSource;    // Per gli effetti sonori

     [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip backgroudCaveMusic;
    [Header("UI sound")]
     public AudioClip buttonSound;
     public AudioClip chestSound;
     public AudioClip workbenchSound;
     public AudioClip inventorySound;
     public AudioClip slotSound;
    [Header("Player sound")]
     public AudioClip deathSound;
     public AudioClip hurtSound;
     public AudioClip attackSound;
     public AudioClip hoeSound;
     public AudioClip collisionCollectSound;
     public AudioClip walkingOnGrassSound;
     public AudioClip walkingOnRockSound;
    [Header("Action sound")]
     public AudioClip rockHurtSound;
     public AudioClip rockBrokenSound;
     public AudioClip treeSound;
     public AudioClip fertilizeSound;
     public AudioClip plantSound;
     public AudioClip doorSound;
     public AudioClip gainLifeSound;
     public AudioClip waterSound;
     public AudioClip stairSound;
    [Header("SlimeSound")]
     public AudioClip slimeAttackSound;
     public AudioClip slimeHurtSound;
     public AudioClip slimeDeathSound;
     public AudioClip slimeMovementSound;
     public AudioClip slimeSleepingSound;

    public void Awake()
    {
        /// Controlla se esiste già un'istanza
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Rendi l'oggetto persistente
            
        }
        else
        {
            Destroy(gameObject); // Elimina il duplicato
        }
    }

    // Cambia la musica di sottofondo
    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }
    public void PlayCaveMusic()
    {
        /*backgroundMusicSource.clip = backgroudCaveMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();*/
        Debug.Log("fingi parta musica");
    }
    // Riproduce un effetto sonoro
    public void PlaySoundEffect(AudioClip clip)
    {
        backgroundMusicSource.PlayOneShot(clip); // Riproduce senza interrompere altri suoni
    }
    public void PlayUIButton()
    {
        soundEffectsSource.PlayOneShot(buttonSound);
    }
    // Cambia il volume della musica di sottofondo
    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
    }

    // Cambia il volume degli effetti sonori
    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsSource.volume = volume;
    }
}
