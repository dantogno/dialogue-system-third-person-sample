using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField]
    private bool useCinemachineForCameraRotation = false;
    [SerializeField]
    private Transform mainCameraTransform;
    [SerializeField]
    private float turnSpeed = 1.0f;
    [Tooltip("How much input before the character starts moving")]
    [SerializeField]
    private float movementInputThreshold = 0.1f;
    private Animator animator;
    private float yInput, xInput;
    private int sprintAnimParameter = Animator.StringToHash("IsSprinting");
    private int forwardAnimParameter = Animator.StringToHash("Forward");
    private Vector3 cameraOffset;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        cameraOffset = mainCameraTransform.position - transform.position;

    }
    private void Update()
    {
        GetMovementInput();
        UpdateForwardAnimParameter();
        if (!useCinemachineForCameraRotation)
            UpdateCamera();
        UpdateRotation();
        UpdateSprintAnimParameter();
    }

    private void UpdateCamera()
    {
        mainCameraTransform.transform.position = transform.position + cameraOffset;
        mainCameraTransform.Rotate(0, turnSpeed * xInput * Time.deltaTime, 0);
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
            newRotation.y = mainCameraTransform.localEulerAngles.y;
            transform.eulerAngles = newRotation;
        }
    }

    private void UpdateForwardAnimParameter()
    {
        animator.SetFloat(forwardAnimParameter, yInput);
    }

    private void GetMovementInput()
    {
        xInput = Input.GetAxis("Horizontal");
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
