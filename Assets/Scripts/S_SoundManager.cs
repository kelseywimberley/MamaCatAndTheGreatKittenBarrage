using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SoundManager : MonoBehaviour
{
    public static S_SoundManager instance;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip clip, Transform transform, float volume)
    {
        instantiateAudio(clip, transform, volume);
    }

    // plays an audio clip, fading out beginning at fadeBegin ms
    public void PlayClipWithFade(AudioClip clip, Transform transform, float volume, float fadeBegin)
    {
        if (fadeBegin < clip.length)
        {
            AudioSource source = instantiateAudio(clip, transform, volume);
            StartCoroutine(FadeOut(source, fadeBegin, (clip.length - fadeBegin) * 0.1f));
        }
        else
        {
            PlayClip(clip, transform, volume);
        }

    }

    private AudioSource instantiateAudio(AudioClip clip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(source, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        float duration = audioSource.clip.length;
        Destroy(audioSource.gameObject, duration);
        return audioSource;
    }

    IEnumerator FadeOut(AudioSource source, float delay, float fadeDuration)
    {
        yield return new WaitForSeconds(delay);
        float timeElapsed = 0;

        while (source.volume > 0)
        {
            source.volume = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return 0;
        }
    }
}
