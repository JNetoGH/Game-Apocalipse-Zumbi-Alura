using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class PlayerTestController : MonoBehaviour
{
    public float movementSpeed = 5;
    private Vector3 movementDirection;
    private Rigidbody objRigidbody;
    public LayerMask groundLayerMask;

    void Start() {
        objRigidbody = this.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void Move()
    {
        Vector3 increment = movementDirection * movementSpeed * Time.deltaTime;
        Vector3 newPosition = objRigidbody.position + increment;
        objRigidbody.MovePosition(newPosition);
    }
    
    private void RotateBasedOnMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact; 
        const float RAY_MAX_LENGTH = 100f; 
        bool isImpactingTheGroundLayer = Physics.Raycast(ray, out impact, RAY_MAX_LENGTH, groundLayerMask);
        if (isImpactingTheGroundLayer) // rotaciona o jogador para onde o ray encontrar com algum objeto na layer do chão
        {
            Vector3 posicaoMiraJogador = impact.point - this.transform.position;  // o player vai rotacionar para essa variável
            posicaoMiraJogador.y = this.transform.position.y;
            Quaternion newRotation = Quaternion.LookRotation(posicaoMiraJogador); // cria um quaternion com base na mira do jogador
            objRigidbody.MoveRotation(newRotation); // rotaciona para o quaternion
        }
        Debug.DrawLine(ray.origin,impact.point, isImpactingTheGroundLayer ? Color.green : Color.red);
    }
    
    
    
    
    private void FixedUpdate()
    {
        
        Move();
        RotateBasedOnMouse();
     
    }
}
