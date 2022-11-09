using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPro : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform body;
    public AnimPro otherFoot = default;
    public float speed = 1;
    public float stepDistance = 4;
    public float stepLenght = 4;
    public float stepHeight = 1;
    public Vector3 footOffset = default;
    public float footSpacing;
    public Vector3 oldPosition, currentPosition, newPosition;
    public Vector3 oldNormal, currentNormal, newNormal;
    public float lerp;

    void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPosition = newPosition = oldPosition = transform.position;
        lerp = 1;
    }
    void Update()
    {
        transform.position = currentPosition;
        transform.up = currentNormal;
        
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        Debug.DrawRay(transform.position, ray.direction, Color.yellow);
        if (Physics.Raycast(ray, out RaycastHit info, Mathf.Infinity, groundLayer))
        {
            Debug.Log(info.point);
            if (Vector3.Distance(newPosition, info.point) > stepDistance /*&& !otherFoot.IsMoving()*/ /*&& lerp >= 1*/)
            {
                lerp = 0;
                int direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;  // Doit on bouger vers l'avant ou vers l'arrière ?
                newPosition = info.point + (body.forward * stepLenght * direction) + footOffset;
                newNormal = info.normal;
            }
        }


        if (lerp < 1)
        {
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight; // pour lever le pied
            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition,0.1f);
    }
    public bool IsMoving()
    {
        return lerp < 1;
    }
}
