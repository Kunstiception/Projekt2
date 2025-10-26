using System.Collections.Generic;
using UnityEngine;

public class SceneItemConfiguration
{
    public string SceneName;
    public List<Vector2> Positions = new List<Vector2>();
    public List<Item> Items = new List<Item>();

    public SceneItemConfiguration(string name, List<Vector2> positions, List<Item> items)
    {
        SceneName = name;
        Positions = positions;
        Items = items;
    }
}
