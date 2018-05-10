using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public GameObject EndUI;
    public Text endMessage;

    public static GameManager Instance;
    private EnemySpawner enemyspawner;

    void Awake() {
        Instance = this;
        enemyspawner = GetComponent<EnemySpawner>();
    }

    public void Win() {
        EndUI.SetActive(true);
        endMessage.text = "胜 利";
    }

     public void Failed() {
         enemyspawner.stop(); 
        EndUI.SetActive(true);
        endMessage.text = "失 败";

    }

     public void OnButtonRetry() {
         Application.LoadLevel("MainScenes");
     }

     public void OnButtonMenu() {
         Application.LoadLevel("MainMenu");         
     }
}
