using BattleSystem.Service.Common;
using BattleSystem.Service.Unit;
using UnityEngine;
using System.Collections;
using UnityEngine;

namespace BattleSystem.Controller.Animator.TargetAnimators
{
    /// <summary>
    /// </summary>
    public class FallAnimator : TargetAnimator
    {
        private readonly float _fallTime;
 

        public FallAnimator(Service.Unit.Unit unit) : base(unit, AnimatorType.TargetAnimator.Fall)
        {
            _fallTime = Time.time;
        }

        public override void TargetAct()
        {
            // GameObject unitDisplayObject = UnitEntity.gameObject;
            // unitDisplayObject.GetComponent<UnitModelDisplay>().UnitModelAni.AddComponent<FallDisplay>();
            
            if (IsTriggerExist(Animator, "fall"))
            {
                _isacting = true;
                Debug.Log(UnitView.UnitName + "fall");
                AnimationHandle.Instance.AddAnimator("fall", Animator);
                Animator.SetTrigger("fall");
                //_mono.StartCoroutine(finishAction());
                Animator.Play("fall");
            }
        }

        private IEnumerator finishAction()
        {
            while (!Animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
            {
                Debug.LogWarning("协程ing");
                yield return null;
            }
            while (Animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
            {
                Debug.LogWarning("协程ing");
                yield return null;
            }
            _isacting = false;
            AnimationHandle.Instance.DeleteAnimator("fall", Animator);
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "fall")) return true;
            return Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f;
        }

        public override void ShutDownAct()
        {
            // 删模型
            // GameObject unitDisplayObject = UnitEntity.unitModelDisplay.animator.gameObject;
            // Object.Destroy(unitDisplayObject);
            // BucketEntityManager.Instance.GetBucketBySerial(UnitEntity.Serial).transform.Find("Attacked")
            UnitEntity.unitDisplay.Display(null);
            UnitEntity.unitDisplay.transform.parent.parent.Find("Attacked").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            
            
            if (UnitView.UnitType != CardType.Chara) return;
            
            Chara chara = GameContext.GetPlayerBySite(UnitEntity.ownerSite).Chara;
            UnitEntity.unitDisplay.Display(chara.GetView());
        }
    }
}