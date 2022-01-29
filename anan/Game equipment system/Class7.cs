public void Print()
{
    Console.WriteLine("装备列表：");
    foreach (EquipPlace qp in MyEquips.Keys)
    {
        Console.WriteLine(qp.ToString() + ":" + MyEquips[qp].ID);
    }
    Console.WriteLine("攻击:" + Attack);
    Console.WriteLine("防御:" + Defence);
    Console.WriteLine("速度:" + Speed);
    Console.WriteLine();
}


