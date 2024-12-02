using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liz1Controller : MonoBehaviour
{
    private Animator animator;

    public float velocidad = 2f; // Velocidad inicial del movimiento
    public float tiempoEntreCambios = 3f; // Tiempo entre cambios de estado
    private float tiempoRestante; // Temporizador para el cambio de estado

    private bool estaMoviendo = true; // Indica si el animal se est� moviendo
    private Vector2 direccionMovimiento; // Direcci�n del movimiento
    private Vector2 ultimaDireccionValida = Vector2.right; // �ltima direcci�n v�lida

    private float velocidadMinima = 2f; // Velocidad m�nima
    private float velocidadMaxima = 5f; // Velocidad m�xima
    private float tiempoCambioVelocidad = 5f; // Tiempo para cambiar la velocidad

    private float tiempoRestanteVelocidad; // Temporizador para el cambio de velocidad

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        tiempoRestante = tiempoEntreCambios;
        tiempoRestanteVelocidad = tiempoCambioVelocidad;
        
        CambiarEstado(); // Establecer un estado inicial
        direccionMovimiento = ultimaDireccionValida; // Inicializamos con la �ltima direcci�n v�lida
    }

    // Update is called once per frame
    void Update()
    {
        // Controlar el temporizador para el cambio de estado de movimiento
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            CambiarEstado(); // Cambiar de estado
            tiempoRestante = tiempoEntreCambios; // Reiniciar temporizador
        }

        // Controlar el temporizador para el cambio de velocidad
        tiempoRestanteVelocidad -= Time.deltaTime;
        if (tiempoRestanteVelocidad <= 0)
        {
            CambiarVelocidad(); // Cambiar la velocidad
            tiempoRestanteVelocidad = tiempoCambioVelocidad; // Reiniciar temporizador
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

            //StartCoroutine(EsperarYMoverse(40f)); // Iniciar la espera
        }
        else
        {
            direccionMovimiento = Vector2.zero; // Sin movimiento
        }
    }

    private void CambiarVelocidad()
    {
        // Cambiar la velocidad aleatoriamente entre un rango m�nimo y m�ximo
        velocidad = Random.Range(velocidadMinima, velocidadMaxima);
    }

    // Mueve al animal
    private void MoverAnimal()
    {
        // Si no hay nueva direcci�n, utilizamos la �ltima direcci�n v�lida
        if (direccionMovimiento == Vector2.zero)
        {
            direccionMovimiento = ultimaDireccionValida; // Mantener la direcci�n anterior
        }

        Vector2 movimiento = direccionMovimiento * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
        animator.SetBool("isActive", true);

        //StartCoroutine(EsperarYMoverse(40f));
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

    private IEnumerator EsperarYMoverse(float tiempoEspera)
    {
        // Durante este tiempo, no se mueve
        yield return new WaitForSeconds(tiempoEspera);
    }

    private void OnCollisionEnter2D()
    {
        // Elegir una nueva direcci�n aleatoria al colisionar
        float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        direccionMovimiento = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
        ultimaDireccionValida = direccionMovimiento; // Actualizar la �ltima direcci�n v�lida al colisionar
    }

    public Vector2 ObtenerDireccionMov()
    {        
        Debug.Log("Direccion " + ultimaDireccionValida.normalized);
        return ultimaDireccionValida;
    }
}

