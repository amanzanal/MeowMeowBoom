using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatFood : Food
{
    [SerializeField] private Points textPoints;
    [SerializeField] private Health health;
    private int points = 10;
    private int amountHealth = 10;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        CatController cat = collision.GetComponent<CatController>();       

        if (collision.CompareTag("Cat"))
        {
            health.updateHealth(amountHealth);
            textPoints.updatePointsText(points);

            cat.grow();

            moveFoodPosition();
        }
    }
}
