using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制所有音乐的管理类
/// </summary>
public class AudioManager : MonoBehaviour
{
    //将要轮流播放的音乐组
    public AudioClip[] audioGroup;

    //当前播放的是谁
    private int playingIndex;

    //是否允许播放音乐
    private bool canPlayAudio;

    //AudioSource组件
    private AudioSource audioSource;


    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        canPlayAudio = true;

        playingIndex = 0;
    }


    void Update()
    {
        if (canPlayAudio)
        {
            PlayAudio();

            canPlayAudio = false;
        }

        if (!audioSource.isPlaying)
        {
            playingIndex++;

            if (playingIndex >= audioGroup.Length)
            {
                playingIndex = 0;
            }

            canPlayAudio = true;
        }
    }


    private void PlayAudio()
    {
        audioSource.clip = audioGroup[playingIndex];
        audioSource.Play();
    }

}
