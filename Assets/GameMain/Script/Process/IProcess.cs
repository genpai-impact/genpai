namespace Genpai
{
    /// <summary>
    /// ��Ϸ�е�ĳ������
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// ִ�и�����
        /// </summary>
        public void Run();

        /// <summary>
        /// ��ȡ�����̵�����
        /// </summary>
        public string GetName();
    }
}