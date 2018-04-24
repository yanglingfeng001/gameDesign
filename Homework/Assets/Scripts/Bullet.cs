using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage = 50;

    public float speed = 20;

    private Transform target;

    public GameObject explosionEffectPrefab;

    private float distanceArriveTarget = 1.2f;

    public void setTarget(Transform _target){
        this.target = _target;
    }

    void Update() { 

        if(target==null){
            Die();
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
       
    }


    //void OnTriggerEnter(Collider col) {
    //    if (col.tag == "Enemy") {
    //        col.GetComponent<Enemy>().TakeDamage(damage);
    //        Die();
    //    }
    //}

    void Die() {
        GameObject effect = (GameObject)GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
