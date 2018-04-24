using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private Transform[] positions;
    public float hp =150;
    private float totalHp;
    public GameObject explosioneffect;
    private int index = 0;
    public float speed = 10;
    private Slider hpSlider;

	// Use this for initialization
	void Start () {
        positions = WayPoints.position;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}

    void move() {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f) {
            index++;
        }
        if(index>positions.Length-1){
            ReachDestination();
        }
    }

    //到达终点
    void ReachDestination() {
        GameManager.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy() {
        EnemySpawner.CountEnemyAlive--; 
    }

    public void TakeDamage(float damage) {
        if (hp <= 0) return;
        hp-=damage;
        hpSlider.value = (float)hp / totalHp;
        if (hp <= 0) {
            Die();
        }
    }

    void Die() {
        GameObject effect=(GameObject)GameObject.Instantiate(explosioneffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f );
        Destroy(this.gameObject);
    }
}
