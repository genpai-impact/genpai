using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class ReactionAnimator : SpecialAnimator
    {
        public string reactionName;
        private GameObject reactionGameObject;

        private float reactionTime;

        public ReactionAnimator(Unit _unit, AnimatorType.SpecialAnimator _specialAnimatorType) : base(_unit, _specialAnimatorType)
        {
        }

        public ReactionAnimator(Unit _unit) : base(_unit)
        {
        }

        public override void SpecialAct()
        {
            if(reactionName==null) return;
            reactionTime = Time.time;

            GameObject gameObject = BucketEntityManager.Instance.GetBucketBySerial(unitEntity.serial).transform.Find("Unit").gameObject;
            GameObject unitDisplayObject = gameObject.transform.Find("UnitDisplay(Clone)").gameObject;

            GameObject BuffOverlayPrefab = Resources.Load("Prefabs/"+reactionName) as GameObject;
            reactionGameObject = GameObject.Instantiate(BuffOverlayPrefab, unitDisplayObject.transform);
            reactionGameObject.transform.localScale = new Vector3(1, 1, 0);
        }

        public override bool IsAnimationFinished()
        {
            if(Time.time-reactionTime<4.0f) return false;
            return true;
        }

        public override void ShutDownAct()
        {
            GameObject.Destroy(reactionGameObject);
        }

        static public ReactionAnimator GenerateReactionAnimator(Unit unit, ElementReactionEnum reactionEnum)
        {
            switch(reactionEnum){
                case ElementReactionEnum.Swirl:
                    return new SwrilAnimator(unit, AnimatorType.SpecialAnimator.Reaction);
                case ElementReactionEnum.Melt:
                    return new MeltAnimator(unit, AnimatorType.SpecialAnimator.Reaction);
                case ElementReactionEnum.Superconduct:
                    return new SuperconductAnimator(unit, AnimatorType.SpecialAnimator.Reaction);
                case ElementReactionEnum.Overload:
                    return new OverloadAnimator(unit, AnimatorType.SpecialAnimator.Reaction);
                case ElementReactionEnum.Vaporise:
                    return new VaporiseAnimator(unit, AnimatorType.SpecialAnimator.Reaction);
                default:
                    return new ReactionAnimator(unit);
            }
        }
    }
}