using System.Collections;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 velocity;

    float speed = 3.5f;
    float fallSpeed = 0.8f;
    float rotationSpeed = 4f;

    bool isOnLeaf = false;
    public int lives = 2;
    public bool isDead = false;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Collider2D[] colliders;
    [SerializeField] Sprite originalSprite;
    [SerializeField] Sprite deathSprite;
    
    SceneController sceneController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
        sceneController = FindObjectOfType<SceneController>();
    }

    void FixedUpdate()
    {
        if (lives >= 0 && !isDead)
        {
            Move();
        }
    }

    void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");


        if (isOnLeaf || vertical > 0)
        {
            velocity = new Vector3(horizontal, vertical, 0f);
            animator.SetBool("isMoving", vertical > 0);
        }
        else
        {
            velocity = new Vector3(horizontal, -fallSpeed, 0f);
            animator.SetBool("isMoving", false);
        }

        transform.position += velocity * speed * Time.deltaTime;


        Quaternion targetRotation;

        if (horizontal > 0) //sað
        {
            targetRotation = Quaternion.Euler(0, 0, -15);
        }
        else if (horizontal < 0) //sol
        {
            targetRotation = Quaternion.Euler(0, 0, 20);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Leaf"))
        {
            isOnLeaf = true;
        }
        else if (collision.CompareTag("Thorn"))
        {
            TakeDamage();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Leaf"))
        {
            isOnLeaf = false;
        }
    }

    void TakeDamage()
    {
        lives--;
        if (lives >= 0)
        {
            StartCoroutine(DeathAnimation());
        }
        else
        {
            lives = 0;
            sceneController.GameOver();
        }
    }

    IEnumerator DeathAnimation()
    {
        isDead = true;
        SetCollidersActive(false);
        animator.enabled = false;
        spriteRenderer.sprite = deathSprite;
        yield return StartCoroutine(MoveToNearestLeaf());
        animator.enabled = true;
        SetCollidersActive(true);
        isDead = false;
    }

    void SetCollidersActive(bool isActive)
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isActive;
        }
    }

    IEnumerator MoveToNearestLeaf()
    {
        GameObject nearestLeaf = FindNearestLeaf();
        if (nearestLeaf != null)
        {
            Collider2D leafCollider = nearestLeaf.GetComponent<Collider2D>();

            //yukarý doðru collider'ýn yüksekliðinin yarýsý kadar bir mesafe eklenir (yapraðýn üstünü hesaplamak için)
            Vector2 leafTop = (Vector2)nearestLeaf.transform.position + (Vector2.up * (leafCollider.bounds.extents.y));
            Vector2 targetPosition = leafTop + Vector2.up * 0.7f;

            float distance = Vector2.Distance(transform.position, targetPosition);
            float duration = distance / speed;
            float elapsedTime = 0f;
            Vector2 startPosition = transform.position;

            while (elapsedTime < duration) //arý hedefe ulaþana kadar yapýlýr
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;
        }
    }

    GameObject FindNearestLeaf()
    {
        GameObject[] leaves = GameObject.FindGameObjectsWithTag("Leaf");
        GameObject nearestLeaf = null;
        float closestDistance = Mathf.Infinity; //baþlangýçta en yakýn mesafe sýnýrsýz ayarlanýr

        foreach (GameObject leaf in leaves)
        {
            float distance = Vector2.Distance(transform.position, leaf.transform.position);
            if (distance < closestDistance)
            {
                nearestLeaf = leaf;
                closestDistance = distance;
            }
        }
        return nearestLeaf;
    }

}
