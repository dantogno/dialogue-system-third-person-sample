using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform mainCameraTransform;
    [Tooltip("How much input before the character starts moving")]
    [SerializeField]
    private float movementInputThreshold = 0.1f;
    private Animator animator;
    private float yInput;
    private int sprintAnimParameter = Animator.StringToHash("IsSprinting");
    private int forwardAnimParameter = Animator.StringToHash("Forward");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        GetMovementInput();
        UpdateForwardAnimParameter();
        UpdateRotation();
        UpdateSprintAnimParameter();
    }

    private void UpdateSprintAnimParameter()
    {
        bool isSprinting = yInput > movementInputThreshold && Input.GetButton("Sprint");
        animator.SetBool(sprintAnimParameter, isSprinting);
    }

    private void UpdateRotation()
    {
        if (Mathf.Abs(yInput) > movementInputThreshold)
        {
            var newRotation = transform.eulerAngles;
            newRotation.y = mainCameraTransform.eulerAngles.y;
            transform.eulerAngles = newRotation;
        }
    }

    private void UpdateForwardAnimParameter()
    {
        animator.SetFloat(forwardAnimParameter, yInput);
    }

    private void GetMovementInput()
    {
        yInput = Input.GetAxis("Vertical");
    }

    private void OnDisable()
    {
        ResetAnimParameters();
    }

    private void ResetAnimParameters()
    {
        animator.SetFloat(forwardAnimParameter, 0);
        animator.SetBool(sprintAnimParameter, false);
    }
}
