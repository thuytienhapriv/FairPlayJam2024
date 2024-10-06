using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject poolLight;
    [SerializeField] GameObject lastBarrier;

    public enum colors { red, blue };
    public colors poolColor;

    public bool isPurple;


    private void Start()
    {
        isPurple = false;
        poolLight.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((poolColor == colors.red && player.GetComponent<PlayerMovement2>().fillColour.ToString() == "blue") || (poolColor == colors.blue && player.GetComponent<PlayerMovement2>().fillColour.ToString() == "red"))
        {
            if (player.GetComponent<PlayerMovement2>().isPouring == true)
            {
                isPurple = true;
                poolLight.SetActive(true);
                lastBarrier.GetComponent<LastBarrier>().AddPool();
            } 
        }
    }
}
