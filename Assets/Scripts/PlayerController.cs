using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    private Vector3 direction;
    
    private Animator playerAnimator;
    private Rigidbody playerRigidbody;
    
    public LayerMask groundLayerMask; // PARA ROTACAO: mascara que guarda a layer do chao
    public bool isPlayerAlive = true; // USADO NO GameController PARA RESETAR O JOGO QUANDO ATINGIDO POR ZUMBI

    
    void Start() // Usei para inicializar as variaveis que guardam os componentes
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    private void MovePlayer() // Move o Player via Rigidbody   
    {
        Vector3 increment = direction * Time.deltaTime * speed;
        Vector3 newPosition = playerRigidbody.position + increment;
        playerRigidbody.MovePosition(newPosition);
    }
    
    private void AnimatePlayer() // Anima o player, manipulando os parametros do Animator Controller.
    { 
        bool isMoving = direction != Vector3.zero;
        playerAnimator.SetBool("moving", isMoving);
    }

    private void RotatePlayerBasedOnMouse() // Rotaciona baseando-se na posição do mouse em relacao a posicao camera
    {
        // retorna um Ray partindo da câmera até um Vector 3 "posição na cena"
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // RaycastHit pega a posição onde um raio bate em um objeto que possui colLider
        RaycastHit impact; // no C#: faz com que seja possível usar variáveis vazias, "agr n tenho, mas dentro dele terá terei valor"
        const float RAY_MAX_LENGTH = 100f; // o raio não pode ser maior que X unidades
        bool isImpactingTheGroundLayer = Physics.Raycast(ray, out impact, RAY_MAX_LENGTH, groundLayerMask);
  
        // desenha o raio na scene view (Vector3 ponto inicial, Vector3 ponto final, cor "opcional")
        Debug.DrawLine(ray.origin,impact.point, isImpactingTheGroundLayer ? Color.green : Color.red);
  
        // rotaciona o jogador para onde o ray encontrar com algum objeto na layer do chão
        if (isImpactingTheGroundLayer)
        {
            // o player vai rotacionar para essa variável
            Vector3 posicaoMiraJogador = impact.point - this.transform.position;
            /* cancela a rotação no eixo Y, fazendo que a mira em Y seja sempre a mesma da posição jogador,
             * assim ele não gira pra cima caso passe em cima de collider alto */
            posicaoMiraJogador.y = this.transform.position.y;
      
            // cria um quaternion com base na mira do jogador
            Quaternion newRotation = Quaternion.LookRotation(posicaoMiraJogador);
            playerRigidbody.MoveRotation(newRotation); // rotaciona para o quaternion
        }
    }

    void Update()
    {
        float variationX = Input.GetAxis("Horizontal"); // input do teclado para mover no eixo X
        float variationZ = Input.GetAxis("Vertical"); // input do teclado para mover no eixo Z
        direction = new Vector3(variationX, 0, variationZ); // calcula a direcao para onde o player vai
        
        AnimatePlayer();
    }
    
    private void FixedUpdate()
    {
        MovePlayer();        
        RotatePlayerBasedOnMouse();
    }
}