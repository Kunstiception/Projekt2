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
        Debug.Log("Entering Walk State!");
    }

    public void Execute()
    {
        if(!_playerController.ReturnWalkingCoroutineState())
        {
            if (_playerController.ReturnItemSelected())
            {
                _playerController.PlayerStateMachine.TransitionTo(_playerController.PlayerStateMachine.interactionState);

                return;
            }
            
            _playerController.PlayerStateMachine.TransitionTo(_playerController.PlayerStateMachine.idleState);
        }
    }
    
    public void Exit()
    {
        
    }
}
