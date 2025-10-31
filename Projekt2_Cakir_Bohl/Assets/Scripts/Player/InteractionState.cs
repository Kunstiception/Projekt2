using UnityEditor.Animations;
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
        _playerController.PlayerAnimator.SetTrigger("TriggerInteract");
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
        _playerController.PlayerAnimator.SetTrigger("TriggerIdle");
    }
}
