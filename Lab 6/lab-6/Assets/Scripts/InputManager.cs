using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] private PlayerControl movementController;
    [SerializeField] private CameraControl cameraController;
    [SerializeField] private ConstructionControl constructController;

    private InputActions inputActions;

    private void Awake()
    {
        Application.targetFrameRate = 144;

        inputActions = new InputActions();

        movementController.Initialize(inputActions.Player.Move, inputActions.Player.Run, inputActions.Player.Attack);

        cameraController.Initialize(inputActions.Player.MouseX, inputActions.Player.MouseY);

        constructController.Initialize(inputActions.Player.Build, inputActions.Player.Attack);
    }

    private void OnEnable()
    {
    }
}
