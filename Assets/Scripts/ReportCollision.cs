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
            pl.startPoint = gameObject;
        }
    }
}
