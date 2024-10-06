using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainArea : MonoBehaviour
{
    public enum colors { red, blue };
    public colors rainColor;

    [SerializeField] private GameObject fillBar;
    [SerializeField] private GameObject player;


    private void OnTriggerStay2D(Collider2D collision)
    {
        // if holds an umbrella, can't fill water
        if (player.GetComponent<PlayerMovement2>().holdsUmbrella == true) { return; } 
        
        fillBar.GetComponent<FillBarScript>().FillWithWater(rainColor.ToString());
    }


}
