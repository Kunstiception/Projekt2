using System;
using UnityEngine;

public class DrawGridSquare : MonoBehaviour
{
    public string SquareName;

    public static event Action<string> OnTouched;

    private bool _wasTouched = false;

    private void OnEnable()
    {
        DrawGridManager.Reset += Reset;
    }

    private void OnDisable()
    {
        DrawGridManager.Reset -= Reset;
    }

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
    
    private void Reset()
    {
        _wasTouched = false;
    }
}
