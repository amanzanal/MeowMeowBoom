using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Shield : MonoBehaviour
{
    public Transform cat;
    public float rotationSpeed = 50f;
    private void Start()
    {
        if (transform.parent != null)
        {
            cat = transform.parent;
        }

        shieldSize();

        StartCoroutine(wait());
    }
    private void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        shieldSize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    public void shieldSize()
    {
        Vector3 catSize = cat.localScale;
        transform.localScale = catSize + new Vector3(1f, 1f, 0f);
    }
}
