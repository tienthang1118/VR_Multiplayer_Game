using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Utility;
public class AudioController : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource seSource;
    public AudioClip[] bgmClips;
    public AudioClip[] seClips;
    // Start is called before the first frame update
    void Start()
    {
        
        PlayBGM(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySE(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= seClips.Length)
        {
            Debug.LogError("Clip index out of range");
            return;
        }

        seSource.clip = seClips[clipIndex];
        seSource.Play();
    }
    public void PlayBGM(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= bgmClips.Length)
        {
            Debug.LogError("Clip index out of range");
            return;
        }

        bgmSource.clip = bgmClips[clipIndex];
        bgmSource.loop = true;
        bgmSource.Play();
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
