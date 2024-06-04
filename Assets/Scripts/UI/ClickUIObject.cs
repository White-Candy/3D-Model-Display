using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickUIObject : MonoBehaviour
{

    public GameObject ClickObject()
    {
        PointerEventData eventDataCurrPos = new PointerEventData(EventSystem.current);
        eventDataCurrPos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrPos, result);
        if (result.Count > 0)
        {
            return result[0].gameObject;
        }
        else
        {
            return null;
        }
    }
}
