using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static event Action OnTouched;

    private bool _wasTouched = false;

    private void OnMouseOver()
    {
        if (_wasTouched)
        {
            return;
        }

        //Debug.Log($"{this.gameObject} entered");
        
        if(Input.GetMouseButton(0))
        {
            Debug.Log($"{this.gameObject} successful");
            OnTouched?.Invoke();
            _wasTouched = true;
        }
    }
}
