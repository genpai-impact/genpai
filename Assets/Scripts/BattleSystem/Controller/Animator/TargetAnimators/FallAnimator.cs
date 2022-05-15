using UnityEditor;
using UnityEngine;

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

            GameObject gameObject = BucketEntityManager.Instance.GetBucketBySerial(UnitEntity.Serial).transform.Find("Unit").gameObject;
            GameObject unitDisplayObject = gameObject.transform.Find("UnitDisplay(Clone)").gameObject;
            unitDisplayObject.GetComponent<UnitModelDisplay>().UnitModelAni.AddComponent<FallDisplay>();
        }

        public override bool IsAnimationFinished()
        {
            return !(Time.time - _fallTime < 3.0f);
        }

        public override void ShutDownAct()
        {
            GameObject gameObject = BucketEntityManager.Instance.GetBucketBySerial(UnitEntity.Serial).transform.Find("Unit").gameObject;
            GameObject unitDisplayObject = gameObject.transform.Find("UnitDisplay(Clone)").gameObject;
            Object.Destroy(unitDisplayObject);

            BucketEntityManager.Instance.GetBucketBySerial(UnitEntity.Serial).transform.Find("Attacked").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}