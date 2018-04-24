using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {

    [HideInInspector]
    public GameObject turretGo;//保存当前cube上的炮台
    [HideInInspector]
    public TurretData turretData;
    public GameObject BuildEffect;
    private Renderer renderer;
    [HideInInspector]
    public bool isUpgraded = false;

    void Start() {
        renderer = GetComponent<Renderer>();
    }



    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        Vector3 pos= transform.position;
        pos.x += 8;
        turretGo = (GameObject)GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = (GameObject)GameObject.Instantiate(BuildEffect, pos, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void UpgradeTurret()
    {
        if (isUpgraded == true) return;

        Destroy(turretGo);
        isUpgraded = true;
        Vector3 pos = transform.position;
        pos.x += 8;
        turretGo = (GameObject)GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effect = (GameObject)GameObject.Instantiate(BuildEffect, pos, Quaternion.identity);
        Destroy(effect, 1.5f);
        
    }

    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData = null;
        Vector3 pos = transform.position;
        pos.x += 8;
        GameObject effect = (GameObject)GameObject.Instantiate(BuildEffect, pos, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    void OnMouseEnter() {
         
        if (turretGo == null&&EventSystem.current.IsPointerOverGameObject()==false) {
            renderer.material.color = Color.red;
        }
    }

    void OnMouseExit() {
        renderer.material.color = Color.white;
    }
	
}
