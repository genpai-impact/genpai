using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class FallAnimator : TargetAnimator
    {
        float fallTime;

        public FallAnimator(Unit _unit) : base(_unit, AnimatorType.TargetAnimator.Fall)
        {
            fallTime = Time.time;
        }

        public override void TargetAct()
        {
            /*
            unitEntity.GetComponent<UnitDisplay>().Init(null);

            GameObject gameObject = BucketEntityManager.Instance.GetBucketBySerial(unitEntity.serial).transform.Find("Unit").gameObject;
            GameObject unitDisplayObject = gameObject.transform.Find("UnitDisplay(Clone)").gameObject;
            GameObject.Destroy(unitDisplayObject);
            */
            GameObject gameObject = BucketEntityManager.Instance.GetBucketBySerial(unitEntity.serial).transform.Find("Unit").gameObject;
            GameObject unitDisplayObject = gameObject.transform.Find("UnitDisplay(Clone)").gameObject;
            unitDisplayObject.GetComponent<UnitModelDisplay>().UnitModelAni.AddComponent<FallDisplay>();
        }

        public override bool IsAnimationFinished()
        {
            if (Time.time - fallTime < 3.0f) return false;
            else
            {
                /*
                GameObject gameObject = BucketEntityManager.Instance.GetBucketBySerial(unitEntity.serial).transform.Find("Unit").gameObject;
                GameObject unitDisplayObject = gameObject.transform.Find("UnitDisplay(Clone)").gameObject;
                GameObject.Destroy(unitDisplayObject);
                */
                return true;
            }
        }

        public override void ShutDownAct()
        {
            GameObject gameObject = BucketEntityManager.Instance.GetBucketBySerial(unitEntity.serial).transform.Find("Unit").gameObject;
            GameObject unitDisplayObject = gameObject.transform.Find("UnitDisplay(Clone)").gameObject;
            GameObject.Destroy(unitDisplayObject);

            BucketEntityManager.Instance.GetBucketBySerial(unitEntity.serial).transform.Find("Attacked").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}