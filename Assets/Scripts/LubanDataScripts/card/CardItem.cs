//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.card
{

public sealed partial class CardItem :  Bright.Config.BeanBase 
{
    public CardItem(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["CardType"].IsNumber) { throw new SerializationException(); }  CardType = (card.CardType)_json["CardType"].AsInt; }
        { if(!_json["CardName_zh"].IsString) { throw new SerializationException(); }  CardNameZh = _json["CardName_zh"]; }
        { if(!_json["CardName"].IsString) { throw new SerializationException(); }  CardName = _json["CardName"]; }
        { if(!_json["CardInfo"].IsString) { throw new SerializationException(); }  CardInfo = _json["CardInfo"]; }
        { if(!_json["HP"].IsNumber) { throw new SerializationException(); }  HP = _json["HP"]; }
        { if(!_json["ATK"].IsNumber) { throw new SerializationException(); }  ATK = _json["ATK"]; }
        { if(!_json["ATKElement"].IsNumber) { throw new SerializationException(); }  ATKElement = (common.Element)_json["ATKElement"].AsInt; }
        { if(!_json["SelfElement"].IsNumber) { throw new SerializationException(); }  SelfElement = (common.Element)_json["SelfElement"].AsInt; }
        { if(!_json["Charge"].IsNumber) { throw new SerializationException(); }  Charge = _json["Charge"]; }
        PostInit();
    }

    public CardItem(int id, card.CardType CardType, string CardName_zh, string CardName, string CardInfo, int HP, int ATK, common.Element ATKElement, common.Element SelfElement, int Charge ) 
    {
        this.Id = id;
        this.CardType = CardType;
        this.CardNameZh = CardName_zh;
        this.CardName = CardName;
        this.CardInfo = CardInfo;
        this.HP = HP;
        this.ATK = ATK;
        this.ATKElement = ATKElement;
        this.SelfElement = SelfElement;
        this.Charge = Charge;
        PostInit();
    }

    public static CardItem DeserializeCardItem(JSONNode _json)
    {
        return new card.CardItem(_json);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 单位卡类型
    /// </summary>
    public card.CardType CardType { get; private set; }
    /// <summary>
    /// 单位名称_中文
    /// </summary>
    public string CardNameZh { get; private set; }
    /// <summary>
    /// 单位名称_英文
    /// </summary>
    public string CardName { get; private set; }
    /// <summary>
    /// 卡牌描述
    /// </summary>
    public string CardInfo { get; private set; }
    /// <summary>
    /// 单位血量
    /// </summary>
    public int HP { get; private set; }
    /// <summary>
    /// 单位攻击
    /// </summary>
    public int ATK { get; private set; }
    /// <summary>
    /// 攻击元素
    /// </summary>
    public common.Element ATKElement { get; private set; }
    /// <summary>
    /// 自身元素
    /// </summary>
    public common.Element SelfElement { get; private set; }
    /// <summary>
    /// 充能系数
    /// </summary>
    public int Charge { get; private set; }

    public const int __ID__ = -1993953407;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "CardType:" + CardType + ","
        + "CardNameZh:" + CardNameZh + ","
        + "CardName:" + CardName + ","
        + "CardInfo:" + CardInfo + ","
        + "HP:" + HP + ","
        + "ATK:" + ATK + ","
        + "ATKElement:" + ATKElement + ","
        + "SelfElement:" + SelfElement + ","
        + "Charge:" + Charge + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
