using System;
using Messager;

namespace Genpai
{
    /// </summary>
    /// AI����
    /// </summary>
    public enum AIType
    {
        SimpleAI,//�ݶ���
        FoolAI
    };

    /// <summary>
    /// AI����
    /// </summary>
    public abstract class BaseAI 
    {
        public AIType AItype;
        public GenpaiPlayer Player;
        public int _currentRound = 0;

        public BaseAI(AIType _Type, GenpaiPlayer _Player)
        {
            AItype = _Type;
            Player = _Player;
        }

        public abstract void CharaStrategy();//�Ͻ�ɫ����

        public abstract void MonsterStrategy();//�Ϲ������

        public abstract void AttackStrategy();//��������

        public void EndRound()//�����غ�
        {
            Player.GenpaiController.EndRound();
        }


        //��������
        //getname

        public virtual void Subscribe()
        {
        }
    }
}