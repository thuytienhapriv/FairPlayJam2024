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
        if (player.GetComponent<PlayerMovement2>().holdsUmbrella == false) { return; }
        fillBar.GetComponent<FillBarScript>().FillWithWater(rainColor.ToString());
    }


}
