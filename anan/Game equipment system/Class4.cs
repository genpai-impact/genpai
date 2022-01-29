public Equips(int ID)//以ID作为装备识别区分
{
    this.ID = ID;
    switch (ID)
    {
        case 0:
            Attack = "i80";
            Defence = "i0";
            Speed = "i0";
            Adapt = EquipPlace.Head;
            break;
        case 1:
            Attack = "i0";
            Defence = "i50";
            Speed = "i20";
            Adapt = EquipPlace.Clothes;
            break;
        case 2:
            Attack = "i0";
            Defence = "i10";
            Speed = "i15";
            Adapt = EquipPlace.LHand;
            break;
        case 3:
            Attack = "i0";
            Defence = "i10";
            Speed = "i15";
            Adapt = EquipPlace.RHand;
            break;
        case 4:
            Attack = "f0.3";
            Defence = "i0";
            Speed = "i0";
            Adapt = EquipPlace.Head;
            break;
        case 5:
            Attack = "i0";
            Defence = "f0.2";
            Speed = "f0.05";
            Adapt = EquipPlace.Clothes;
            break;
    }
}




