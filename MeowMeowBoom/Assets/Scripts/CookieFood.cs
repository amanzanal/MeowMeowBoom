using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieFood : Food
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            CatController cat = collision.GetComponent<CatController>();
            if (cat != null)
            {
                cat.activateShield();
                moveFoodPosition();
            }
        }
    }
}
