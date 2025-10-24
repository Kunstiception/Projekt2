using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static event Action OnTouched;

    private void OnMouseEnter()
    {
        if(Input.GetMouseButton(0))
        {
            OnTouched?.Invoke();
        }
    }
}
