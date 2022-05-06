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



namespace cfg.skill
{

public sealed partial class SkillItems
{
    private readonly Dictionary<int, skill.SkillItem> _dataMap;
    private readonly List<skill.SkillItem> _dataList;
    
    public SkillItems(JSONNode _json)
    {
        _dataMap = new Dictionary<int, skill.SkillItem>();
        _dataList = new List<skill.SkillItem>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = skill.SkillItem.DeserializeSkillItem(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, skill.SkillItem> DataMap => _dataMap;
    public List<skill.SkillItem> DataList => _dataList;

    public skill.SkillItem GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public skill.SkillItem Get(int key) => _dataMap[key];
    public skill.SkillItem this[int key] => _dataMap[key];

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