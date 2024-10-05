using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    public enum ScrollAxis { horizontal, vertical }
    public ScrollAxis scrollAxis;
    public bool autoScroll = false;
    public float speed = 0f;

    void FixedUpdate()
    {
        if (autoScroll)
        {
            if (scrollAxis == ScrollAxis.horizontal)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (scrollAxis == ScrollAxis.vertical)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }


    }
}
