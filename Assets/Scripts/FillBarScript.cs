using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarScript : MonoBehaviour
{
    GameObject rainArea;
    [SerializeField] float changeRate;
    public float colorMeter; // 1 - 33 blue, 34 - 66 purple, 67 - 99 red
    Color32 myRed;
    Color32 myBlue;

    private void Start()
    {
        gameObject.GetComponent<Image>().fillAmount = 0 ;
        colorMeter = 50;
        myRed = new Color32(219,51,74,255 );
        myBlue = new Color32(69,135,209,255);
    }

    public void FillWithWater(string rainColor)
    {
        gameObject.GetComponent<Image>().fillAmount += changeRate;

        if (rainColor == "blue") { gameObject.GetComponent<Image>().color = myBlue; }
        else if (rainColor == "red") { gameObject.GetComponent<Image>().color = myRed; }
    }

    public void PourOutWater(string rainColor)
    {
        gameObject.GetComponent<Image>().fillAmount -= changeRate;
    }

    private void Update()
    {/*
        colorMeter = Mathf.Clamp(colorMeter, 0, 99);

        Debug.Log(colorMeter / 99 * Time.deltaTime);

        ///gameObject.GetComponent<Image>().color = Color.Lerp(myBlue, myRed, colorMeter/99);
        
        // color lerp ifs

            *//*if (colorMeter <= 33) { gameObject.GetComponent<Image>().color = Color.Lerp(myBlue, myPurple, colorMeter /99); }
            else if (colorMeter > 33 && colorMeter <= 66) { gameObject.GetComponent<Image>().color = myPurple; }
            else if (colorMeter > 66) { gameObject.GetComponent<Image>().color = myRed; }
        */
    }
}
