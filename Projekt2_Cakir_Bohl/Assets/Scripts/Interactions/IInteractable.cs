using System;

public interface IInteractable
{
    // https://stackoverflow.com/questions/9772005/how-do-you-add-an-actionstring-to-an-interface
    // Can't contain fields, only properties
    //public static event Action OnInteractionFinished { get; set; }

    void StartInteraction();

    void CloseInteraction();
}
