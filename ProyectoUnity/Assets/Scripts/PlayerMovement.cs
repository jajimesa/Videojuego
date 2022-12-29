using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* NOTA: Evitamos usar "this" para no ensuciar el código, en este
     * contexto no hace falta usarlo continuamente porque solo se va a
     * trabajar sobre Player.
     */
    
    /* Instanciamos un objeto de tipo RigidBody2D para referenciar más
     * adelante a la componente Rigidbody2D de Player. Si no guardamos
     * la referencia, e invocamos GetComponent() en Update, perderemos
     * rendimiento de forma inncecesaria.
     */
    private Rigidbody2D rb;


    /* Ahora instanciamos un objeto de tipo Animator para poder referenciar
     * a los diferentes booleanos que hemos creado en Unity para determinar
     * cuando se transiciona de una animación a otra (Idle->Running, con el
     * booleano running, etc.). Como se trata de una componente de Player,
     * guardamos en este objeto la refencia a su Animator.
     */
    private Animator anim;

    // Instanciamos un float para almacenar el input del eje X.
    private float dirX = 0;
    private float epsilon = 0.001f;

    /* Instanciamos dos floats más, para almacenar las magnitudes del salto
     * y del movimiento. [SerializeField] permite que estos valores puedan
     * ser visibles y modificables desde el Inspector de Unity en Player,
     * sobre el panel-componente que referencia a este script. En lugar de
     * [SerializeField] podemos obtener el mismo resultado declarandolos
     * "public", pero exponiendonos a que otros scripts puedan acceder a ellos.
     */
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float jumpForce = 14;

    /* Para poder referenciar al SpriteRenderer de nuestro Player, y jugar
     * con que, por ejemplo, pueda cambiar de sentido el sprite cuando el
     * juego detecte que vamos marcha atrás, instanciamos un objeto de tipo
     * SpriteRenderer.
     */
    private SpriteRenderer sprite;

    /* Creamos una enumeración para distinguir los diferentes estados del
     * jugador (idle, running, jumping, falling) y así poder programar el
     * cambio entre animaciones desde el script para luego pasarselo al
     * Animator por referencia.
     */
    private enum MovementState { iddle, running, jumping, falling }
    private MovementState state;

    /* Guardamos en collider la componente BoxCollider2D de nuestro player.
     * Creamos una "LayerMask" que usaremos más adelante para la colisión
     * con el suelo.
     */
    private BoxCollider2D collider;
    [SerializeField] private LayerMask jumpableGround;

    /* Declaramos un AudioSource para saltar. Reproduciremos el sonido cada
     * vez que el jugador salte.
     */
    [SerializeField] private AudioSource jumpSoundEffect;


    // Start es invocado en el primer frame de ejecución.
    private void Start()
    {   
        // Obtenemos las refencias a las componentes de Player.
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update es invocado en cada nuevo frame.
    private void Update()
    {
        /* Input.GetAxis() e Input.GetAxisRaw() nos devuelve un input de estilo joystick,
         * es decir, una especie de input numérico variable en función de cuánto estemos 
         * presionando el joystick en una dirección. La diferencia entre los dos métodos
         * es que .GetAxisRaw() regresa a 0 inmediatamente después de dejar de pulsar la
         * palanca, mientras que .GetAxis() regresa a 0 de forma gradual. 
         */
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);   // Mantenemos la velocidad en el eje Y si no queremos que frene en seco.
        
        /* Input.GetButtonDown cuenta ya con varios "botones" prefabricados, accedemos a ellos con el nombre que les da Unity en forma de string.
         */
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();     // Reproducimos el sonido de salto.
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);     // Mantenemos la velocidad en el eje X si no queremos que frene en seco.
        }

        // Invocamos a UpdateAnimationState() para cambiar de animación si hace falta.
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX != 0)   // Si dirX!=0 es porque hay input de movimiento.
        {
            state = MovementState.running;
            if (dirX < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        else
        {
            state = MovementState.iddle;
        }

        /* Para velocidades es mejor usar un épsilon, debido a la baja precisión de los
         * float y a que es un valor que cambia constantemente.
         */
        if (rb.velocity.y > epsilon)
        {
            state = MovementState.jumping;
        } 
        else if (rb.velocity.y < -epsilon)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("State", (int)state);
    }

    /* Physics2D.BoxCast castea una hitbox alrededor del player, la guarda en jumpableGround
     * y devuelve true si esta hitbox entra en contacto con otra hitbox (en nuestro caso, será
     * del suelo).
     */
    private bool isGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
