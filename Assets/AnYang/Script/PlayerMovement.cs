using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 autoMoveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // �÷��̾� �Է��� ����
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // �̵� ���� ����
        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        // ĳ���� �̵�
        controller.Move(moveDirection * Time.deltaTime);

    }
}