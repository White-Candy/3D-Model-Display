using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSet : MonoBehaviour
{
    private string target_name = "target";
    private string drag_name;
    private GameObject instance;
    public float distance = 6f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateWithCusorObj("cube", "Prefabs/");
        }
        WithOjbOperation();
    }

    /// <summary>
    /// 创建跟随鼠标拖动的物体
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="path"></param>
    private void CreateWithCusorObj(string tagName, string path)
    {
        GameObject clickGameObject = transform.GetComponent<ClickUIObject>().ClickObject();
        if (clickGameObject != null)
        {
            //Debug.Log(clickGameObject.name);
            if (clickGameObject.tag == tagName)
            {
                //Debug.Log(clickGameObject.tag + " equal!");
                string name = clickGameObject.name;
                instance = (GameObject)Instantiate(Resources.Load(path + name));
            }
            else
            {
                //Debug.Log(clickGameObject.tag + " not equal!");
            }
        }
    }

    /// <summary>
    /// 物体跟随
    /// </summary>
    private void WithOjbOperation()
    {
        if (instance != null)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            instance.transform.position = mouseWorldPos;
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Dragging..." + instance.name);
                instance.transform.GetComponent<BoxCollider>().enabled = false;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (target_name == hit.collider.name)
                    {
                        if (string.IsNullOrEmpty(drag_name) && GlobalParams.NeedTools.Count > 0)
                        {
                            drag_name = GlobalParams.NeedTools.Dequeue();
                        }
                        Debug.Log(drag_name + " || " + instance.tag);
                        if (instance != null && drag_name == instance.tag)
                        {
                            GlobalParams.NeedCnt--;
                            drag_name = "";
                            //Anim Begin
                        }
                    }
                }
                Destroy(instance);
            }
        }
    }
}
