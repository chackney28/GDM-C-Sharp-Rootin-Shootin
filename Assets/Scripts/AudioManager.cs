
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    public AudioClip backgroundMusic;
    public AudioClip shotSound;
    public AudioClip playerHurtSound;
    public AudioClip enemyHurtSound;
    public AudioClip barnHurtSound;
    public AudioClip playerDeathSound;
    public AudioClip enemyDeathSound;
    public AudioClip barnDeathSound;
    public float masterVolume = 0f;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.GetFloat("volume") == null)
        {
            PlayerPrefs.SetFloat("volume", 0.7f);
            PlayerPrefs.Save();
            masterVolume = PlayerPrefs.GetFloat("volume");
        } else {
            masterVolume = PlayerPrefs.GetFloat("volume");
        }
    }
    
    void Start()
    {
        AudioListener.volume = masterVolume;
        PlayMusic(backgroundMusic);
    }

    void Update()
    {
       AudioListener.volume = masterVolume; 
       PlayerPrefs.SetFloat("volume", masterVolume);
       PlayerPrefs.Save();
    }
    
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    
    public void PlaySoundEffect(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}