using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManger : MonoBehaviour {

    public TurretData laserturretData;
    public TurretData misileTurretData;
    public TurretData standardTurretData;

    private MapCube selectedMapcube;

    //当前选择的炮台
    private TurretData selectTurretData;

    private int money = 1000;

    public GameObject upgradeCanvas;
    public GameObject buttonUpgrade;

    //public Animator upgradeCavansAnimator;

    public Text moneyText;

    public Animator moneyAnimator;

    void ChangeMoney(int change=0) {
        money += change;
        moneyText.text = "$" + money;
    }

    void Start() {
       // upgradeCavansAnimator = upgradeCanvas.GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) { 
            if(EventSystem.current.IsPointerOverGameObject()==false){
                //炮台
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));

                if (isCollider) {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//得到点击的mapcube
                    if (selectTurretData != null && mapCube.turretGo == null)
                    {
                            //可以创建
                            if(money>selectTurretData.cost){
                                ChangeMoney(-selectTurretData.cost);
                                mapCube.BuildTurret(selectTurretData);
                                
                            }else{
                                //钱不够
                                moneyAnimator.SetTrigger("flicker");
                            }
                        }else if(mapCube.turretGo!=null){
                            //升级
                            if (mapCube == selectedMapcube && upgradeCanvas.activeInHierarchy)
                            {
                                //StartCoroutine(hideUpgradeUI());
                                hideUpgradeUI();
                            }
                            else {
                                showUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                            }
                            selectedMapcube  = mapCube;
                        }
                }
            }
        }
    }

    public void onlaserselected(bool isOn) {
        if (isOn) {
            selectTurretData = laserturretData;
        }
    }

    public void onMislieSelected(bool isOn) {
        if (isOn) {
            selectTurretData = misileTurretData;
        }
    }

    public void onStandardSelected(bool isOn) { 
        if(isOn){
            selectTurretData=standardTurretData;
        }
    }

    void showUpgradeUI(Vector3 pos, bool isDisableUpgrade=false) {
        //StopCoroutine("hideUpgradeUI");
        //upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.SetActive(!isDisableUpgrade);
        
    }


    //IEnumerator hideUpgradeUI() {
    //    upgradeCavansAnimator.SetTrigger("Hide");

    //    //upgradeCanvas.SetActive(false);
    //    yield return new WaitForSeconds(0.3f);
    //    upgradeCanvas.SetActive(false);
    //}

    void hideUpgradeUI()
    {
        //upgradeCavansAnimator.SetTrigger("Hide");

        //upgradeCanvas.SetActive(false);
        //yield return new WaitForSeconds(0.3f);
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown() {
        if (money >= selectedMapcube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapcube.turretData.costUpgraded);
        }
        else {
            moneyAnimator.SetTrigger("flicker"); 
        }

        selectedMapcube.UpgradeTurret();
        hideUpgradeUI();
    }

    public void OnDestroyButtonDown() {
        selectedMapcube.DestroyTurret();
        hideUpgradeUI();
    }

}
