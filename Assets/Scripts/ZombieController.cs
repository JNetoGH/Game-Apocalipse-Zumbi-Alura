using UnityEngine;

public class ZombieController : MonoBehaviour
{

    // THE SPEED THE ENEMY IS GOING TO MOVE
    public float movementSpeed = 2f;
    
    // PLAYER RELATED VARIABLES
    public GameObject player;
    public float minDistanceFromPlayer = 2.5f;
    private Vector3 directionToPlayer;
    
    // COMPONENTS VARIABLES
    private Rigidbody zombieRigidbody;
    private Animator zombieAnimator;

    // USED TO INITIALIZE COMPONENTS
    void Start()
    {
        zombieRigidbody = this.GetComponent<Rigidbody>();
        zombieAnimator = this.GetComponent<Animator>();
    }
    
    // MOVE PARA A DIREÇÃO DO PLAYER
    private void MoveZombieToPlayerDirection() 
    {
        Vector3 increment = directionToPlayer * movementSpeed * Time.deltaTime;
        Vector3 newPosition = zombieRigidbody.position + increment;
        zombieRigidbody.MovePosition(newPosition); // Move pelo Rigidbody para a direcao do player 
    }
    
    // ROTACIONA PARA A DIREÇÃO DO PLAYER, LookRotation RECEBE A DIREÇÃO PRO PLAYER 
    void RotateZombieToPlayerDirection() 
    {
        Quaternion newRotation = Quaternion.LookRotation(directionToPlayer); 
        zombieRigidbody.MoveRotation(newRotation); // rotaciona pelo Rigidbody
    }
    
    private void FixedUpdate()
    {
        /* pega a direção até o player, tirando a diferença entre os dois Vector3 e normaliza o Vector3 resultante
         * oq resulta em algo próximo de (-1/1, 0, -1/1) sendo só mesmo a direção para onde o mob deve ir */
        directionToPlayer = (player.transform.position - this.transform.position).normalized;

        // rotaciona o MOB
        RotateZombieToPlayerDirection(); 
        
        // retorna a distancia entre os Vectors, no caso a distancia do zombie pro player
        float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        /* CHECA O RANGE PRO MOVIMENTO: para de se mover quando chegar perto do player,
         * cada radius == 1 unidade que foi setado la no coillder, ent tem q ser maior q 2 unidades
         * isso tudo pq a ideia eh fazer o zombie parar um pouco antes do player pra n encostar nele */
        if (distanceFromPlayer > minDistanceFromPlayer) // quando longe
        {
            MoveZombieToPlayerDirection();
            zombieAnimator.SetBool("attacking", false);
        }
        else //quando perto
        {
            zombieAnimator.SetBool("attacking", true);
        }
    }
    
    // tem que ter o mesmo nome do evento da animacao, vai ser chamado quando o evento acontecer
    void AttackPlayer() 
    {
        player.GetComponent<PlayerController>().isPlayerAlive = false;
    }

}
