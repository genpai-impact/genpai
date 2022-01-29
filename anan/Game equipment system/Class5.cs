public void EquipEquipments(Equips Equipment)
{
    if (MyEquips.ContainsKey(Equipment.Adapt))//如果存在同位置的装备则先卸下原有装备
    {
        UnloadEquipments(MyEquips[Equipment.Adapt]);
    }
    Equipment = Equipment.Copy();//拷贝一份装备，防止修改原有属性
    if (Equipment.Attack.Substring(0, 1) == "f")
    {
        Equipment.Attack = "i" + (int)(Attack * float.Parse(Equipment.Attack.Remove(0, 1)) + 0.5f);//将百分比加成改为整数加成，方便后续计算
    }
    Attack += int.Parse(Equipment.Attack.Remove(0, 1));//在原基础上加成
    if (Equipment.Defence.Substring(0, 1) == "f")
    {
        Equipment.Defence = "i" + (int)(Defence * float.Parse(Equipment.Defence.Remove(0, 1)) + 0.5f);
    }
    Defence += int.Parse(Equipment.Defence.Remove(0, 1));
    if (Equipment.Speed.Substring(0, 1) == "f")
    {
        Equipment.Speed = "i" + (int)(Speed * float.Parse(Equipment.Speed.Remove(0, 1)) + 0.5f);
    }
    Speed += int.Parse(Equipment.Speed.Remove(0, 1));
    MyEquips.Add(Equipment.Adapt, Equipment);//添加装备到自身装备列表
}


