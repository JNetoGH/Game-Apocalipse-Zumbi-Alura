using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTest : MonoBehaviour
{


    private CharacterController characterController;
    public float speed = 5;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        float xAxisVariation = Input.GetAxis("Horizontal");
        float zAxisVariation = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xAxisVariation, 0, zAxisVariation);


        characterController.Move(direction * speed * Time.deltaTime);

    }
}
