using System;
using UnityEngine;

public class DrawGridSquare : Box
{
    public string SquareName;

    public static new event Action<string> OnTouched;

    private void OnMouseOver()
    {
        if (_wasTouched)
        {
            return;
        }

        //Debug.Log($"Entered {this.gameObject}");
        if (Input.GetMouseButton(0))
        {
            Debug.Log($"Correctly entered: {this.gameObject}");
            OnTouched?.Invoke(SquareName);
            _wasTouched = true;
        }
    }
}
