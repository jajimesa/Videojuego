using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de nivel

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool hasFinished = false;

    private void Start()
    {
        finishSound= GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !hasFinished)
        {
            finishSound.Play();
            hasFinished = true;
            Invoke("CompleteLevel", 2f); // Invocamos CompleteLevel a los 2 seg.
        }
    }

    private void CompleteLevel()
    {   
        // Cargamos la siguiente Scene sumando uno al índice de escena.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
