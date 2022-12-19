using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    /* Como hemos marcado Cherry como "trigger" podemos usar reescribir el siguiente método en lugar de
     * OnCollisionEnter2D. OnTriggerEnter2D es un evento que se desencadena cuando el collider2D del
     * jugador entra en contacto con otro Collider2D que se ha marcado como trigger. "collision" es el
     * Collider2D con el que se entra en contacto.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Si el objeto que está asociado a collision tiene una etiqueta (tag) "Cherry" es que nos hemos
         * entrado en contacto con una cherry. En ese caso, la destruimos.
         */
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
        }
    }
}
