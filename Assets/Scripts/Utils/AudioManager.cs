using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 控制所有音乐的管理类
    /// </summary>
    public class AudioManager : MonoSingleton<AudioManager>
    {


        // 播放音效
        public void PlayerEffect(string name)
        {
            //if (i < EffectsGroup.Length)
            //{
            //    EffectAudioSource.clip = EffectsGroup[i];
            //    EffectAudioSource.Play();
            //}
            try
            {
                AkSoundEngine.PostEvent(name, gameObject);
            }
            catch
            {
                Debug.Log("Play Event "+name+" Error");
            }
        
        }
    

    }
}
