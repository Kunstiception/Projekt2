using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _feetTransform;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _raycastLayermask;
    private bool _isWalkingRight;
    private Vector3 _scale;
    private SpriteRenderer _renderer;
    private Coroutine _movementCoroutine;

    void Start()
    {
        _isWalkingRight = true;
        _renderer = GetComponent<SpriteRenderer>();
        _scale = transform.localScale;
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

            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            FlipSprite(cursorPosition);

            _movementCoroutine = StartCoroutine(MoveTransform(cursorPosition));
        }
    }

    private void ScaleSprite()
    {
        // vorläufige Rechnung
        transform.localScale = _scale * (1 - (transform.position.y - 0.1f) / 10);
    }

    private void FlipSprite(Vector2 cursorPosition)
    {
        if (_transform.position.x < cursorPosition.x)
        {
            if (_isWalkingRight)
            {
                return;
            }
        }
        else
        {
            if (!_isWalkingRight)
            {
                return;
            }
        } 
        
        // https://stackoverflow.com/questions/26568542/flipping-a-2d-sprite-animation-in-unity-2d
        _scale.x *= -1;
        transform.localScale = _scale;

        _isWalkingRight = !_isWalkingRight;      
    }

    private float LookForBorders(Vector2 endPoint)
    {
        RaycastHit2D hit;
        hit = Physics2D.Linecast(_feetTransform.position, endPoint, _raycastLayermask);

        if (hit)
        {
            Debug.Log(hit.point);

            return hit.point.y;
        }

        return endPoint.y;
    }

    // Ensures that target position is at the feet of the character
    private float GetCorrectYPosition(float mousePosition)
    {
        return mousePosition + (transform.position.y -_feetTransform.position.y);
    }

    // Moves transform to the selected position
    private IEnumerator MoveTransform(Vector2 targetPosition)
    {
        float timer = 0;

        Vector2 initialPosition = _transform.position;
        Vector2 finalPosition = new Vector2 (targetPosition.x, LookForBorders(targetPosition));
        finalPosition.y = GetCorrectYPosition(finalPosition.y);

        Debug.Log(finalPosition);

        float movementLength = Vector2.Distance(initialPosition, finalPosition);
        float movementDuration = movementLength / GameConfig.PlayerSpeed;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;

            _rigidbody.MovePosition(Vector2.Lerp(initialPosition, finalPosition,
                timer / movementDuration));

            ScaleSprite();

            yield return null;
        }

        _movementCoroutine = null;

        Debug.Log("Target position reached");
    }
}
