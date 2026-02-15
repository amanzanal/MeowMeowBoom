using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private Health health;
    [SerializeField] private Tilemap river;
    private Vector2 dir;
    private Rigidbody2D rb2D;
    private float moveX;
    private float moveY;
    private float decreaseSpeed = 2f;
    private Animator animator;
    private GameObject activeShield;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);

        if (moveX != 0 || moveY != 0)
        {
            animator.SetFloat("LastX", moveX);
            animator.SetFloat("LastY", moveY);
        }

        dir = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + dir * speed * Time.fixedDeltaTime);
    }

    public void grow()
    {
        transform.localScale += new Vector3(0.5f, 0.5f, 0);
        updateShieldSize();
    }

    public void decrease()
    {
        Vector3 newScale = transform.localScale - new Vector3(1f, 1f, 0);

        if (transform.localScale.x > 0.5f && transform.localScale.y > 0.5f &&
            newScale.x >= 1f && newScale.y >= 1f)
        {
            transform.localScale = newScale;
            updateShieldSize();
        }
    }

    private void updateShieldSize()
    {
        if (activeShield != null)
        {
            activeShield.transform.localScale = transform.localScale;
        }
    }

    public void activateShield()
    {
        if (activeShield == null)
        {
            activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            activeShield.transform.SetParent(transform);
            activeShield.transform.localScale = transform.localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("River"))
        {
            Vector2 knockbackDirection = -dir.normalized;

            // Calcula la nueva posición
            Vector2 newPosition = rb2D.position + knockbackDirection * 10f;

            // Verifica y ajusta los límites
            newPosition.x = Mathf.Clamp(newPosition.x, 1020f, 1050f);
            newPosition.y = Mathf.Clamp(newPosition.y, 504f, 540f);

            // Asigna la nueva posición
            rb2D.position = newPosition;

            StartCoroutine(speedEffect());
            health.takeDamage(20);
        }
        else if (collision.CompareTag("Bullet"))
        {
            health.takeDamage(10);
        }
        else if (collision.CompareTag("Enemy"))
        {
            health.takeDamage(20);
        }
    }


    private IEnumerator speedEffect()
    {
        speed -= decreaseSpeed;
        yield return new WaitForSeconds(6f);
        speed += decreaseSpeed;
    }
}
