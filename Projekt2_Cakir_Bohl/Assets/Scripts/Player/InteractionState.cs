using UnityEngine;

public class InteractionState: IState
{
    private PlayerController _playerController;
    private float _timer = 0;

    public InteractionState(PlayerController playerController)
    {
        this._playerController = playerController;
    }

    public void Enter()
    {
        Debug.Log("Entering Interaction State!");
    }

    public void Execute()
    {
        _timer += Time.deltaTime;
        
        if(_timer > 2f)
        {
            _playerController.PlayerStateMachine.TransitionTo(_playerController.PlayerStateMachine.idleState);         
        }
        
    }
    
    public void Exit()
    {
        _timer = 0;
    }
}
