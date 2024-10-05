using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float maxOffset;
    private Vector2 playerDir;
    private Vector2 targetCamera;
    private float lookAhead;

    public CinemachineVirtualCamera cam;
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTime = 0.2f;

    private void Start()
    {
        playerDir = player.GetComponent<PlayerMovement2>().direction;
        ControlShake(0);
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
        targetCamera = new Vector2(player.transform.position.x + playerDir.x * lookAhead, transform.position.y);
        
        if (Input.GetKeyDown(KeyCode.Space)) { CameraShake(); }
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed)

        /*transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);*/
    }
}
