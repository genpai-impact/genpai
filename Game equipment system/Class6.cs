public void UnloadEquipments(Equips Equipment)
{
    Attack -= int.Parse(Equipment.Attack.Remove(0, 1));//减去加成整数
    Defence -= int.Parse(Equipment.Defence.Remove(0, 1));
    Speed -= int.Parse(Equipment.Speed.Remove(0, 1));
    MyEquips.Remove(Equipment.Adapt);//从自身装备列表中去掉该装备
}


