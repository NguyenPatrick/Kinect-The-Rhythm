using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graphs : MonoBehaviour {
    public RawImage img;
    public string url = "https://chart.googleapis.com/chart?cht=lc&chs=250x150&chxt=y&chg=10,10&chtt=Accuracy&chd=t:" + "1,3,5,6,87,13,16,45,47,48";

    void Awake()
    {
        img = this.gameObject.GetComponent<RawImage>();
    }


    IEnumerator Start() {

        WWW www = new WWW(url);
        yield return www;
        img.texture = www.texture;
    
        img.SetNativeSize();
    }
    void Update()
    {

    }
}

