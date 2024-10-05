using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Umbrella : MonoBehaviour
{
    private bool isHeld;
    [SerializeField] GameObject player;
    private Vector2 distance;

    void Start()
    {
        player = gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        isHeld = player.GetComponent<PlayerMovement>().isHolding;

    }

    public void Position()
    {
        if (isHeld) {
            transform.RotateAround(player.transform.position, Vector3.forward, 90);
        }
        else
        {
            transform.RotateAround(player.transform.position, Vector3.forward, -90);
        
        }
    }
}
