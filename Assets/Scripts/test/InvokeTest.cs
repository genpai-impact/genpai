using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeTest : MonoBehaviour
{
    public bool 开始invoke=false;
    public UnityAction action;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (开始invoke) {
            StartCoroutine(NextAfterAllAnimationOver());
        }
    }

    public UnityAction func()
    {
        Debug.LogWarning("开始invoke");
        return null;
    }

    private IEnumerator NextAfterAllAnimationOver()
    {
        
        yield return new WaitForSeconds(2);
        func().Invoke();
    }
}
