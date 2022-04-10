
namespace Genpai
{
    /// <summary>
    /// 魔法：抽一定数量的卡
    /// <para>同元素增强：抽更多数量的卡</para>
    /// </summary>
    public class Spell_Draw_EnhanceDrawMore : BaseSpell
    {
        public override void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            base.Init(_elementType, _basaeNumerical, _enhanceNumerical);
            SelectType = SelectTargetType.None;
        }

        public override void Release(NewUnit sourceUnit, NewUnit targetUnit)
        {
            int drawNumber = BaseNumericalValue;
            if (ElementType == sourceUnit.ATKElement)
            {
                drawNumber = EnhanceNumericalValue;
            }

            GenpaiPlayer player = GameContext.Instance.GetPlayerBySite(sourceUnit.ownerSite);
            player.HandOutCard(drawNumber);
        }
    }
}