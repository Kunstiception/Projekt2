using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine PlayerStateMachine => _playerStateMachine;
    public Interaction CurrentInteraction => _currentInteraction;
    public Animator PlayerAnimator;

    [SerializeField] private Transform _feetTransform;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _raycastLayermask;
    [SerializeField] private Transform _playerTransform;
    private bool _isWalkingRight;
    private Vector2 _walkingDirection;
    private Vector2 _actualTargetPoint;
    private Vector3 _scale;
    private Interaction _currentInteraction;
    private Coroutine _movementCoroutine;
    private PlayerStateMachine _playerStateMachine;

    private void Awake()
    {
        _playerStateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        _isWalkingRight = true;
        _scale = _playerTransform.localScale;
        _movementCoroutine = null;

        _playerStateMachine.Initialize(_playerStateMachine.idleState);

        ScaleSprite();
    }

    private void Update()
    {
        _playerStateMachine.Execute();

        if (_playerStateMachine.CurrentState == _playerStateMachine.interactionState)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }

            Vector2 targetPosition;
            _currentInteraction = LookForInteraction();

            if (_currentInteraction == null)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            }
            else
            {
                Debug.Log($"Selected interaction: {_currentInteraction}");
                targetPosition = _currentInteraction.ReturnClosestAnchor(_rigidbody.position);
            }

            FlipSprite(targetPosition);

            _movementCoroutine = StartCoroutine(MoveRigidbody(SetMovement(targetPosition), false));
        }
    }

    private void ScaleSprite()
    {
        // vorläufige Rechnung
        _playerTransform.localScale = _scale * (1 - (transform.position.y - 0.1f) / 10);
    }

    private void FlipSprite(Vector2 cursorPosition)
    {
        if (transform.position.x < cursorPosition.x)
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
        _playerTransform.localScale = _scale;

        _isWalkingRight = !_isWalkingRight;
    }
    
    private Interaction LookForInteraction()
    {
        // https://stackoverflow.com/questions/20583653/raycasting-to-find-mouseclick-on-object-in-unity-2d-games
        RaycastHit2D hit;

        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit && hit.transform.CompareTag("Interaction"))
        {
            return hit.transform.GetComponent<Interaction>();
        }
        else
        {
            return null;
        }
    }

    private IEnumerator LookForObstacles(Vector2 direction)
    {
        RaycastHit2D hitLeft;
        RaycastHit2D hitRight;

        // https://discussions.unity.com/t/multiply-quaternion-by-vector/31759
        hitLeft = Physics2D.Raycast(_feetTransform.position, Quaternion.Euler(0, 0, -45) * direction, 1, _raycastLayermask);
        hitRight = Physics2D.Raycast(_feetTransform.position, Quaternion.Euler(0, 0, 45) * direction, 1, _raycastLayermask);

        Vector2 position = new Vector2(transform.position.x, transform.position.y);

        if (!hitLeft)
        {
            Debug.Log("Left side is free!");

            Debug.Log(position);
            Debug.Log(position + new Vector2(-0.1f, 0));

            yield return _movementCoroutine = StartCoroutine(MoveRigidbody(SetMovement(position + new Vector2(-0.1f, 0)), true));

            _movementCoroutine = StartCoroutine(MoveRigidbody(_actualTargetPoint, false));
        }
        else if (!hitRight)
        {
            Debug.Log("Right side is free!");

            yield return _movementCoroutine = StartCoroutine(MoveRigidbody(position + new Vector2(0.1f, 0), true));

            _movementCoroutine = StartCoroutine(MoveRigidbody(_actualTargetPoint, false));
        }

    }

    // Ensures that target position is at the feet of the character
    private float GetCorrectYPosition(float mousePosition)
    {
        return mousePosition + (transform.position.y -_feetTransform.position.y);
    }

    // Moves transform to the selected position
    private Vector2 SetMovement(Vector2 targetPosition)
    {
        Vector2 startingPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 finalPosition = new Vector2(targetPosition.x, targetPosition.y);
        finalPosition.y = GetCorrectYPosition(finalPosition.y);

        //https://gamedevbeginner.com/direction-vectors-in-unity/
        _walkingDirection = (targetPosition - startingPosition).normalized;

        //Debug.Log(finalPosition);

        return finalPosition;
    }

    // https://discussions.unity.com/t/how-would-i-move-a-rigidbody-towards-another-object/33779/2
    private IEnumerator MoveRigidbody(Vector2 targetPosition, bool isCollision)
    {
        Vector2 currentPosition;
        Vector2 endpoint;

        if (!isCollision)
        {
            _actualTargetPoint = targetPosition;
            endpoint = targetPosition;
        }
        else
        {
            endpoint = targetPosition;
        }

        while (Vector2.Distance(_rigidbody.position, targetPosition) > 0.05f)
        {
            currentPosition = Vector2.MoveTowards(transform.position, endpoint, GameConfig.PlayerSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(currentPosition);

            ScaleSprite();

            yield return null;
        }

        _rigidbody.position = endpoint;

        if(_currentInteraction != null)
        {
            FlipSprite(_currentInteraction.transform.position);
            ScaleSprite();

            if(_currentInteraction is IInteractable)
            {
                _currentInteraction.GetComponent<IInteractable>().StartInteraction();
            }
        }

        _movementCoroutine = null;

        //Debug.Log("Target position reached");
    }

    public bool ReturnWalkingCoroutineState()
    {
        if (_movementCoroutine == null)
        {
            return false;
        }

        return true;
    }

    public bool ReturnItemSelected()
    {
        if (_currentInteraction != null)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Border") && _movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
            _movementCoroutine = null;

            Debug.Log("Hit Border!");
        }

        if(collision.transform.CompareTag("Obstacle") && _movementCoroutine != null)
        {
            // StopCoroutine(_movementCoroutine);
            // _movementCoroutine = null;

            Debug.Log("Hit Obstacle!");

            // StartCoroutine(LookForObstacles(_walkingDirection));
        }
    }
}
