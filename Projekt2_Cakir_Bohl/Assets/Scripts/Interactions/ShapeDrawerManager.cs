using System;
using UnityEngine;

public enum InteractionType
{
    Once = 1,
    Repeated = 2
}

public class ShapeDrawerManager : Interaction
{
    public InteractionType InteractionType;

    //[SerializeField] private GameObject[] _possibleShapes;
    [SerializeField] GameObject _shape;
    private int _counter;
    private int _correctCounter;
    private GameObject _selectedShape;
    private bool _wasCorrect;

    public static event Action<bool> SendResult;
    public static event Action SendCompleted;

    private void Start()
    {
        _correctCounter = _shape.GetComponent<Shape>().CorrectCounter;
        Debug.Log(_correctCounter);
    }

    private void OnEnable()
    {
        if (InteractionType == InteractionType.Once)
        {
            MouseDraw.OnMouseUp += CheckResult;
        }

        Box.OnTouched += IncrementCounter;
    }

    private void OnDisable()
    {
        if (InteractionType == InteractionType.Once)
        {
            MouseDraw.OnMouseUp += CheckResult;
        }
        
        Box.OnTouched -= IncrementCounter;
    }

    // private void LoadShape()
    // {
    //     _selectedShape = Instantiate(_possibleShapes[UnityEngine.Random.Range(0, _possibleShapes.Length)]);

    //     _correctCounter = _selectedShape.GetComponent<Shape>().CorrectCounter;
    // }

    public void CheckResult()
    {
        if (_counter / _correctCounter >= 0.8f)
        {
            Debug.Log($"Correct! {_counter / (float)_correctCounter}");
            _wasCorrect = true;
        }
        else
        {
            Debug.Log("Do it again!");
            _wasCorrect = false;
        }

        RaiseReset();

        CloseInteraction();
    }

    private void IncrementCounter()
    {
        _counter++;
        Debug.Log(_counter);

        if(InteractionType == InteractionType.Repeated && _counter == _correctCounter -1)
        {
            SendCompleted?.Invoke();

            _counter = 0;
        }
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
