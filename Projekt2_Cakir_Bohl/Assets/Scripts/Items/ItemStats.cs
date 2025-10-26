using UnityEngine;

[CreateAssetMenu(fileName = "ItemStats", menuName = "Scriptable Objects/ItemStats")]
public class ItemStats : ScriptableObject
{
    public string Name;
    public Sprite ItemVisual;
    public int RitualStat1;
    public int RitualStat2;
    public int RitualStat3;
    public int RitualStat4;
}
