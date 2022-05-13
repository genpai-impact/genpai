using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制所有音乐的管理类
/// </summary>
public class AudioManager : MonoSingleton<AudioManager>
{


    // 当前播放的是谁
    private int playingIndex;

    // 是否允许播放音乐
    private bool canPlayAudio;

    // BGM组件
    public AudioSource BGMAudioSource;

    // 音效组件
    public AudioSource EffectAudioSource;

    // 将要轮流播放的音乐组
    public AudioClip[] BGMGroup;

    // 待优化音效字典
    public AudioClip[] EffectsGroup;

    void Start()
    {

        canPlayAudio = true;

        playingIndex = 0;
    }

    // 播放音效
    public void PlayerEffect(string name)
    {
        //if (i < EffectsGroup.Length)
        //{
        //    EffectAudioSource.clip = EffectsGroup[i];
        //    EffectAudioSource.Play();
        //}

        AkSoundEngine.PostEvent(name, gameObject);
    }


    void Update()
    {
        //if (canPlayAudio)
        //{
        //    PlayAudio();

        //    canPlayAudio = false;
        //}

        //if (!BGMAudioSource.isPlaying)
        //{
        //    playingIndex++;

        //    if (playingIndex >= BGMGroup.Length)
        //    {
        //        playingIndex = 0;
        //    }

        //    canPlayAudio = true;
        //}
    }


    private void PlayAudio()
    {
        BGMAudioSource.clip = BGMGroup[playingIndex];
        BGMAudioSource.Play();
    }

}
