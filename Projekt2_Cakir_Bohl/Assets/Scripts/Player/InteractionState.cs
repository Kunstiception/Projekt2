using UnityEngine;

public class InteractionState: IState
{
    private PlayerController _playerController;

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
        if(_playerController.CurrentInteraction.HasFinished)
        {
            _playerController.PlayerStateMachine.TransitionTo(_playerController.PlayerStateMachine.idleState);         
        }    
    }
    
    public void Exit()
    {

    }
}
