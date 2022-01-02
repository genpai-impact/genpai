namespace Genpai
{
    /// <summary>
    /// 元素
    /// </summary>
    public class Element : BaseBuff
    {
        /// <summary>
        /// 元素类型
        /// </summary>
        public ElementEnum ElementType
        {
            set; get;
        }
        /// <summary>
        /// 元素锁，锁了之后认为不存在元素
        /// </summary>
        public bool ElementLock
        {
            set; get;
        }
        /// <summary>
        /// 进行元素反应
        /// </summary>
        /// <param name="element"></param>
        public void ElementReaction(Element element)
        {
            // todo 仔细观察元素反应文档，想想怎么设计元素反应
        }

        public override void OnAttack()
        {
            throw new System.NotImplementedException();
        }

        public override void OnTakeDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}
