using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public enum colors { red, blue };
    public colors rainColor;

    [SerializeField] private GameObject fillBar;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
