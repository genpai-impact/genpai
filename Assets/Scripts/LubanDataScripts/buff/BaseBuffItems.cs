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

public sealed partial class BaseBuffItems
{
    private readonly Dictionary<string, buff.BaseBuff> _dataMap;
    private readonly List<buff.BaseBuff> _dataList;
    
    public BaseBuffItems(JSONNode _json)
    {
        _dataMap = new Dictionary<string, buff.BaseBuff>();
        _dataList = new List<buff.BaseBuff>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = buff.BaseBuff.DeserializeBaseBuff(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.BaseBuffName, _v);
        }
        PostInit();
    }

    public Dictionary<string, buff.BaseBuff> DataMap => _dataMap;
    public List<buff.BaseBuff> DataList => _dataList;

    public buff.BaseBuff GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public buff.BaseBuff Get(string key) => _dataMap[key];
    public buff.BaseBuff this[string key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    
    partial void PostInit();
    partial void PostResolve();
}

}