using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /* Instanciamos un objeto de tipo Transform para poder referenciar
     * a la componente Transform (la que nos dice posici�n, giro, etc.)
     * de Player. Podr�amos establecer la referencia desde el script,
     * pero lo vamos a hacer desde el editor de Unity. Para ello, usar
     * [SerializeField] de nuevo.
     */
    [SerializeField] private Transform player;
    private void Update()
    {
        /* NOTA IMPORTANTE. Transform es una componente de nuestra c�mara,
         * como RigidBody2D lo es de Player. Es tan com�n tener que acceder
         * a Transform, que no es necesario invocar a GetComponent<Transform>
         * para ello, podemos acceder directamente a ella.
         */
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }
}
