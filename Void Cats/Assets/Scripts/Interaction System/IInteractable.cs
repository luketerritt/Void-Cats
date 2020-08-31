
// Anything that is Interactable will have theses variables 
public interface IInteractable
{
    // The Max Range of an Interactable Object
    float MaxRange { get; }

    // Decide what happens when theses are called
    void OnStartHover();
    void OnInteract();
    void OnEndHover();
}