using UnityEngine;

public class ShapDrawerManager : Interaction, IInteractable
{
    [SerializeField] private Shape[] _possibleShapes;
    private int _counter;
    private int _correctCounter;
    private Shape _selectedShape;

    private void Start()
    {
        foreach (Shape shape in _possibleShapes)
        {
            shape.gameObject.SetActive(false);
        }
    }

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

    private void LoadShape()
    {
        _selectedShape = _possibleShapes[UnityEngine.Random.Range(0, _possibleShapes.Length)];
        _selectedShape.gameObject.SetActive(true);

        _correctCounter = _selectedShape.CorrectCounter;
    }

    public void CheckResult()
    {
        if ((float)_counter / (float)_correctCounter >= 0.8f)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Do it again!");
        }

        _counter = 0;

        _selectedShape.gameObject.SetActive(false);
        _selectedShape = null;

        _hasFinished = true;
    }
    
    private void IncrementCounter()
    {
        _counter++;
        Debug.Log(_counter);
    }

    public void StartInteraction()
    {
        Debug.Log("Started interaction!");
        LoadShape();
    }
}
