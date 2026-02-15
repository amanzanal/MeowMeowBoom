using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Food : MonoBehaviour
{
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    [SerializeField] protected Tilemap river;

    private Vector2 randomPos;

    public void moveFoodPosition()
    {
        bool posValid = false;

        while (!posValid)
        {
            randomPos = new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y)
            );

            Vector3Int cellPosition = river.WorldToCell(randomPos);
            TileBase tileAtPosition = river.GetTile(cellPosition);

            if (tileAtPosition == null)
            {
                posValid = true;
            }
        }
        transform.position = randomPos;
    }
}
