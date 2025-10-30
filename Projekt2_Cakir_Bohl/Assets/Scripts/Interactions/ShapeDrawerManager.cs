using System;
using UnityEngine;

public class ShapeDrawerManager : Interaction
{
    //[SerializeField] private GameObject[] _possibleShapes;
    private int _counter;
    private int _correctCounter;
    private GameObject _selectedShape;
    private bool _wasCorrect;

    public static event Action<bool> SendResult;

    private void OnEnable()
    {
        MouseDraw.OnMouseUp += CheckResult;
        Box.OnTouched += IncrementCounter;
    }

    private void OnDisable()
    {
        MouseDraw.OnMouseUp -= CheckResult;
        Box.OnTouched -= IncrementCounter;
    }

    // private void LoadShape()
    // {
    //     _selectedShape = Instantiate(_possibleShapes[UnityEngine.Random.Range(0, _possibleShapes.Length)]);

    //     _correctCounter = _selectedShape.GetComponent<Shape>().CorrectCounter;
    // }

    public void CheckResult()
    {
        if ((float)_counter / (float)_correctCounter >= 0.8f)
        {
            Debug.Log("Correct!");
            _wasCorrect = true;
        }
        else
        {
            Debug.Log("Do it again!");
            _wasCorrect = false;
        }

        CloseInteraction();
    }
    
    private void IncrementCounter()
    {
        _counter++;
        Debug.Log(_counter);
    }

    // public void StartInteraction()
    // {
    //     Debug.Log("Started interaction!");
    //     LoadShape();

    //     ResetHasFinished();
    // }

    public void CloseInteraction()
    {
        _counter = 0;

        Destroy(_selectedShape);
        _selectedShape = null;

        //_hasFinished = true;

        SendResult?.Invoke(_wasCorrect);
    }
}
