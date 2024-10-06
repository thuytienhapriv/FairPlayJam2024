using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBarrier : MonoBehaviour
{
    [SerializeField] private GameObject[] pools;

    private float poolCount;
    private bool poolsAreReady;

    private void Start()
    {
        poolCount = 0;
        poolsAreReady = false;
        gameObject.GetComponent<Collider2D>().isTrigger = false;

    }

    public void AddPool()
    {
        poolCount++;
        if (poolCount == 5)
        {
            poolsAreReady = true;
        }
    }

    private void Update()
    {
        if (poolsAreReady)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true; 
        }

    }




}
