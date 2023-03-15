public class Crate : Interactible
{
    public override void Interact()
    {
        base.Interact();
        gameObject.transform.parent = actor.transform;
    }

    public override void StopInteract()
    {
        base.StopInteract();
        gameObject.transform.parent = null;
    }
}
