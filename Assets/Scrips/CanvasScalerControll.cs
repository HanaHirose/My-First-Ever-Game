using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerControll : MonoBehaviour
{
    private UnityEngine.UI.CanvasScaler cs;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Canvas Scaler Controll");
        cs = GetComponent<UnityEngine.UI.CanvasScaler>();
        cs.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("cs.uiScaleMode: " + cs.uiScaleMode);
    }
}
