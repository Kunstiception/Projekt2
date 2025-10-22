using UnityEngine;

public class IdleState : IState
{
    private PlayerController _playerController;

    public IdleState(PlayerController playerController)
    {
        this._playerController = playerController;
    }

    public void Enter()
    {
        Debug.Log("Idle Animation!");
    }

    public void Execute()
    {
        if(!_playerController.ReturnWalkingCoroutineState())
        {
            _playerController.PlayerStateMachine.TransitionTo(_playerController.PlayerStateMachine.walkState);
        }
    }
    
    public void Exit()
    {
        
    }
}
