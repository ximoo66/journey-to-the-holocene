using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// Author: Omid Ameri
/// Project: Journey to the Holocene (ER-P3)
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton instance

    private Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> voiceClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> ambientClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> interactionClips = new Dictionary<string, AudioClip>(); // Interaction sounds

    private AudioSource musicSource;
    private AudioSource voiceSource;
    private AudioSource ambientSource;
    private AudioSource interactionSource; // For interaction sounds

    [SerializeField] private float defaultFadeDuration = 1.5f; // Default crossfade duration

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSoundManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSoundManager()
    {
        // Add AudioSources for each category
        musicSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        ambientSource = gameObject.AddComponent<AudioSource>();
        interactionSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource for interaction sounds

        // Set default properties for each AudioSource
        musicSource.loop = true;
        ambientSource.loop = true;

        // Load all audio clips into their respective dictionaries
        LoadAudioClips("Audio/Music", musicClips);
        LoadAudioClips("Audio/VoiceLines", voiceClips);
        LoadAudioClips("Audio/Ambient", ambientClips);
        LoadAudioClips("Audio/Interactions", interactionClips); // Load interaction sounds

       
        // Preload all music clips
        if (musicClips.Count > 0)
        {
            foreach (var clip in musicClips.Values)
            {
                musicSource.clip = clip; // Assign clip to AudioSource
                musicSource.Play();      // Trigger Unity to prepare the clip
                musicSource.Stop();      // Stop playback immediately
            }
        }

        Debug.Log($"SoundManager initialized and {musicClips.Count} music clips preloaded.");

    }

    private void LoadAudioClips(string folderPath, Dictionary<string, AudioClip> audioDictionary)
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(folderPath);
        foreach (var clip in clips)
        {
            audioDictionary[clip.name] = clip;
        }

        Debug.Log($"Loaded {clips.Length} clips from {folderPath}");
    }

    // Play music by name with crossfade
    public void PlayMusic(string clipName, float fadeDuration = -1f)
    {
        if (musicClips.TryGetValue(clipName, out AudioClip newClip))
        {
            if (fadeDuration < 0)
            {
                fadeDuration = defaultFadeDuration;
            }
            StartCoroutine(CrossfadeMusic(newClip, fadeDuration));
        }
        else
        {
            Debug.LogWarning($"Music clip '{clipName}' not found.");
        }
    }

    // Stop music with an optional fade-out
    public void StopMusic(float fadeDuration = -1f)
    {
        if (fadeDuration < 0)
        {
            fadeDuration = defaultFadeDuration;
        }
        StartCoroutine(FadeOutMusic(fadeDuration));
    }

    private IEnumerator FadeOutMusic(float fadeDuration)
    {
        float initialVolume = musicSource.volume;

        // Gradually reduce volume to 0
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(initialVolume, 0, t / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = initialVolume; // Restore volume
    }

    // Play ambient sound by name with crossfade
    public void PlayAmbient(string clipName, float fadeDuration = -1f)
    {
        if (ambientClips.TryGetValue(clipName, out AudioClip newClip))
        {
            if (fadeDuration < 0)
            {
                fadeDuration = defaultFadeDuration;
            }
            StartCoroutine(CrossfadeAmbient(newClip, fadeDuration));
        }
        else
        {
            Debug.LogWarning($"Ambient sound '{clipName}' not found.");
        }
    }

    // Stop ambient with an optional fade-out
    public void StopAmbient(float fadeDuration = -1f)
    {
        if (fadeDuration < 0)
        {
            fadeDuration = defaultFadeDuration;
        }
        StartCoroutine(FadeOutAmbient(fadeDuration));
    }

    private IEnumerator FadeOutAmbient(float fadeDuration)
    {
        float initialVolume = ambientSource.volume;

        // Gradually reduce volume to 0
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            ambientSource.volume = Mathf.Lerp(initialVolume, 0, t / fadeDuration);
            yield return null;
        }

        ambientSource.Stop();
        ambientSource.volume = initialVolume; // Restore volume
    }

    // Play interaction sound (one-shot)
    public void PlayInteractionSound(string clipName)
    {
        if (interactionClips.TryGetValue(clipName, out AudioClip clip))
        {
            interactionSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Interaction sound '{clipName}' not found.");
        }
    }

    // Play voice line by name (global sound)
    public void PlayVoiceLine(string clipName)
    {
        if (voiceClips.TryGetValue(clipName, out AudioClip clip))
        {
            voiceSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Voice line '{clipName}' not found.");
        }
    }

    // Play voice line at a specific AudioSource
    public void PlayVoiceLineAtSource(string clipName, AudioSource source)
    {
        if (voiceClips.TryGetValue(clipName, out AudioClip clip))
        {
            if (source != null)
            {
                source.Stop(); // Stop any currently playing voice line
                source.clip = clip;
                source.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource is null. Cannot play voice line at source.");
            }
        }
        else
        {
            Debug.LogWarning($"Voice line '{clipName}' not found.");
        }
    }

    // Crossfade music to a new track
    private IEnumerator CrossfadeMusic(AudioClip newClip, float fadeDuration)
    {
        float initialVolume = musicSource.volume;

        // Fade out current music
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(initialVolume, 0, t / fadeDuration);
            yield return null;
        }

        // Switch to the new clip
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in new music
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0, initialVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = initialVolume;
    }

    // Crossfade ambient to a new track
    private IEnumerator CrossfadeAmbient(AudioClip newClip, float fadeDuration)
    {
        float initialVolume = ambientSource.volume;

        // Fade out current ambient
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            ambientSource.volume = Mathf.Lerp(initialVolume, 0, t / fadeDuration);
            yield return null;
        }

        // Switch to the new clip
        ambientSource.clip = newClip;
        ambientSource.Play();

        // Fade in new ambient
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            ambientSource.volume = Mathf.Lerp(0, initialVolume, t / fadeDuration);
            yield return null;
        }

        ambientSource.volume = initialVolume;
    }

    // Adjust volume for a category
    public void SetVolume(string category, float volume)
    {
        volume = Mathf.Clamp01(volume);

        switch (category.ToLower())
        {
            case "music":
                musicSource.volume = volume;
                break;
            case "voice":
                voiceSource.volume = volume;
                break;
            case "ambient":
                ambientSource.volume = volume;
                break;
            case "interaction":
                interactionSource.volume = volume;
                break;
            default:
                Debug.LogWarning($"Unknown sound category: {category}");
                break;
        }
    }
}
