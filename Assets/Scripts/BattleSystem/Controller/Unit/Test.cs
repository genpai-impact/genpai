using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 单位显示模块，管理单位UI显示
    /// </summary>
    public class Test : MonoBehaviour
    {
        private void Start() {
        }

        private void Update() {
        }

        private void OnWillRenderObject() {
            GameObject obj = GameObject.Find("HelloWorld");
            Debug.Log(obj.GetComponent<MeshRenderer>().material.shader.name);
            obj.GetComponent<MeshRenderer>().material.shader = Shader.Find("Spine/DeadDissolve");
            Debug.Log(obj.GetComponent<MeshRenderer>().material.shader.name);
        }
    }
}