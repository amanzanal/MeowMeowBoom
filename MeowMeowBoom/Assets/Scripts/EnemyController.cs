using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private CatController cat;
    [SerializeField] private Vector2 spawnMin;
    [SerializeField] private Vector2 spawnMax;
    [SerializeField] private GameObject bulletX;
    [SerializeField] private GameObject bulletY;
    public Transform catImage;
    private Animator animator;
    private bool stopEnemy = true;
    private float shootCooldown = 1.5f;
    private float lastShotTime;
    private static float globalSpeedMultiplier;
    private float currentSpeed;
    private bool isChasingCat = true;

    void Start()
    {
        globalSpeedMultiplier = 0.5f;
        currentSpeed = baseSpeed;
        animator = GetComponent<Animator>();
        StartCoroutine(appearDisappearRandomly());
        StartCoroutine(adjustDifficultyOverTime());
    }

    void Update()
    {
        if (stopEnemy && GetComponent<Renderer>().enabled)
        {
            if (isChasingCat)
            {
                MoveTowardsCat();
            }

            if (Time.time >= lastShotTime + shootCooldown)
            {
                shoot();
                lastShotTime = Time.time;
            }
        }
    }

    private void MoveTowardsCat()
    {
        Vector2 randomOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * 0.5f;
        Vector2 targetPosition = (Vector2)catImage.position + randomOffset;
        Vector2 newPos = Vector2.MoveTowards(transform.position, targetPosition, currentSpeed * globalSpeedMultiplier * Time.deltaTime);
        Vector2 dir = (newPos - (Vector2)transform.position).normalized;

        transform.position = newPos;

        if (dir.magnitude > 0.01f)
        {
            animator.SetFloat("MoveX", dir.x);
            animator.SetFloat("MoveY", dir.y);
            animator.SetFloat("LastX", dir.x);
            animator.SetFloat("LastY", dir.y);
        }
        else
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
        }
    }

    private IEnumerator adjustDifficultyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(12f);

            float randomChange = Random.Range(0, 2) == 0 ? -1f : 1f; //Cambio velocidad [-1,1]

            globalSpeedMultiplier += randomChange;

            float totalSpeed = baseSpeed * globalSpeedMultiplier; //Velocidad entre [6,22]

            if (totalSpeed < 6f)
            {
                globalSpeedMultiplier = 6f / baseSpeed;
            }
            else if (totalSpeed > 22f)
            {
                globalSpeedMultiplier = 22f / baseSpeed;

                shootCooldown = Mathf.Clamp(shootCooldown + Random.Range(-0.2f, 0.2f), 0.8f, 3f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            StartCoroutine(collisionCat(collision));
        }
        else if (collision.CompareTag("Shield"))
        {
            StartCoroutine(waitShieldCollision());
        }
    }

    private IEnumerator collisionCat(Collider2D collision)
    {
        cat.decrease();

        respawnRandomPos();
        stopEnemy = false;
        yield return new WaitForSeconds(2f);
        stopEnemy = true;
    }

    private IEnumerator waitShieldCollision()
    {
        Rigidbody2D enemyRb = GetComponent<Rigidbody2D>();

        if (enemyRb != null)
        {
            Vector2 originalVelocity = enemyRb.velocity;
            enemyRb.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            enemyRb.velocity = originalVelocity;
        }
    }

    private IEnumerator appearDisappearRandomly()
    {
        while (true)
        {
            respawnRandomPos();
            GetComponent<Renderer>().enabled = true;
            isChasingCat = true;
            yield return new WaitForSeconds(Random.Range(7f, 15f)); // Aparición entre 7 y 15 segundos

            isChasingCat = false;
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(Random.Range(3f, 6f)); // Desaparición entre 2 a 4 segundos
        }
    }

    private void respawnRandomPos()
    {
        float randomX = Random.Range(spawnMin.x, spawnMax.x);
        float randomY = Random.Range(spawnMin.y, spawnMax.y);
        transform.position = new Vector2(randomX, randomY);
    }

    public void shoot()
    {
        float moveX = animator.GetFloat("MoveX");
        float moveY = animator.GetFloat("MoveY");

        Vector2 bulletDirection;
        GameObject bulletPrefab;

        if (Mathf.Abs(moveY) > Mathf.Abs(moveX))
        {
            bulletDirection = (moveY > 0) ? Vector2.up : Vector2.down;
            bulletPrefab = bulletY;
        }
        else
        {
            bulletDirection = (moveX > 0) ? Vector2.right : Vector2.left;
            bulletPrefab = bulletX;
        }

        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        bullet.dir = bulletDirection;
    }
}
