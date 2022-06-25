using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    public float distanceFromPlayerInY = 0;
    public float distanceFromPlayerInZ = 0;
    
    private Vector3 compensationDistance;
    
    void MoveToPlayerPosition() 
    { // se tiver algo nas distancias move a camera pra lá no start, se n, deixa onde foi largada na Cena
        transform.position = new Vector3(player.transform.position.x, 
            distanceFromPlayerInY == 0 ? this.transform.position.y : player.transform.position.y + distanceFromPlayerInY,
            distanceFromPlayerInZ == 0 ? this.transform.position.z : player.transform.position.z + distanceFromPlayerInZ);
    }
    
    void Start()
    {
        MoveToPlayerPosition();
        compensationDistance = this.transform.position - player.transform.position;
    }
    
    void Update()
    {
        this.transform.position = player.transform.position + compensationDistance;
    }
}
