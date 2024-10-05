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

    private void Start()
    {
        playerDir = player.GetComponent<PlayerMovement2>().direction;
    }

    void Update()
    {
        targetCamera = new Vector2(player.transform.position.x + playerDir.x * lookAhead, transform.position.y);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed)

        /*transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);*/
    }
}
