using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintNumber : MonoBehaviour
{
    public Text Hint;

    void Start()
    {
     
    }

    void Update()
    {
        if (Hint != null && GlobalParams.NeedTools.Count != 0)
        {
            Hint.gameObject.SetActive(true);
            Hint.text = GlobalParams.NeedCnt.ToString();
        }
        else
        {
            Hint.gameObject.SetActive(false);
            //Hint.text = GlobalParams.NeedCnt.ToString();
        }
    }
}
