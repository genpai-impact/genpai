static void Main(string[] args)
{
    People Me = new People();
    Equips[] AllEquipments = new Equips[6];
    for (int i = 0; i < 6; i++)
    {
        AllEquipments[i] = new Equips(i);
    }
    Me.Attack = 20;
    Me.Defence = 10;
    Me.Speed = 30;
    Me.Print();
    Me.EquipEquipments(AllEquipments[0]);
    Me.Print();
    Me.EquipEquipments(AllEquipments[1]);
    Me.Print();
    Me.EquipEquipments(AllEquipments[2]);
    Me.Print();
    Me.UnloadEquipments(AllEquipments[1]);
    Me.Print();
    Me.EquipEquipments(AllEquipments[4]);
    Me.Print();
    Me.EquipEquipments(AllEquipments[0]);
    Me.Print();
    Console.Read();
}


