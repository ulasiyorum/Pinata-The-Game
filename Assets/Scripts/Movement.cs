using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rbx;
    [SerializeField] float movementSpeed;
    [SerializeField] float input;
    private float movement;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 10f;
        rbx = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.GetComponent<AttackingPinata>().openShop)
        { this.transform.position = new Vector2(Vector2.zero.x,this.transform.position.y); }
        else
        {
            rbx.MovePosition(rbx.position + Vector2.right * movement * Time.deltaTime * movementSpeed);
            float inp = Input.GetAxis("Horizontal");
            rbx.velocity = new Vector2(inp * movementSpeed, rbx.velocity.y);
        }    
    }


    public void Move(float value)
    {
        if(value == -1 || value == 1)
        {
            AudioManager.PlaySound("moving");
        }
        movement = value;
    }
}
