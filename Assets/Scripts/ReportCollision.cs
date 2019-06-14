using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportCollision : MonoBehaviour
{
    public PhicLine pl;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.ToString().Substring(0, 2) == "ob")
        {
            //pl.startPoint = gameObject;
        }
    }

    void FixedUpdate()
    {
        Vector3 l = transform.TransformDirection(Vector3.left);
        Vector3 r = transform.TransformDirection(Vector3.right);

        Debug.DrawRay(transform.position, l, Color.green);
        Debug.DrawRay(transform.position, r, Color.green);

        RaycastHit hit;
        RaycastHit hit2;

        if (Physics.Raycast(transform.position, l,out hit, 1))
        {
            
            Debug.Log("There is something in left of the object!"+hit.collider.name);
        }
            
        if (Physics.Raycast(transform.position, r,out hit2, 1))
        {
            Debug.Log("There is something in right of the object!"+hit2.collider.name);
        }
    }

}
