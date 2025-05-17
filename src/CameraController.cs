using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerCenter;
    [SerializeField] private Transform cameraTransform;
        
    [Header("Настройки камеры")]
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float rotationSmoothTime = 0.1f;

    [Header("Ограничение углов")]
    [SerializeField] private float minVerticalAngle = -30f;
    [SerializeField] private float maxVerticalAngle = 60f;

    [Header("Коллизии")]
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float cameraCollisionRadius = 0.3f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float targetDistance;
    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        targetDistance = distance;
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        
        yaw += mouseX;
        pitch -= mouseY;
        //todo
        
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);
        
        Vector3 targetRotation = new Vector3(pitch, yaw);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            targetDistance -= scroll * zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minZoom, maxZoom);
        }
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        Vector3 desiredPosition = playerCenter.position + rotation * new Vector3(0, 0, -targetDistance);

        
        RaycastHit hit;
        if (Physics.SphereCast(playerCenter.position, cameraCollisionRadius, 
                desiredPosition - playerCenter.position, out hit, 
                targetDistance, collisionMask))
        {
            cameraTransform.position = hit.point + hit.normal * cameraCollisionRadius;
        }
        else
        {
            cameraTransform.position = desiredPosition;
        }
     
        cameraTransform.LookAt(playerCenter.position);
    }
}
