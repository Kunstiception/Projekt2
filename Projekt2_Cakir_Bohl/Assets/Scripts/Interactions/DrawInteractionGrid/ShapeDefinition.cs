using UnityEngine;

[CreateAssetMenu(fileName = "ShapeDefinition", menuName = "Scriptable Objects/ShapeDefinition")]
public class ShapeDefinition : ScriptableObject
{
    public string[] CorrectSquares;
    public string[] FalseSquares;
}
