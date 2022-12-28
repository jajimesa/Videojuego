using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    /* Podríamos darle una animación directamente a la sierra,
     * pero vamos a hacer que gire manualmente mediante un script.
     */

    [SerializeField] private float speed = 2f;
    private void Update()
    {
        /* Rotamos en cada actualización de frames, sobre el eje Z:
         */
        transform.Rotate(0, 0, 360 * Time.deltaTime * speed);
    }
}
