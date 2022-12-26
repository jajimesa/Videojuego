using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // Importamos esta biblioteca

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisionamos con un "Trap" gameObject:
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("death");
        /* Ponemos RigidBody2D a Static, para desactivar su
         * movimiento y físicas. 
         */
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        /* Invocamos al Gestor de escenas y recargamos la escena (nivel)
         * en el que nos encontremos.
         */
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
