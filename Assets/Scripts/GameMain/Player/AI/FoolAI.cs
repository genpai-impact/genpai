namespace Genpai
{
    public class FoolAI : BaseAI//do nothing������ʹ��
    {
        public FoolAI(AIType _Type, GenpaiPlayer _Player) : base(_Type, _Player) { }

        public override void CharaStrategy() { }//�Ͻ�ɫ����

        public override void MonsterStrategy() { }//�Ϲ������

        public override void AttackStrategy() { }//��������
    }
}