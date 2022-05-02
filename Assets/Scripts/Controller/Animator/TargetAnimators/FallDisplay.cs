using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class FallDisplay : MonoBehaviour {
        private float startTime;

        private void Awake() {
            Debug.Log(Time.time);
            startTime = Time.time;
            GetComponent<Animator>().SetTrigger("fall");
        }

        private void OnWillRenderObject() {
            GetComponent<MeshRenderer>().material.shader = Shader.Find("Spine/DeadDissolve");
            GetComponent<MeshRenderer>().material.SetFloat("_GameTime", Time.time-startTime);
            GetComponent<MeshRenderer>().material.SetFloat("_Clip", 0.1f);
            // GetComponent<MeshRenderer>().material.SetTex
        }
    }
}