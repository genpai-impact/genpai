using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//此层解决代码复用
public class MonsterCard : Card
{
    public void CancelSelected()
    {
        throw new NotImplementedException();
    }

    public object Clone()
    {
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        throw new NotImplementedException();
    }

    public void GameObject_Display()
    {
        throw new NotImplementedException();
    }

    public void GameObject_SetActive()
    {
        throw new NotImplementedException();
    }

    public void GameObject_SetContainer(GameObject parentGameObject)
    {
        throw new NotImplementedException();
    }

    public void GameObject_SetParent(GameObject parentGameObject)
    {
        throw new NotImplementedException();
    }

    public CardType GetCardType()
    {
        return CardType.Monster;
    }

    public int GetID()
    {
        throw new NotImplementedException();
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }

    public string MoveTo()
    {
        throw new NotImplementedException();
    }

    public void SetCanNotUse(CardType type)
    {
        throw new NotImplementedException();
    }

    public void SetCanUse(CardType type)
    {
        throw new NotImplementedException();
    }

    public void SetSelected()
    {
        throw new NotImplementedException();
    }

    public void Use()
    {
        throw new NotImplementedException();
    }
}

