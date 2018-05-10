using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public static int CountEnemyAlive = 0;
    public Wave[] waves;
    public Transform start;
    public float waveRate = 0.3f;
    private Coroutine coroutine;

    void Start() {
        coroutine= StartCoroutine(SpawnEnemy());
    }

    public void stop() {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnEnemy() {
        foreach (Wave wave in waves) {
            for (int i = 0; i < wave.count; i++) {
                GameObject.Instantiate(wave.EnemyPrefab, start.position,Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1) {
                    yield return new WaitForSeconds(wave.rate);
                }
               
            }
            while (CountEnemyAlive > 0) {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0) {
            yield return 0;
        }
        GameManager.Instance.Win();
    }
}
