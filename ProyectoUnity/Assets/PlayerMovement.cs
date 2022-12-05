using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        this.rb.velocity = new Vector2(dirX * 7, this.rb.velocity.y);
        if(Input.GetButtonDown("Jump"))
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, 14);
        }
        
    }
}
