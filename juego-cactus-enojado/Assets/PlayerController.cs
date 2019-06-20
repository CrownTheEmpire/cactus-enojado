using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float maxSpeed = 5f;
    public bool grounded;
    public float jumpPower = 6.5f;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //accede al componente <RigidBody2D> propio del padre de este <Script>
        anim = GetComponent<Animator>(); //accede al componente <Animator> propio del padre de este <Script>
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat( "Speed", Mathf.Abs( rb2d.velocity.x ) );
        anim.SetBool( "Grounded", grounded );

        if ( Input.GetKeyDown( KeyCode.UpArrow ) && grounded )
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        //Obtener los movimientos de las teclas Izqierda y/o Derecha del teclado
        float movHorizontal = Input.GetAxis( "Horizontal" );
        

        //aplicar fuerza al jugador
        rb2d.AddForce( Vector2.right * speed * movHorizontal );

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        //fijar la velocidad maxima
        rb2d.velocity = new Vector2( limitedSpeed, rb2d.velocity.y );

        if ( movHorizontal > 0.1f )
        {
            transform.localScale = new Vector3( 1f, 1f, 1f );
        }

        if ( movHorizontal < -0.1f )
        {
            transform.localScale = new Vector3( -1f, 1f, 1f );
        }

        if (jump)
        {
            rb2d.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
            jump = false;
        }
    }
}
