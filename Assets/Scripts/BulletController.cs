using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 30; // velocidade de movimento da bala
    private Rigidbody objRigidbody; 
    
    // chamado quando a bala encontra algo que tenha collider
    // other == collider o objeto colidido
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Inimigo")
        { 
            Destroy(other.gameObject);
        }
        // destroi a bala depois de qualquer colisao
        Destroy(this.gameObject);  // gameObject que possui esse scripts
    }
    
    private void Start()
    {
        objRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        objRigidbody.MovePosition(objRigidbody.position + transform.forward * speed* Time.deltaTime); 
    }
    
}
