using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Get the infection and recovery chance from the start menu input
//TODO: Figure out how to make cells bounce off walls and everything and maintain momentum/velocity after bounce

public class Cell : MonoBehaviour
{
    public const string CellTag = "Cell";

    private const float InfectionChance = 1f;

    private const float RecoveryChance = 0.5f;

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

        //Get random direction
        var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        //Get random speed
        var speed = Random.Range(15f, 40f);

        //Add force to move cell
        rb.AddForce(direction.normalized * speed);

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
            //TODO: Do whatever general logic for hitting wall
            return;
        }

        var otherCell = other.GetComponent<Cell>();

        if (otherCell.Infected)
        {
            TryInfect();
        }
    }

    private void TryInfect()
    {
        var rand = Random.Range(0f, 1f);

        if (rand > InfectionChance)
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
        var rand = Random.Range(0f, 1f);

        if (rand > RecoveryChance)
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
}
