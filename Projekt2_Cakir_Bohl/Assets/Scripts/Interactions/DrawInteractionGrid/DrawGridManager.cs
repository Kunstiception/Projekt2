using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawGridManager : Interaction, IInteractable
{
    [SerializeField] private ShapeDefinition[] _shapeDefinitions;
    [SerializeField] private GameObject _grid;

    private List<ShapeDefinition> _selectedShapeDefinitions = new List<ShapeDefinition>();
    private List<string> _currentSquares = new List<string>();
    private ShapeDefinition _currentShapeDefinion;
    private Coroutine _drawingCoroutine;
    private bool _isDrawing;

    private void OnEnable()
    {
        MouseDraw.OnMouseUp += CheckResult;
        DrawGridSquare.OnTouched += HandleSquareOutput;
    }

    private void OnDisable()
    {
        MouseDraw.OnMouseUp -= CheckResult;
        DrawGridSquare.OnTouched -= HandleSquareOutput;
    }

    private void Start()
    {
        _grid.SetActive(false);
    }

    private void ChoseDefinitions()
    {
        int randomNumber = UnityEngine.Random.Range(2, _shapeDefinitions.Length);

        List<ShapeDefinition> tempDefinitions = _shapeDefinitions.ToList();

        for (int i = 0; i < randomNumber; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, tempDefinitions.Count);

            _selectedShapeDefinitions.Add(tempDefinitions[randomIndex]);

            Debug.Log(tempDefinitions[randomIndex].name);

            tempDefinitions.RemoveAt(randomIndex);
        }

        _drawingCoroutine = StartCoroutine(DrawShapes());
    }
    
    private IEnumerator DrawShapes()
    {
        for (int i = 0; i < _selectedShapeDefinitions.Count; i++)
        {
            _currentShapeDefinion = _selectedShapeDefinitions[i];

            _isDrawing = true;

            yield return new WaitUntil(() => _isDrawing == false);

            RaiseReset();
        }
    }

    private void HandleSquareOutput(string output)
    {
        if (!_currentSquares.Contains(output))
        {
            _currentSquares.Add(output);

            Debug.Log(output);
        }    
    }
    
    private void CheckResult()
    {
        int correctCounter = 0;

        foreach (string squareName in _currentSquares)
        {
            if (_currentShapeDefinion.FalseSquares.Contains(squareName))
            {
                Debug.LogError($"Wrong!!! {squareName}");

                CloseInteraction();

                return;
            }

            if (_currentShapeDefinion.CorrectSquares.Contains(squareName))
            {
                correctCounter++;
            }
        }

        if ((float)correctCounter == _currentShapeDefinion.CorrectSquares.Length)
        {
            Debug.LogError("Correct!");
        }
        else
        {
            Debug.LogError($"Wrong number: {correctCounter}");
        }

        if(_currentShapeDefinion == _selectedShapeDefinitions.Last())
        {
            CloseInteraction();
        }

        _isDrawing = false;
        _currentSquares.Clear();

        correctCounter = 0;
    }

    public void StartInteraction()
    {
        _hasFinished = false;
        _grid.SetActive(true);

        ChoseDefinitions();
    }

    public void CloseInteraction()
    {
        _currentSquares.Clear();
        _selectedShapeDefinitions.Clear();

        _hasFinished = true;

        _grid.SetActive(false);
    }
}
