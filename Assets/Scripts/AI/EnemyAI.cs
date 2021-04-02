using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health;
    public Slider healthBar;
    private Animator animator;
    [HideInInspector] public bool postureBroken = false;

    [Header("Pathfinding")]
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, .5f);

        healthBar.maxValue = health;
        healthBar.value = health;
    }

    private void OnEnable()
    {
        animator.SetBool("Walk", true);
    }

    private void OnDisable()
    {
        animator.SetBool("Walk", false);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }


    /// <summary>
    /// Reduces the health of the object.
    /// </summary>
    /// <param name="damage">Factor by which the health is to be dropped.</param>
    public void DealDamage(int damage)
    {
        CancelInvoke("FadeOut");
        CancelInvoke("HideHealthBar");

        healthBar.GetComponent<Animator>().ResetTrigger("Fadeout");
        health -= damage;
        healthBar.value = health;

        // The enemy dies
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        ShowHealthBar();
        Invoke("FadeOut", 2.25f);
    }


    /// <summary>
    /// Plays the fade out animation for health bar.
    /// </summary>
    private void FadeOut()
    {
        healthBar.GetComponent<Animator>().SetTrigger("Fadeout");
        Invoke("HideHealthBar", 1f);
    }

    /// <summary>
    /// Toggles the health bar on.
    /// </summary>
    private void ShowHealthBar()
    {
        healthBar.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Toggles the health bar off.
    /// </summary>
    private void HideHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }


    /// <summary>
    /// Reset the posturebroken attribute.
    /// </summary>
    private void EnablePosture()
    {
        postureBroken = false;
    }


    public IEnumerator RiposteHit()
    {
        yield return new WaitForSeconds(.2f);
        animator.SetTrigger("RiposteHit");
        yield return null;
    }

}
