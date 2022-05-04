using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class ReactionAnimator : SpecialAnimator
    {
        public ElementReactionEnum ReactionEnum;
        private GameObject reactionGameObject;

        private float reactionTime;

        private float reactionLength;

        public HashSet<string> clip_set = new HashSet<string>()
        {
            "感电",
            "蒸发",
            "融化",
            "解冻",
            "超导",
            "超载",
        };

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
            // reactionGameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            specialAnimator = reactionGameObject.GetComponent<Animator>();
            reactionLength = GetAnimatorLength();
        }

        public float GetAnimatorLength()
        {
            float length = 0;
            AnimationClip[] clips = specialAnimator.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in clips)
            {
                // Debug.Log(clip.name + clip.length);
                // if (clip.name.Equals("reaction"))
                if (clip_set.Contains(clip.name))
                {
                    length = clip.length;
                    Debug.Log(clip.name + clip.length);
                    break;
                }
            }
            return length;
        }

        public override bool IsAnimationFinished()
        {

            if (Time.time - reactionTime < reactionLength) return false;
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
                    // 未实现
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