using UnityEngine;

public class WalkState : IState
{
    private PlayerController _playerController;

    public WalkState(PlayerController playerController)
    {
        this._playerController = playerController;
    }

    public void Enter()
    {
        Debug.Log("Moving animation!");
    }

    public void Execute()
    {
        if(_playerController.ReturnWalkingCoroutineState())
        {
            _playerController.PlayerStateMachine.TransitionTo(_playerController.PlayerStateMachine.idleState);
        }
    }
    
    public void Exit()
    {
        
    }
}
