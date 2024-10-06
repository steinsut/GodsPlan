using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Coroutine fadeOutCoroutine = null;
    private Coroutine fadeInCoroutine = null;

    public void SetAudio(AudioClip clip) {
        audioSource.clip = clip;
    }

    private IEnumerator FadeOutRoutine(float fadeOutTime) {
        float elapsedTime = 0;
        audioSource.volume = 1;
        while (audioSource.volume > 0) {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(1, 0, elapsedTime/fadeOutTime);
            yield return null;
        }
        yield break;
    }

    private IEnumerator FadeInRoutine(float fadeInTime)
    {
        float elapsedTime = 0;
        audioSource.volume = 0;
        while (audioSource.volume < 1)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, 1, elapsedTime / fadeInTime);
            yield return null;
        }
        yield break;
    }

    public float GetTime() {
        return audioSource.time;
    }

    public void Seek(float time) {
        audioSource.time = time;
    }

    public void FadeOut(float fadeOutTime) {
        if (fadeInCoroutine != null) {
            StopCoroutine(fadeInCoroutine);
            fadeInCoroutine = null;
        }
        if(!audioSource.isPlaying) audioSource.Play();
        fadeOutCoroutine = StartCoroutine(FadeOutRoutine(fadeOutTime));
    }

    public bool CancelFadeOut() {
        if(fadeOutCoroutine == null) { 
            return false;
        }

        StopCoroutine(fadeOutCoroutine);
        fadeOutCoroutine = null;
        return true;
    }

    public void FadeIn(float fadeInTime)
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }
        if (!audioSource.isPlaying) audioSource.Play();
        fadeInCoroutine = StartCoroutine(FadeInRoutine(fadeInTime));
    }

    public bool CancelFadeIn()
    {
        if (fadeInCoroutine == null)
        {
            return false;
        }

        StopCoroutine(fadeInCoroutine);
        fadeInCoroutine = null;
        return true;
    }

    public void ResetVolume() {
        audioSource.volume = 0;
    }

}
