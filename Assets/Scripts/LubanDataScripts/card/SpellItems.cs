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

public sealed partial class SpellItems
{
    private readonly Dictionary<int, card.SpellItem> _dataMap;
    private readonly List<card.SpellItem> _dataList;
    
    public SpellItems(JSONNode _json)
    {
        _dataMap = new Dictionary<int, card.SpellItem>();
        _dataList = new List<card.SpellItem>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = card.SpellItem.DeserializeSpellItem(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, card.SpellItem> DataMap => _dataMap;
    public List<card.SpellItem> DataList => _dataList;

    public card.SpellItem GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public card.SpellItem Get(int key) => _dataMap[key];
    public card.SpellItem this[int key] => _dataMap[key];

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