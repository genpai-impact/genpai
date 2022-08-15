using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface Card : ICloneable
{
    public int GetID();
    public string GetName();
    public string MoveTo();

    public GameObject GetGameObject();//删除
    public void GameObject_Display();
    public void GameObject_SetActive();
    public void Use();
    public void GameObject_SetContainer(GameObject parentGameObject);
    public CardType GetCardType();

    public void SetSelected();
    public void CancelSelected();

    public void SetCanUse(CardType type);
    public void SetCanNotUse(CardType type);

    public void Destroy();
}

