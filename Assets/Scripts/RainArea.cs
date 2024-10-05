using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainArea : MonoBehaviour
{
    public enum colors { red, blue };
    public colors rainColor;

    [SerializeField] private GameObject FillBar;

    private void OnTriggerStay2D(Collider2D collision)
    {
        FillBar.GetComponent<FillBarScript>().FillWithWater(rainColor.ToString());
    }


}
