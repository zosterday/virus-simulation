using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCompCell : MonoBehaviour
{
    public const string CellTag = "Cell";

    public CellColor CellColor { get; set; }

    private Color redColor = Color.red;

    private Color greenColor = Color.green;

    private Color blueColor = Color.blue;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        //Get SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Get Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        switch (CellColor)
        {
            case CellColor.red:
                spriteRenderer.color = redColor;
                break;

            case CellColor.green:
                spriteRenderer.color = greenColor;
                break;

            case CellColor.blue:
                spriteRenderer.color = blueColor;
                break;

            default:
                spriteRenderer.color = Color.white;
                break;
        }
        AddRandomForce();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerColorComp.Instance.IsSimActive)
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

        var otherCell = other.GetComponent<ColorCompCell>();

        if (otherCell.CellColor == CellColor.white && CellColor == CellColor.white)
        {
            return;
        }

        switch (otherCell.CellColor)
        {
            case CellColor.red:
                if (CellColor == CellColor.green)
                {
                    UpdateColor(CellColor.red);
                    GameManagerColorComp.Instance.UpdateGreenCount(-1);
                    GameManagerColorComp.Instance.UpdateRedCount(1);
                }
                if (CellColor == CellColor.white)
                {
                    UpdateColor(CellColor.red);
                    GameManagerColorComp.Instance.UpdateRedCount(1);
                }
                break;

            case CellColor.green:
                if (CellColor == CellColor.blue)
                {
                    UpdateColor(CellColor.green);
                    GameManagerColorComp.Instance.UpdateBlueCount(-1);
                    GameManagerColorComp.Instance.UpdateGreenCount(1);
                }
                if (CellColor == CellColor.white)
                {
                    UpdateColor(CellColor.green);
                    GameManagerColorComp.Instance.UpdateGreenCount(1);
                }
                break;

            case CellColor.blue:
                if (CellColor == CellColor.red)
                {
                    UpdateColor(CellColor.blue);
                    GameManagerColorComp.Instance.UpdateRedCount(-1);
                    GameManagerColorComp.Instance.UpdateBlueCount(1);
                }
                if (CellColor == CellColor.white)
                {
                    UpdateColor(CellColor.blue);
                    GameManagerColorComp.Instance.UpdateBlueCount(1);
                }
                break;

            default:
                break;
        }
    }

    public void UpdateColor(CellColor newCellColor)
    {
        CellColor = newCellColor;
        if (newCellColor == CellColor.red)
        {
            spriteRenderer.color = redColor;
        }
        if (newCellColor == CellColor.green)
        {
            spriteRenderer.color = greenColor;
        }
        if (newCellColor == CellColor.blue)
        {
            spriteRenderer.color = blueColor;
        }
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

