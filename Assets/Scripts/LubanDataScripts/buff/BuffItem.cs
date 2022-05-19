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



namespace cfg.buff
{

public sealed partial class BuffItem :  Bright.Config.BeanBase 
{
    public BuffItem(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["BuffName_zh"].IsString) { throw new SerializationException(); }  BuffNameZh = _json["BuffName_zh"]; }
        { if(!_json["BuffName"].IsString) { throw new SerializationException(); }  BuffName = _json["BuffName"]; }
        { if(!_json["ConstructProperties"].IsObject) { throw new SerializationException(); }  ConstructProperties = effect.BuffConstructProperties.DeserializeBuffConstructProperties(_json["ConstructProperties"]); }
        { if(!_json["OverrideProperties"].IsNumber) { throw new SerializationException(); }  OverrideProperties = (effect.BuffPropertiesOverrideable)_json["OverrideProperties"].AsInt; }
        PostInit();
    }

    public BuffItem(int id, string BuffName_zh, string BuffName, effect.BuffConstructProperties ConstructProperties, effect.BuffPropertiesOverrideable OverrideProperties ) 
    {
        this.Id = id;
        this.BuffNameZh = BuffName_zh;
        this.BuffName = BuffName;
        this.ConstructProperties = ConstructProperties;
        this.OverrideProperties = OverrideProperties;
        PostInit();
    }

    public static BuffItem DeserializeBuffItem(JSONNode _json)
    {
        return new buff.BuffItem(_json);
    }

    /// <summary>
    /// BuffId
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// Buff名称
    /// </summary>
    public string BuffNameZh { get; private set; }
    /// <summary>
    /// Buff英文名
    /// </summary>
    public string BuffName { get; private set; }
    /// <summary>
    /// Buff类型
    /// </summary>
    public effect.BuffConstructProperties ConstructProperties { get; private set; }
    /// <summary>
    /// 重载参数
    /// </summary>
    public effect.BuffPropertiesOverrideable OverrideProperties { get; private set; }

    public const int __ID__ = -937651871;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        ConstructProperties?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        ConstructProperties?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "BuffNameZh:" + BuffNameZh + ","
        + "BuffName:" + BuffName + ","
        + "ConstructProperties:" + ConstructProperties + ","
        + "OverrideProperties:" + OverrideProperties + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
