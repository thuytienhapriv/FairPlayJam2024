using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarScript : MonoBehaviour
{
    GameObject rainArea;
    [SerializeField] float changeRate;
    Color32 myRed;
    Color32 myBlue;
    public string myColor;

    private void Start()
    {
        gameObject.GetComponent<Image>().fillAmount = 0 ;
        myRed = new Color32(219,51,74,255 );
        myBlue = new Color32(69,135,209,255);
    }

    public void FillWithWater(string rainColor)
    {
        gameObject.GetComponent<Image>().fillAmount += changeRate;

        if (rainColor == "blue") { gameObject.GetComponent<Image>().color = myBlue; myColor = "blue"; }
        else if (rainColor == "red") { gameObject.GetComponent<Image>().color = myRed; myColor = "red"; }
    }

    public void PourOutWater()
    {
        gameObject.GetComponent<Image>().fillAmount -= changeRate;
    }
}
