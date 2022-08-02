using BattleSystem.Controller.Bucket;
using BattleSystem.Service.Common;
using BattleSystem.Service.Effect;
using BattleSystem.Service.Player;
using System.Collections;
using UnityEngine;

using Utils;

namespace BattleSystem.Controller.Animator.SourceAnimators
{
    public class AttackAnimator : SourceAnimator
    {
        private readonly Vector3 _sourceVector;

        private readonly Vector3 _targetVector;

        private readonly GameObject _attackObject;
        
        private int _index=0;

       

        private BattleSite _attackBattleSite;
        private static readonly int Atk = UnityEngine.Animator.StringToHash("atk");

        public AttackAnimator(Service.Unit.Unit unit, Damage damage) : base(unit, AnimatorType.SourceAnimator.Attack)
        {
            _sourceVector = BucketEntityManager.Instance.GetBucketBySerial(damage.GetSource().Carrier.serial).transform.position;
            _targetVector = BucketEntityManager.Instance.GetBucketBySerial(damage.GetTarget().Carrier.serial).transform.position;

            _attackObject = UnitEntity.carrier.gameObject;

            _attackBattleSite = damage.GetSource().Carrier.ownerSite;
        }

       /* private IEnumerator AnimationAfterReach()
        {   
            //Debug.LogWarning("start  "+(_attackObject.transform.position - _targetVector).magnitude);
            while ((_attackObject.transform.position - _targetVector).magnitude > 1)
            {
                Debug.LogWarning("atk1  " + (_attackObject.transform.position - _targetVector).magnitude);
                yield return null;
            }
            Debug.LogWarning("atk2");
            Animator.SetTrigger(Atk);
        }*/
        private IEnumerator finishAction()
        {
            //Debug.LogWarning("back"+ _index);
            while (!Animator.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                //Debug.LogWarning("协程1ing");
                yield return null;
            }
            while (Animator.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                //Debug.LogWarning("协程2ing");
                yield return null;
            }
            //Debug.LogWarning("关闭atk: "+Animator.name);
            _isacting = false;
            AnimationHandle.Instance.DeleteAnimator("atk", Animator);
            yield break;
        }

        public override void SourceAct()
        {

            if (IsTriggerExist(Animator, "atk"))
            {
                _isacting = true;
                AnimationHandle.Instance.AddAnimator("atk", Animator);
                //Debug.LogWarning("start1  " + (_attackObject.transform.position - _targetVector).magnitude);
                _attackObject.transform.position = _targetVector;
               
                //_attackObject.transform.Translate((_sourceVector - _targetVector).normalized * 4);

                //采用协程控制，达到移动到位置（延时）后播放动画(未来可能使用)
                //mono.StartCoroutine(AnimationAfterReach());
                Animator.SetTrigger(Atk);//暂时用瞬移至敌方脸上
                //Debug.LogWarning("monocont"+ _allmono.Length);
                //Debug.LogWarning("front" + ++_index);
                //_mono=GameObject.FindObjectOfType<MonoBehaviour>();
                //Debug.LogWarning("findmono"+_mono.gameObject.name);
                _mono.StartCoroutine(finishAction());
                // AudioManager.Instance.PlayerEffect("Play_bells_2");
                AudioManager.Instance.PlayerEffect("Effect_Attack_Smash");
            }
        }




        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "atk")) return true;
            
            //return !Animator.GetBool(Atk);atc为trigger，不能获取状态
            return !_isacting;
        }

        //回到原来位置
        public override void ShutDownAct()
        {
            _attackObject.transform.position = _sourceVector;
            UnitEntity.unitDisplay.UnitColorChange();
        }
    }
}