using DataScripts;
using UnityEngine;

namespace Utils
{


public class CardGroupLoader : MonoBehaviour
{
   //本来是放在CardGroupManager的start里面，但是忘记那个对象一开始是false了。。
    void Start()
    {
        LubanLoader.Init();
    }
       
}
}

