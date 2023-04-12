using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject player;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    private float mouseX, mouseY;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private InputAction mouseXAction;
    private InputAction mouseYAction;

    public void Initialize(InputAction mouseX, InputAction mouseY)
    {
        Cursor.lockState = CursorLockMode.Locked;

        mouseXAction = mouseX;
        mouseXAction.Enable();

        mouseYAction = mouseY;
        mouseYAction.Enable();
    }

    private void Update()
    {
        PerformCameraRotation();
    }

    private void PerformCameraRotation()
    {
        // get mouse movement
        mouseX = (mouseXAction.ReadValue<float>()) * sensitivityX * Time.deltaTime;
        mouseY = (mouseYAction.ReadValue<float>()) * sensitivityY * Time.deltaTime;

        // calculate rotation
        xRotation -= mouseY;
        yRotation += mouseX;

        // restrict x-rotation degree
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate
        playerCamera.transform.position = player.transform.position;
        playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerCamera.transform.position += playerCamera.transform.right * offset.x + playerCamera.transform.up * offset.y + playerCamera.transform.forward * offset.z;

    }
    public Transform GetCameraTransform()
    {
        return playerCamera.transform;
    }
}
