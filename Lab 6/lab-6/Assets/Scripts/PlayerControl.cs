using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameController;

    [SerializeField] private GameObject damager;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private Animator animator;
    private float horizontal;
    private float vertical;

    private bool isStanding = true;
    private float moveSpeed;

    public Vector3 moveDirection;

    private Quaternion look;
    private Quaternion prevLook;

    private InputAction moveAction;
    private InputAction runAction;
    private InputAction attackAction;

    public bool allowAttack = true;

    private bool doingAttackAnimation = false;

    public void Initialize(InputAction moveAction, InputAction runAction, InputAction attackAction)
    {
        this.moveAction = moveAction;
        moveAction.Enable();

        this.runAction = runAction;
        runAction.Enable();

        this.attackAction = attackAction;
        attackAction.Enable();

        animator = player.GetComponent<Animator>();
    }

    private void Update()
    {

        moveSpeed = 0f;

        horizontal = moveAction.ReadValue<Vector2>().x;
        vertical = moveAction.ReadValue<Vector2>().y;

        Transform camera = gameController.GetComponent<CameraControl>().GetCameraTransform();
        moveDirection = camera.forward * vertical + camera.right * horizontal;

        PerformStanding();

        PerformWalking();

        PerformRunning();

        if (allowAttack)
            PerformAttacking();

        player.transform.position += moveDirection * moveSpeed * Time.deltaTime;

        Vector3 rot = player.transform.eulerAngles;
        player.transform.eulerAngles = new Vector3(0f, rot.y, 0f);
    }

    private void PerformStanding()
    {
        isStanding = true;
        animator.SetBool("standing", isStanding);
    }
    private void PerformWalking()
    {

        bool isWalking = (horizontal != 0f || vertical != 0f);
        if (isWalking)
        {
            moveSpeed = walkSpeed;
            look = Quaternion.LookRotation(moveDirection);
            isStanding = false;
        }
        if (!doingAttackAnimation)
        {
            player.transform.localRotation = Quaternion.RotateTowards(player.transform.rotation, look, rotateSpeed * Time.deltaTime);
        }
        
        animator.SetBool("walking", isWalking);

    }

    private void PerformRunning()
    {
        bool isRunning = runAction.IsPressed();
        if (isRunning)
        {
            moveSpeed = runSpeed;
            isStanding = false;
        }
        animator.SetBool("running", isRunning);
    }

    private void PerformAttacking()
    {
        bool isAttacking = attackAction.IsPressed();

        if (!doingAttackAnimation)
        {
            animator.SetBool("isAttacking", isAttacking);
            if (isAttacking)
            {
                animator.SetTrigger("attack");
                doingAttackAnimation = true;
            }
        }
        if (doingAttackAnimation)
        {
            moveSpeed = 0;
        }
    }

    public void FinishAttack()
    {
        doingAttackAnimation = false;
    }

    private void Attack()
    {
        damager.GetComponent<Damager>().Attack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            gameController.GetComponent<ConstructionControl>().AddWood(1);
            Destroy(collision.gameObject);
        }
    }
}
