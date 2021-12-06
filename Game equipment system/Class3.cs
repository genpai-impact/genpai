class Equips
{
	public string Name;//名称
	public int ID;//编号
	public string Attack;//攻击加成
	public string Defence;//防御加成
	public string Speed;//速度加成
	public EquipPlace Adapt;//适配位置
	public Equips Copy()//拷贝一份装备，用以装上装备时的计算
	{
		Equips e = new Equips();
		FieldInfo[] fi = GetType().GetFields();//获取装备类
		foreach (FieldInfo field in fi)
		{
			field.SetValue(e, field.GetValue(this));
		}
		return e;
	}
}



