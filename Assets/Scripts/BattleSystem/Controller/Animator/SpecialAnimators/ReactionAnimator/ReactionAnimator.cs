using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class ReactionAnimator : SpecialAnimator
    {
        public ElementReactionEnum ReactionEnum;
        private GameObject reactionGameObject;

        private float reactionTime;

        public ReactionAnimator(Unit _unit) : base(_unit, AnimatorType.SpecialAnimator.Reaction)
        {
            featureTypeEnum = AnimatorType.AnimatorTypeEnum.SourceAnimator;
        }

        public override void SpecialAct()
        {
            if (ReactionEnum == ElementReactionEnum.None) return;

            reactionTime = Time.time;

            Transform unitDisplay = unitEntity.gameObject.transform;

            GameObject ReactionPrefab = Resources.Load("Prefabs/Reaction/" + ReactionEnum.ToString()) as GameObject;

            reactionGameObject = GameObject.Instantiate(ReactionPrefab, unitDisplay);
            reactionGameObject.transform.localScale = new Vector3(1, 1, 0);
        }

        public override bool IsAnimationFinished()
        {
            if (Time.time - reactionTime < 3.5f) return false;
            return true;
        }

        public override void ShutDownAct()
        {
            GameObject.Destroy(reactionGameObject);
        }

        static public ReactionAnimator GenerateReactionAnimator(Unit unit, ElementReactionEnum reactionEnum)
        {
            switch (reactionEnum)
            {
                case ElementReactionEnum.Swirl:
                    // Î´ÊµÏÖ
                    return new SwrilAnimator(unit);
                case ElementReactionEnum.Melt:
                    return new MeltAnimator(unit);
                case ElementReactionEnum.Superconduct:
                    return new SuperconductAnimator(unit);
                case ElementReactionEnum.Overload:
                    return new OverloadAnimator(unit);
                case ElementReactionEnum.Vaporise:
                    return new VaporiseAnimator(unit);
                case ElementReactionEnum.ElectroCharge:
                    return new ElectroChargeAnimator(unit);
                default:
                    return new ReactionAnimator(unit);
            }
        }
    }
}