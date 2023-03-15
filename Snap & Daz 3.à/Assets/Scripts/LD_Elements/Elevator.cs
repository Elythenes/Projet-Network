using UnityEngine;

public class Elevator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Snap") || other.CompareTag("Daz"))
        {
            other.transform.parent = gameObject.transform;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Snap") || other.CompareTag("Daz"))
        {
            other.transform.parent = null;
        }
    }
}
