class YamlExpander
{
    private Dictionary<string, object> anchors = new Dictionary<string, object>();

    public object Expand(object obj)
    {
        if (obj is Dictionary<object, object> dict)
        {
            return ExpandDictionary(dict);
        }
        else if (obj is List<object> list)
        {
            return ExpandList(list);
        }
        return obj;
    }

    private Dictionary<object, object> ExpandDictionary(Dictionary<object, object> dict)
    {
        var result = new Dictionary<object, object>();
        foreach (var kvp in dict)
        {
            if (kvp.Key is string key && key.StartsWith("&"))
            {
                anchors[key.Substring(1)] = Expand(kvp.Value);
            }
            else if (kvp.Key is string mergeKey && mergeKey == "<<")
            {
                if (kvp.Value is List<object> mergeList)
                {
                    foreach (var item in mergeList)
                    {
                        if (item is string anchorRef && anchorRef.StartsWith("*") && anchors.TryGetValue(anchorRef.Substring(1), out var anchorValue))
                        {
                            MergeDictionaries(result, anchorValue as Dictionary<object, object>);
                        }
                    }
                }
                else if (kvp.Value is string anchorRef && anchorRef.StartsWith("*") && anchors.TryGetValue(anchorRef.Substring(1), out var anchorValue))
                {
                    MergeDictionaries(result, anchorValue as Dictionary<object, object>);
                }
            }
            else
            {
                result[kvp.Key] = Expand(kvp.Value);
            }
        }
        return result;
    }

    private List<object> ExpandList(List<object> list)
    {
        return list.ConvertAll(Expand);
    }

    private void MergeDictionaries(Dictionary<object, object> target, Dictionary<object, object> source)
    {
        if (source == null) return;
        foreach (var kvp in source)
        {
            if (!target.ContainsKey(kvp.Key))
            {
                target[kvp.Key] = kvp.Value;
            }
        }
    }
}