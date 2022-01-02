
namespace Genpai
{
    public class Damage
    {
        /// <summary>
        /// 本次伤害结束后，执行的下一个伤害
        /// </summary>
        public Damage Next
        {
            get; set;
        }
        /// <summary>
        /// 伤害来源
        /// </summary>
        public GenpaiPlayer Resource
        {
            get; set;
        }
        /// <summary>
        /// 伤害目标
        /// </summary>
        public BaseUnit Target
        {
            get; set;
        }
        /// <summary>
        /// 造成的伤害
        /// </summary>
        public float DamageValue
        {
            get; set;
        }
        /// <summary>
        /// 本次攻击的元素属性
        /// </summary>
        public Element Element
        {
            get; set;
        }

        /// <summary>
        /// 向列表内新增一个伤害
        /// </summary>
        /// <param name="newDamage"></param>
        public void AddDamage(Damage newDamage)
        {
            if (Next == null)
            {
                Next = newDamage;
            }
            Damage temp = Next;
            for (; temp.Next != null;)
            {
                temp = temp.Next;
            }
            temp.Next = newDamage;
        }
        /// <summary>
        /// 造成伤害
        /// </summary>
        public void DoDamage()
        {
            Target.TakeDamage(this);
            if (Next != null)
            {
                Next.DoDamage();
            }
        }
    }
}
