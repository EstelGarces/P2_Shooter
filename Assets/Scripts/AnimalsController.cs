using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsController : MonoBehaviour
{
    private Animator animator;

    public float velocidad = 2f; // Velocidad del movimiento
    public float tiempoEntreCambios = 3f; // Tiempo entre cambios de estado
    private float tiempoRestante; // Temporizador para el cambio de estado

    private bool estaMoviendo = true; // Indica si el animal se est� moviendo
    private Vector2 direccionMovimiento; // Direcci�n del movimiento
    private Vector2 ultimaDireccionValida = Vector2.right; // �ltima direcci�n v�lida


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        tiempoRestante = tiempoEntreCambios;
        CambiarEstado(); // Establecer un estado inicial

    }

    // Update is called once per frame
    void Update()
    {
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            CambiarEstado(); // Cambiar de estado
            tiempoRestante = tiempoEntreCambios; // Reiniciar temporizador
        }

        if (estaMoviendo)
        {
            MoverAnimal(); // Solo se mueve si est� en estado de movimiento
        }
        else
        {
            animator.SetBool("isActive", false);
        }
    }

    private void CambiarEstado()
    {
        estaMoviendo = Random.value > 0.5f; // 50% de probabilidad de moverse o quedarse quieto

        if (estaMoviendo)
        {
            // Elegir una direcci�n aleatoria de movimiento
            float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            direccionMovimiento = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
            ultimaDireccionValida = direccionMovimiento; // Actualizar la �ltima direcci�n v�lida
        }
        else
        {
            direccionMovimiento = Vector2.zero; // Sin movimiento
        }
    }

    // Mueve al animal
    private void MoverAnimal()
    {
        Vector2 movimiento = direccionMovimiento * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
        animator.SetBool("isActive", true);

        // Voltear el sprite dependiendo de la direcci�n del movimiento
        if (direccionMovimiento.x < 0) // Si el movimiento es hacia la izquierda
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Volteamos el sprite horizontalmente
        }
        else if (direccionMovimiento.x > 0) // Si el movimiento es hacia la derecha
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Devolvemos el sprite a su orientaci�n normal
        }
    }
    private void OnCollisionEnter2D()
    {
        // Elegir una nueva direcci�n aleatoria
        float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        direccionMovimiento = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
    }

    public Vector2 ObtenerDireccionMov()
    {        
        return ultimaDireccionValida;
    }

}
