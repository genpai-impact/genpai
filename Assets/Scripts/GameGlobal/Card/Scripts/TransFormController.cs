using BattleSystem.Service.MessageDatas;
using UnityEngine;


/// <summary>
/// 卡牌动画管理器
/// 主要管理卡牌的移动动画
/// </summary>
public class TransFormController : MonoBehaviour
{
    /// <summary>
    /// TODO：由HandCardManager动态分配位置
    /// </summary>
    public Vector3 targetPosition;      //移动目标位置
    public float ConvergentSpeed = 0.99f;
    public float x = 0.99f;
    private int _times = 0;
    

    // Use this for initialization
    private void Awake()
    {
        _times = 0;
    }

    // Update is called once per frame
    // fixme 当一张手牌还没有完全移动好的时候，就再次点击，回手操作会让牌变得鬼畜。
    void FixedUpdate()
    {
        if (_times >= 70) { }
        else
        {
            _times++;
            Vector3 origin = transform.localPosition;
            Vector3 temp = Vector3.Lerp(origin, targetPosition, -x + 1);
            var localPosition = temp;
            localPosition += new Vector3(1, 0, 0);
            transform.localPosition = localPosition;
            x *= ConvergentSpeed;
        }
    }

    public void MoveTo(Vector3 target)
    {
            _times = 0;
            x = ConvergentSpeed;
            targetPosition = target;
    }

    public void SetPosition(Vector3 target)
    {
        transform.localPosition = target;
    }

}