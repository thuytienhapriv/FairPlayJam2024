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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((poolColor == colors.red && player.GetComponent<PlayerMovement2>().fillColour.ToString() == "blue") || (poolColor == colors.blue && player.GetComponent<PlayerMovement2>().fillColour.ToString() == "red"))
        {
            isPurple = true;
            poolLight.SetActive(true);
            lastBarrier.GetComponent<LastBarrier>().AddPool();
        }
    }
}
