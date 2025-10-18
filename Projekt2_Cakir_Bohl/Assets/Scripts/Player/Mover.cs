using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    private Coroutine _movementCoroutine;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Objektlayer checken
            if (_movementCoroutine != null)
            {
                _movementCoroutine = null;
            }

            Vector2 cursorPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            _movementCoroutine = StartCoroutine(MoveTransform(Camera.main.ScreenToWorldPoint(cursorPosition)));
        }
    }

    // Ensures that target position is at the feet of the character
    private float GetCorrectYPosition(float mousePosition)
    {
        return mousePosition + _transform.GetComponent<SpriteRenderer>().size.y / 2;
    }
    
    // Moves transform to the selected position
    private IEnumerator MoveTransform(Vector2 targetPosition)
    {
        Debug.Log(targetPosition);

        float timer = 0;
        Vector2 initialPosition = _transform.position;
        Vector2 finalPosition = new Vector2(targetPosition.x, GetCorrectYPosition(targetPosition.y));

        Debug.Log(GetCorrectYPosition(targetPosition.y));

        float movementLength = Vector2.Distance(initialPosition, finalPosition);
        float movementDuration = movementLength / GameConfig.PlayerSpeed;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            _transform.position = Vector2.Lerp(initialPosition, finalPosition,
                timer/movementDuration);
            yield return null;
        }

        _transform.position = finalPosition;

        Debug.Log("Target position reached");
    }
}
