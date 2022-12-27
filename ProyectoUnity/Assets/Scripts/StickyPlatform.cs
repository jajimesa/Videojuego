using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    /* Ahora buscamos que una vez el jugador se encuentre sobre una plataforma m�vil,
     * su componente transform (que da su posici�n), se acople (emparente) a la transform
     * de la plataforma. Para ello, comprobamos si hay solapamiento entre hitboxes (si existe
     * colisi�n) con OnTriggerEnter2D
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Si nuestra plataforma colisiona con el jugador (si el nombre del objeto con el que
         * se colisiona es "Player"), emparentamos la componente transform del jugador con
         * la transform de la plataforma (recordar que basta escribir "transform", no hace falta
         * invocar a GetComponent<Transform>.
         */
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    /* Cuando el jugador ya no est� en contacto con la plataforma, queremos que se desacople
     * su movimiento de �sta. Usamos ahora OnTriggerExit2D y procedemos de forma an�loga, pero
     * deshaciendo el emparentamiento.
     */

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
