using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liz1Controller : MonoBehaviour
{
    private Animator animator;

    public float velocidad = 2f; // Velocidad inicial del movimiento
    public float tiempoEntreCambios = 3f; // Tiempo entre cambios de estado
    private float tiempoRestante; // Temporizador para el cambio de estado

    private bool estaMoviendo = true; // Indica si el animal se está moviendo
    private Vector2 direccionMovimiento; // Dirección del movimiento
    private Vector2 ultimaDireccionValida = Vector2.right; // Última dirección válida

    private float velocidadMinima = 2f; // Velocidad mínima
    private float velocidadMaxima = 5f; // Velocidad máxima
    private float tiempoCambioVelocidad = 5f; // Tiempo para cambiar la velocidad

    private float tiempoRestanteVelocidad; // Temporizador para el cambio de velocidad

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        tiempoRestante = tiempoEntreCambios;
        tiempoRestanteVelocidad = tiempoCambioVelocidad;
        
        CambiarEstado(); // Establecer un estado inicial
        direccionMovimiento = ultimaDireccionValida; // Inicializamos con la última dirección válida
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
            MoverAnimal(); // Solo se mueve si está en estado de movimiento
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
            // Elegir una dirección aleatoria de movimiento
            float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            direccionMovimiento = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
            ultimaDireccionValida = direccionMovimiento; // Actualizar la última dirección válida

            //StartCoroutine(EsperarYMoverse(40f)); // Iniciar la espera
        }
        else
        {
            direccionMovimiento = Vector2.zero; // Sin movimiento
        }
    }

    private void CambiarVelocidad()
    {
        // Cambiar la velocidad aleatoriamente entre un rango mínimo y máximo
        velocidad = Random.Range(velocidadMinima, velocidadMaxima);
    }

    // Mueve al animal
    private void MoverAnimal()
    {
        // Si no hay nueva dirección, utilizamos la última dirección válida
        if (direccionMovimiento == Vector2.zero)
        {
            direccionMovimiento = ultimaDireccionValida; // Mantener la dirección anterior
        }

        Vector2 movimiento = direccionMovimiento * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
        animator.SetBool("isActive", true);

        //StartCoroutine(EsperarYMoverse(40f));
        // Voltear el sprite dependiendo de la dirección del movimiento
        if (direccionMovimiento.x < 0) // Si el movimiento es hacia la izquierda
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Volteamos el sprite horizontalmente
        }
        else if (direccionMovimiento.x > 0) // Si el movimiento es hacia la derecha
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Devolvemos el sprite a su orientación normal
        }
    }

    private IEnumerator EsperarYMoverse(float tiempoEspera)
    {
        // Durante este tiempo, no se mueve
        yield return new WaitForSeconds(tiempoEspera);
    }

    private void OnCollisionEnter2D()
    {
        // Elegir una nueva dirección aleatoria al colisionar
        float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        direccionMovimiento = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
        ultimaDireccionValida = direccionMovimiento; // Actualizar la última dirección válida al colisionar
    }

    public Vector2 ObtenerDireccionMov()
    {        
        Debug.Log("Direccion " + ultimaDireccionValida.normalized);
        return ultimaDireccionValida;
    }
}

