using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    static public SoundManager instance_;
    private Coroutine soundCor;

    public void Start()
    {
        if (instance_ == null)
            instance_ = this;
    }

    public void SFXPlay(AudioClip clip, float time)
    {
        if(soundCor != null)
            StopCoroutine(soundCor);

        soundCor = StartCoroutine(SFXPlayCor(clip, time));
    }

    IEnumerator SFXPlayCor(AudioClip clip, float time)
    {
        bgmSource.volume = 0.1f;
        sfxSource.PlayOneShot(clip);
        yield return new WaitForSeconds(time);
        bgmSource.volume = 0.3f;
    }

}
