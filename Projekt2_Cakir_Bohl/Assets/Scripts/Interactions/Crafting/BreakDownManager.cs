using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BreakDownManager : Interaction, IInteractable
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _shape;
    private bool _isRunning = true;
    private int _counter;

    private void Start()
    {
        _shape.SetActive(false);
    }

    private void OnEnable()
    {
        ShapeDrawerManager.SendResult += RestartClick;
    }

    private void OnDisable()
    {
        ShapeDrawerManager.SendResult -= RestartClick;
    }

    private void FixedUpdate()
    {
        if(_isRunning)
        {
            _slider.value -= GameConfig.BreakDownAutoStep;         
        }
    }

    private void OnMouseDown()
    {
        if (_isRunning)
        {
            _slider.value += GameConfig.BreakDownIncrement;
        }

        if (_slider.value >= 1f)
        {
            _isRunning = false;
            Debug.LogError("Won!");

            StartCoroutine(HandOverToShape());
        }
    }

    private IEnumerator HandOverToShape()
    {
        yield return new WaitForSeconds(1);

        _shape.SetActive(true);
    }

    private void RestartClick(bool wasCorrect)
    {
        _shape.SetActive(false);

        if (wasCorrect)
        {
            _counter++;
            Debug.Log(_counter);

            if(_counter == GameConfig.BreakDownMaxCounter)
            {
                Debug.LogError("Done!!!");

                return;
            }
        }

        _isRunning = true;
    }

    public void StartInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void CloseInteraction()
    {
        _hasFinished = true;
    }
}
