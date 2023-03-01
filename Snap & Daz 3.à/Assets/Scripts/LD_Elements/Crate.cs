public class Crate : Interactible
{
    public override void Interact()
    {
        gameObject.transform.parent = actor.transform;
    }

    public override void StopInteract()
    {
        gameObject.transform.parent = null;
    }
}
