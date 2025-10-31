using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static event Action OnTouched;

    public bool _wasTouched = false;

    protected void OnEnable()
    {
        DrawGridManager.Reset += ResetBox;
    }

    protected void OnDisable()
    {
        DrawGridManager.Reset -= ResetBox;
    }

    private void OnMouseOver()
    {
        if (_wasTouched)
        {
            return;
        }

        //Debug.Log($"{this.gameObject} entered");

        if (Input.GetMouseButton(0))
        {
            Debug.Log($"{this.gameObject} successful");
            OnTouched?.Invoke();
            _wasTouched = true;
        }
    }
    
    protected void ResetBox()
    {
        //Debug.LogError($"reset {this.gameObject}");
        _wasTouched = false;
    }
}
