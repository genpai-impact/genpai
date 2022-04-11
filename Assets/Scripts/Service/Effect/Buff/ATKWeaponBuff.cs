
using Messager;

namespace Genpai
{
    /// <summary>
    /// 用于日后实现武器加的攻击力
    /// </summary>
    public class ATKWeaponBuff : BaseATKEnhanceBuff, IBuffDeleteable, IMessageReceiveHandler
    {
        public ATKWeaponBuff(int _storey)
        {
            buffType = BuffType.ATKEnhanceBuff;
            buffName = BuffEnum.ATKBuff;
            Storey = _storey;
        }

        public override void AddBuff(NewUnit _target)
        {
            base.AddBuff(_target);
            //_target.GetComponent<UnitDisplay>().FreshUnitUI();
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            trigger = false;
            target.buffAttachment.Remove(this);
            //target.GetComponent<UnitDisplay>().FreshUnitUI();
        }

        // 待实现
        public void Subscribe()
        {

        }
    }
}