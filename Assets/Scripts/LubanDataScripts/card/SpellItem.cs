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

public sealed partial class SpellItem :  Bright.Config.BeanBase 
{
    public SpellItem(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["CardName"].IsString) { throw new SerializationException(); }  CardName = _json["CardName"]; }
        { if(!_json["ElementType"].IsNumber) { throw new SerializationException(); }  ElementType = (Element)_json["ElementType"].AsInt; }
        { if(!_json["CardInfo"].IsString) { throw new SerializationException(); }  CardInfo = _json["CardInfo"]; }
        { var _json1 = _json["EffectInfos"]; if(!_json1.IsArray) { throw new SerializationException(); } EffectInfos = new System.Collections.Generic.List<effect.EffectInfo>(_json1.Count); foreach(JSONNode __e in _json1.Children) { effect.EffectInfo __v;  { if(!__e.IsObject) { throw new SerializationException(); }  __v = effect.EffectInfo.DeserializeEffectInfo(__e); }  EffectInfos.Add(__v); }   }
        PostInit();
    }

    public SpellItem(int id, string CardName, Element ElementType, string CardInfo, System.Collections.Generic.List<effect.EffectInfo> EffectInfos ) 
    {
        this.Id = id;
        this.CardName = CardName;
        this.ElementType = ElementType;
        this.CardInfo = CardInfo;
        this.EffectInfos = EffectInfos;
        PostInit();
    }

    public static SpellItem DeserializeSpellItem(JSONNode _json)
    {
        return new card.SpellItem(_json);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 卡名
    /// </summary>
    public string CardName { get; private set; }
    /// <summary>
    /// 增幅元素
    /// </summary>
    public Element ElementType { get; private set; }
    /// <summary>
    /// 补充描述
    /// </summary>
    public string CardInfo { get; private set; }
    public System.Collections.Generic.List<effect.EffectInfo> EffectInfos { get; private set; }

    public const int __ID__ = 1442281181;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var _e in EffectInfos) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in EffectInfos) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "CardName:" + CardName + ","
        + "ElementType:" + ElementType + ","
        + "CardInfo:" + CardInfo + ","
        + "EffectInfos:" + Bright.Common.StringUtil.CollectionToString(EffectInfos) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
