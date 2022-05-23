using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class FallAnimator : TargetAnimator
    {
        private readonly float _fallTime;

        public FallAnimator(Unit unit) : base(unit, AnimatorType.TargetAnimator.Fall)
        {
            _fallTime = Time.time;
        }

        public override void TargetAct()
        {
            // GameObject unitDisplayObject = UnitEntity.gameObject;
            // unitDisplayObject.GetComponent<UnitModelDisplay>().UnitModelAni.AddComponent<FallDisplay>();
            Animator.SetTrigger("fall");
        }

        public override bool IsAnimationFinished()
        {
            return !(Time.time - _fallTime < 3.0f);
        }

        public override void ShutDownAct()
        {
            // 删模型
            GameObject unitDisplayObject = UnitEntity.unitModelDisplay.animator.gameObject;
            Object.Destroy(unitDisplayObject);
            // BucketEntityManager.Instance.GetBucketBySerial(UnitEntity.Serial).transform.Find("Attacked")
            UnitEntity.unitDisplay.transform.parent.parent.Find("Attacked").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            UnitEntity.unitDisplay.Display(null);
            
            
            if (UnitView.UnitType != CardType.Chara) return;
            
            Chara chara = GameContext.GetPlayerBySite(UnitEntity.ownerSite).Chara;
            UnitEntity.unitDisplay.Display(chara.GetView());
        }
    }
}