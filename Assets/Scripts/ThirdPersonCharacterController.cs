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
        yInput = Input.GetAxis("Vertical");
        animator.SetFloat(forwardAnimParameter, yInput);
        if (Mathf.Abs(yInput) > movementInputThreshold)
        {
            var newRotation = transform.eulerAngles;
            newRotation.y = mainCameraTransform.eulerAngles.y;
            transform.eulerAngles = newRotation;
        }
        bool isSprinting = yInput > movementInputThreshold && Input.GetButton("Sprint");
        animator.SetBool(sprintAnimParameter, isSprinting);        
    }
}
