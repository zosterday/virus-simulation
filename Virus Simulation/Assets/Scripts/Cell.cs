using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Figure out how to make cells bounce off walls and everything and maintain momentum/velocity after bounce

public class Cell : MonoBehaviour
{
    public const string CellTag = "Cell";

    public bool Infected { get; set; }

    private Color healthyColor = Color.white;

    private Color InfectedColor = Color.green;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //Get SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Get Rigidbody component
        rb = GetComponent<Rigidbody2D>();

        AddRandomForce();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsSimActive)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;

        if (!other.CompareTag(CellTag))
        {
            AddRandomForce();
            return;
        }

        var otherCell = other.GetComponent<Cell>();

        if (otherCell.Infected && !Infected)
        {
            TryInfect();
        }
    }

    private void TryInfect()
    {
        var rand = Random.Range(0f, 1f);

        if (rand > StateMachine.InfectionChance)
        {
            return;
        }

        Infect();
        StartCoroutine(WaitForRecover());
    }

    public void Infect()
    {
        Infected = true;
        spriteRenderer.color = InfectedColor;
        GameManager.Instance.UpdateHealthyCount(-1);
        GameManager.Instance.UpdateInfectedCount(1);
    }

    private void TryRecover()
    {
        if (!GameManager.Instance.IsSimActive)
        {
            return;
        }

        var rand = Random.Range(0f, 1f);

        if (rand > StateMachine.RecoveryChance)
        {
            return;
        }

        Infected = false;
        spriteRenderer.color = healthyColor;
        GameManager.Instance.UpdateHealthyCount(1);
        GameManager.Instance.UpdateInfectedCount(-1);
    }

    private IEnumerator WaitForRecover()
    {
        yield return new WaitForSeconds(10f);
        TryRecover();
    }

    private void AddRandomForce()
    {
        //Get random direction
        var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        //Get random speed
        var speed = Random.Range(20f, 65f);

        //Add force to move cell
        rb.AddForce(direction.normalized * speed);
    }
}
