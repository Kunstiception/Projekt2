using UnityEngine;
using UnityEngine.UI;

public class CookingManager : Interaction, IInteractable
{
    [SerializeField] private GameObject _shape;
    [SerializeField] private Slider _slider;
    private int _counter;
    private bool _isRunning = true;

    private void OnEnable()
    {
        ShapeDrawerManager.SendCompleted += IncrementCounter;
    }

    private void OnDisable()
    {
        ShapeDrawerManager.SendCompleted -= IncrementCounter;
    }

    private void FixedUpdate()
    {
        if(_isRunning)
        {
            _slider.value += GameConfig.CookingAutoStep;  

            if(_slider.value >= 1)
            {
                Debug.LogError("Failed!!!");

                CloseInteraction();
            }       
        }
    }

    private void IncrementCounter()
    {
        _counter++;

        Debug.LogError(_counter);

        if (_counter == GameConfig.CookingMaxCounter)
        {
            CloseInteraction();
        }

        _slider.value -= GameConfig.CookingIncrement;
    }

    public void StartInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void CloseInteraction()
    {
        _shape.SetActive(false);
        _hasFinished = true;
        _isRunning = false;
    }
}
