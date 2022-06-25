using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunController : MonoBehaviour
{
     public GameObject bullet;
     public GameObject bulletInstantiationPoint;

     // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // cria um novo game object (obj, pos, rotacao)
            // vai puxar o prefab da bala pra ser instanciada no jogo
            Instantiate(bullet, bulletInstantiationPoint.transform.position, bulletInstantiationPoint.transform.rotation);
        }
    }
}