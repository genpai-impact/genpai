using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeTest : MonoBehaviour
{
    public bool ��ʼinvoke=false;
    public UnityAction action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (��ʼinvoke) {
            StartCoroutine(NextAfterAllAnimationOver());
        }
    }

    public UnityAction func()
    {
        Debug.LogWarning("��ʼinvoke");
        return null;
    }

    private IEnumerator NextAfterAllAnimationOver()
    {
        
        yield return new WaitForSeconds(2);
        func().Invoke();
    }
}
