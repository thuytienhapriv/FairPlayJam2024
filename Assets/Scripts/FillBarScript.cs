using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarScript : MonoBehaviour
{
    GameObject rainArea;
    public string myColor;
    [SerializeField] float changeRate;
    [SerializeField] GameObject player;

    Color32 myRed;
    Color32 myBlue;

    private void Start()
    {
        gameObject.GetComponent<Image>().fillAmount = 0 ;
        myRed = new Color32(219,51,74,255 );
        myBlue = new Color32(69,135,209,255);
    }

    public void FillWithWater(string rainColor)
    {
        gameObject.GetComponent<Image>().fillAmount += changeRate;
        player.GetComponent<PlayerMovement2>().waterVolume += changeRate;

        if (rainColor == "blue") 
        { 
            gameObject.GetComponent<Image>().color = myBlue;
            myColor = "blue";

        } 
        else if (rainColor == "red") 
        { 
            gameObject.GetComponent<Image>().color = myRed; 
            myColor = "red"; 
        }

        player.GetComponent<PlayerMovement2>().SetColour(myColor);
    }

    public void PourOutWater(float pourSpeed)
    {
        gameObject.GetComponent<Image>().fillAmount -= pourSpeed;
    }
}
