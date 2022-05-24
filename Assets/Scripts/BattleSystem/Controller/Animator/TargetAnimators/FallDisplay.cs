using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class FallDisplay : MonoBehaviour {
        private float _startTime;
        private static readonly int GameTime = Shader.PropertyToID("_GameTime");
        private static readonly int Clip = Shader.PropertyToID("_Clip");

        private void Awake() {
            // Debug.Log(Time.time);
            _startTime = Time.time;
            GetComponent<Animator>().SetTrigger("fall");
            
        }

        private void OnWillRenderObject() {
            // GetComponent<MeshRenderer>().material.shader = Shader.Find("Spine/DeadDissolve");
            // GetComponent<MeshRenderer>().material.SetFloat(GameTime, Time.time-_startTime);
            // GetComponent<MeshRenderer>().material.SetFloat(Clip, 0.1f);
            // GetComponent<MeshRenderer>().material.SetTex
        }
    }
}