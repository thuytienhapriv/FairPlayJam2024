using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;
using UnityEngine.Windows.WebCam;
using System;
using UnityEditor;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float maxOffset;
    [SerializeField] private float offsetCamera;

    private Vector2 playerDir;
    private Vector2 targetCamera;
    private float lookAhead;
    private float originalZ;

    public CinemachineVirtualCamera cam;
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTime = 0.2f;

    private void Start()
    {
        playerDir = player.GetComponent<PlayerMovement2>().direction;
        ControlShake(0);
        originalZ = transform.position.z;
        Time.timeScale = 1;
    }

    public void ControlShake(float intensity)
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
    }

    public void CameraShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        ControlShake(shakeIntensity);
        yield return new WaitForSeconds(shakeTime);
        ControlShake(0);
    }


    void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.Space)) { CameraShake(); }


        /*CinemachineFramingTransposer transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();

        transposer.m_BiasX = Mathf.Lerp(-maxOffset, maxOffset, Time.deltaTime * Mathf.Sign(playerDir.x));*/
        ////transform.position = new Vector3(player.transform.position.x + lookAhead * Mathf.Sign(playerDir.x), transform.position.y, transform.position.z);




        /*playerDir = player.GetComponent<PlayerMovement2>().direction;

        if (playerDir.x >= 0)
        {
            targetCamera = new Vector3(player.transform.position.x + lookAhead, transform.position.y, transform.position.z);
        } else
        {
            targetCamera = new Vector3(player.transform.position.x - lookAhead, transform.position.y, transform.position.z);
        }


        transform.position = Vector3.Lerp(transform.position, targetCamera, Time.deltaTime * offsetCamera);*/






        //transform.position.z = originalZ;
        //lookAhead = Mathf.Lerp(lookAhead * Mathf.Sign(playerDir.x), (aheadDistance * player.transform.localScale.x) * Mathf.Sign(playerDir.x), Time.deltaTime * cameraSpeed);
        


        //Debug.Log(player.transform.localScale.x);
        //Debug.Log(player.transform.position.x + " " + player.transform.position.x + lookAhead * Mathf.Sign(playerDir.x));




        //cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_BiasX = Mathf.Lerp(-maxOffset, maxOffset, Mathf.Sign(playerDir.x) * 100 * Time.deltaTime);
        //cam.GetCinemachineComponent<CinemachineFramingTransposer>().
        //Debug.Log(cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_BiasX + " / " + Mathf.Sign(playerDir.x) * 100 * Time.deltaTime);



        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed)

        /*transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(targetCamera, 0.2f);
    }

}
