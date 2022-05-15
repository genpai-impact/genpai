
using Messager;

namespace Genpai
{
    /// <summary>
    /// 用于日后实现武器加的攻击力
    /// </summary>
    public class ATKWeaponBuff : BaseAtkEnhanceBuff, IBuffDeleteable, IMessageReceiveHandler
    {
        public ATKWeaponBuff(int _storey)
        {
            BuffType = BuffType.ATKEnhanceBuff;
            BuffName = BuffEnum.AtkBuff;
            Storey = _storey;
        }

        public override void AddBuff(Unit _target)
        {
            base.AddBuff(_target);
            //_target.GetComponent<UnitDisplay>().FreshUnitUI();
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            Trigger = false;
            Target.BuffAttachment.Remove(this);
            //target.GetComponent<UnitDisplay>().FreshUnitUI();
        }

        // 待实现
        public void Subscribe()
        {

        }
    }
}