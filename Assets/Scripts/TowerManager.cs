using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    TowerBtn towerBtnPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            Vector3 adjustZ = new Vector3(mousePoint.x, mousePoint.y, towerBtnPressed.TowerObject.transform.position.z);

            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector3.zero);
            if(hit.collider.tag == "TowerSide")
            {
                hit.collider.tag = "TowerSideFull";
                PlaceTower(adjustZ, hit);
            }
        }
    }

    public void PlaceTower(Vector3 adjustZ, RaycastHit2D hit)
    {
        if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            Instantiate(towerBtnPressed.TowerObject).transform.position = hit.transform.position;
            Debug.Log("object created");
        }
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        towerBtnPressed = towerSelected;
        Debug.Log("Pressed" + towerBtnPressed.gameObject);
    }
}
