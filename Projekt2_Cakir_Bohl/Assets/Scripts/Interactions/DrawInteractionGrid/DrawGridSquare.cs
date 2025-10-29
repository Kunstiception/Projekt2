using System;
using UnityEngine;

public class DrawGridSquare : MonoBehaviour
{
    public string SquareName;

    public static event Action<string> OnTouched;

    private void OnMouseEnter()
    {
        if(Input.GetMouseButton(0))
        {
            OnTouched?.Invoke(SquareName);
        }
    }
}
