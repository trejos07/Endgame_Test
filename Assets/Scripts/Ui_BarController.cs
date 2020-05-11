using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_BarController : MonoBehaviour
{
    Image fill_Image;

    private void Awake(){
       fill_Image = transform.Find("Fill").GetComponent<Image>();
       transform.parent.GetComponentInParent<Character>().OnHealthChange += UpdateBar; //Herachy/enemmy/canvas/this
    }

    void UpdateBar(float value){
        if(fill_Image)
            fill_Image.fillAmount = value;
    }


}
