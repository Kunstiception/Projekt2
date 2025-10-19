using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _feetTransform;
    private Coroutine _movementCoroutine;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }

            Vector2 cursorPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            LookForObstacles(cursorPosition);

            _movementCoroutine = StartCoroutine(MoveTransform(Camera.main.ScreenToWorldPoint(cursorPosition)));
        }
    }

    private void LookForObstacles(Vector2 cursorPosition)
    {
        Vector2 startingPosition = new Vector2(_feetTransform.position.x, _feetTransform.position.y);
        float distance = Vector2.Distance(startingPosition, cursorPosition);
        //https://gamedevbeginner.com/direction-vectors-in-unity/
        Vector2 direction = (startingPosition - cursorPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(_feetTransform.position, direction, distance);

        if(hit)
        {
            Debug.Log(hit.collider.name);
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
        //Debug.Log(targetPosition);

        float timer = 0;
        Vector2 initialPosition = _transform.position;
        Vector2 finalPosition = new Vector2(targetPosition.x, GetCorrectYPosition(targetPosition.y));

        //Debug.Log(GetCorrectYPosition(targetPosition.y));

        float movementLength = Vector2.Distance(initialPosition, finalPosition);
        float movementDuration = movementLength / GameConfig.PlayerSpeed;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            _rigidbody.MovePosition(Vector2.Lerp(initialPosition, finalPosition,
                timer/movementDuration));
            yield return null;
        }

        _transform.position = finalPosition;

        _movementCoroutine = null;

        //Debug.Log("Target position reached");
    }
}
