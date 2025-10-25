using System;
using System.Collections;
using UnityEngine;

public class MouseDraw : MonoBehaviour
{
    // https://www.youtube.com/watch?v=bcPNyY84qAY

    [SerializeField] private GameObject _line;
    private Coroutine _drawingCoroutine;

    public static event Action OnMouseUp;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishDrawing();

            OnMouseUp?.Invoke();
        }
    }

    private IEnumerator DrawLine()
    {
        GameObject line = Instantiate(_line, new Vector3(0, 0, 0), Quaternion.identity);
        LineRenderer renderer = line.GetComponent<LineRenderer>();
        renderer.positionCount = 0;

        while(true)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            renderer.positionCount++;
            renderer.SetPosition(renderer.positionCount-1, position);
            yield return null;
        }
    }

    private void StartDrawing()
    {
        if (_drawingCoroutine != null)
        {
            StopCoroutine(_drawingCoroutine);
            _drawingCoroutine = null;
        }

        _drawingCoroutine = StartCoroutine(DrawLine());
    }
    
    private void FinishDrawing()
    {
        StopCoroutine(_drawingCoroutine);
        _drawingCoroutine = null;
    }

}
