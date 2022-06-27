using System.Collections.Generic;
using System.Linq;
using BattleSystem.Service.Element;
using UnityEngine;

namespace BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator
{
    public class ReactionAnimator : SpecialAnimator
    {
        public ElementReactionEnum ReactionEnum;
        private GameObject _reactionGameObject;

        private float _reactionTime;

        private float _reactionLength;

        private readonly HashSet<string> _clipSet = new HashSet<string>()
        {
            "感电",
            "蒸发",
            "融化",
            "解冻",
            "超导",
            "超载",
        };

        public ReactionAnimator(Service.Unit.Unit unit) : base(unit, AnimatorType.SpecialAnimator.Reaction)
        {
            FeatureTypeEnum = AnimatorType.AnimatorTypeEnum.SourceAnimator;
        }

        public override void SpecialAct()
        {
            if (ReactionEnum == ElementReactionEnum.None) return;

            _reactionTime = Time.time;

            Transform unitDisplay = UnitEntity.gameObject.transform;

            GameObject reactionPrefab = Resources.Load("Prefabs/Reaction/" + ReactionEnum.ToString()) as GameObject;

            _reactionGameObject = Object.Instantiate(reactionPrefab, unitDisplay);
            // reactionGameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            Animator = _reactionGameObject.GetComponent<UnityEngine.Animator>();
            _reactionLength = GetAnimatorLength();
        }

        public float GetAnimatorLength()
        {
            AnimationClip[] clips = Animator.runtimeAnimatorController.animationClips;

            return (from clip in clips where _clipSet.Contains(clip.name) select clip.length).FirstOrDefault();
        }

        public override bool IsAnimationFinished()
        {
            return !(Time.time - _reactionTime < _reactionLength);
        }

        public override void ShutDownAct()
        {
            Object.Destroy(_reactionGameObject);
        }

        public static ReactionAnimator GenerateReactionAnimator(Service.Unit.Unit unit, ElementReactionEnum reactionEnum)
        {
            return reactionEnum switch
            {
                ElementReactionEnum.Swirl => new SwrilAnimator(unit), //未实现
                ElementReactionEnum.Melt => new MeltAnimator(unit),
                ElementReactionEnum.Superconduct => new SuperconductAnimator(unit),
                ElementReactionEnum.Overload => new OverloadAnimator(unit),
                ElementReactionEnum.Vaporise => new VaporiseAnimator(unit),
                ElementReactionEnum.ElectroCharge => new ElectroChargeAnimator(unit),
                _ => new ReactionAnimator(unit)
            };
        }
    }
}