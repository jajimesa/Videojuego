using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class WaypontFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;    // Almacenamos los waypoints en el vector
    private int currentWayPointIndex = 0;

    private float epsilon = 0.1f;
    [SerializeField] float speed = 2.0f;


    private void Update()
    {
        /* Recordar que Update() se invoca en cada nuevo frame.
         * Comprobamos si la distancia entre la posición de nuestro objeto (plataforma, por ejemplo) y el waypoint
         * es menor que épsilon (no podemos establecer la igualdad con floats).
         */
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < epsilon)
        {
            currentWayPointIndex++;
            
            // Quedaría más elegante utilizando aritmética módulo waypoints.Length
            if (currentWayPointIndex >= waypoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        /* Cambiamos la posición de nuestro objeto en dirección el waypoint. Time.deltaTime nos devuelve el intervalo en segundos entre frames.
             * Hacer el cálculo del desplazamiento multiplicando Time.deltaTime por la velocidad nos permite establecer un movimiento INDEPENDIENTE
             * del framerate al que corra nuestro videojuego.
             */
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }
}
