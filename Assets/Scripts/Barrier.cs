using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public enum colors { red, blue };
    public colors rainColor;

    [SerializeField] private GameObject fillBar;
    [SerializeField] private GameObject player;

    void FixedUpdate()
    {
        if (fillBar.GetComponent<FillBarScript>().myColor == rainColor.ToString()) // if the color is the same
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;

        } else {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
