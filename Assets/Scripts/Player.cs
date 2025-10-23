using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    public CharacterController controlador;
    public float velocidad = 6f;
    public float tiempoSuavizadoGiro = 0.1f;
    float velocidadSuavizadoGiro;
    public Transform camara;

    void Update()
    {
        // Obtener entrada
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized;

        if (direccion.magnitude >= 0.1f)
        {
            // Calcular ángulo de rotación basado en la cámara
            float anguloObjetivo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloObjetivo, ref velocidadSuavizadoGiro, tiempoSuavizadoGiro);
            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            // Mover en la dirección a la que miramos
            Vector3 dirMovimiento = Quaternion.Euler(0f, anguloObjetivo, 0f) * Vector3.forward;
            controlador.Move(dirMovimiento.normalized * velocidad * Time.deltaTime);
        }
    }
}
