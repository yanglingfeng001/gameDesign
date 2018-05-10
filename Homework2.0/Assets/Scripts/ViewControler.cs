using UnityEngine;
using System.Collections;

public class ViewControler : MonoBehaviour {

    public float speed=1;
    public float mouseSpeed = 60;
	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h*speed, mouse*mouseSpeed, v*speed)*Time.deltaTime,Space.World);

      


	}
}
