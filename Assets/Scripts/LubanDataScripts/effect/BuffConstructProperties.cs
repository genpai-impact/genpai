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



namespace cfg.effect
{

/// <summary>
/// 用于构造Buff
/// </summary>
public sealed partial class BuffConstructProperties :  Bright.Config.BeanBase 
{
    public BuffConstructProperties(JSONNode _json) 
    {
        { if(!_json["buff_type"].IsString) { throw new SerializationException(); }  BuffType = _json["buff_type"]; }
        { if(!_json["construct_info"].IsString) { throw new SerializationException(); }  ConstructInfo = _json["construct_info"]; }
        { if(!_json["stories"].IsNumber) { throw new SerializationException(); }  Stories = _json["stories"]; }
        { if(!_json["life_cycles"].IsNumber) { throw new SerializationException(); }  LifeCycles = _json["life_cycles"]; }
        PostInit();
    }

    public BuffConstructProperties(string buff_type, string construct_info, int stories, int life_cycles ) 
    {
        this.BuffType = buff_type;
        this.ConstructInfo = construct_info;
        this.Stories = stories;
        this.LifeCycles = life_cycles;
        PostInit();
    }

    public static BuffConstructProperties DeserializeBuffConstructProperties(JSONNode _json)
    {
        return new effect.BuffConstructProperties(_json);
    }

    /// <summary>
    /// 构造类型
    /// </summary>
    public string BuffType { get; private set; }
    /// <summary>
    /// 构造备注
    /// </summary>
    public string ConstructInfo { get; private set; }
    /// <summary>
    /// 构造层数
    /// </summary>
    public int Stories { get; private set; }
    /// <summary>
    /// 构造时间
    /// </summary>
    public int LifeCycles { get; private set; }

    public const int __ID__ = 640024474;
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
        + "BuffType:" + BuffType + ","
        + "ConstructInfo:" + ConstructInfo + ","
        + "Stories:" + Stories + ","
        + "LifeCycles:" + LifeCycles + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}