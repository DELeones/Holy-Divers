using Unity.Cinemachine;

using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    [Header("Sensibilidad")]
    public float horizontalSensitivity = 100f;
    public float verticalSensitivity = 100f;

    [Header("Límites de rotación vertical")]
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    [Header("Referencias")]
    public Transform playerTransform; // El transform del jugador
    public CinemachineVirtualCamera virtualCamera; // Referencia opcional a la cámara virtual

    private float rotationX = 0f;

    void Start()
    {
        // Oculta y bloquea el cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Si no se asignó la cámara, intentar encontrarla
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // Obtener input del ratón
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

        // Rotación vertical (limitada)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // Si tenemos una referencia al jugador, rotarlo horizontalmente
        if (playerTransform != null)
        {
            playerTransform.Rotate(Vector3.up * mouseX);
        }

        // Actualizar componente CinemachineRotationComposer si existe
        if (virtualCamera != null)
        {
            CinemachineComposer composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
            if (composer != null)
            {
                composer.m_TrackedObjectOffset.y = rotationX * 0.1f;
            }
        }
        else
        {
            // Aplicar rotación vertical directamente a la cámara si no usamos Cinemachine
            transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }
}