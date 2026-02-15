using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Health health;
    private float speed = 10f;
    public Vector2 dir;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime(3f));
    }

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            Destroy(gameObject);

        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
