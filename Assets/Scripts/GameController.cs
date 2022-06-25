using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // GUAR O PLAYER PRA SABER QUANDO ELE FOI ATINGIDO POR UM ZOMBIE
    public GameObject player;
    
    // GUI text
    public GameObject gameOverText;
    
    // Start is called before the first frame update
    void Start()
    {
        // coloca o tempo ao normal novamente para que n fique == 0 quando se reinicia o jogo, pois a scene é congelada ao player ser atingido
        Time.timeScale = 1; 
        
        // tira a mensagem da tela pois ela fica setada como true quando o player morre
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // checa se est morto e reseta o game, é setada como falsa quando o zumbie o atinge
        if (player.GetComponent<PlayerController>().isPlayerAlive == false)
        {
            // trava o jogo
            Time.timeScale = 0; // 0 == para o jogo, 1 == velocidade normal, 2 == jogo 2x mais rapido
            
            // motra a mensagem na tela
            gameOverText.SetActive(true);
            
            // se apertar o botao do mouse reseta
            if (Input.GetButtonDown("Fire1"))
            {
                // carrega a cena dada no parametro
                SceneManager.LoadScene("MainScene");
            }
        }
    }
    
}
