using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Parallax : MonoBehaviour
{
    public float parallaxEffectMultiplier = 0f;
    public float minInterval;
    public float maxInterval;
    private int offsetX = 2;
    private int offsetY = 0; //vertical

    [HideInInspector] public bool hasRightTile = false;
    [HideInInspector] public bool hasLeftTile = false;
    [HideInInspector] public bool hasAboveTile = false; //vertical
    [HideInInspector] public bool hasBelowTile = false; //vertical

    public Sprite[] alternateSprites;

    private float spriteWidth = 0f;
    private float spriteHeight = 0f; //vertical
    private int nextSprite;
    private float nextSpriteWidth;
    private float nextSpriteHeight; //vertical

    private Camera cam;
    private ParallaxCamera parallaxCamera;
    private Vector3 lastCameraPosition;
    private Transform myTransform;
    [HideInInspector] public Transform myRightTile;
    [HideInInspector] public Transform myLeftTile;
    [HideInInspector] public Transform myAboveTile; //vertical
    [HideInInspector] public Transform myBelowTile; //vertical

    void Awake()
    {
        cam = Camera.main;
        lastCameraPosition = cam.transform.position;
        myTransform = transform;

    }

    // Use this for initialization
    void Start()
    {

        parallaxCamera = cam.GetComponent<ParallaxCamera>();

        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
        spriteHeight = sRenderer.sprite.bounds.size.y; //vertical

        nextSpriteWidth = spriteWidth;
        nextSpriteHeight = spriteHeight;




    }

    // Update is called once per frame
    void Update()
    {

        if (parallaxCamera.scrollAxis == ParallaxCamera.ScrollAxis.vertical && parallaxCamera != null)
        {
            if (hasBelowTile == false || hasAboveTile == false)
            {

                float camVerticalExtend = cam.orthographicSize;


                float edgeVisiblePositionAbove = (myTransform.position.y + spriteHeight / 2) - camVerticalExtend;
                float edgeVisiblePositionBelow = (myTransform.position.y - spriteHeight / 2) + camVerticalExtend;


                if (cam.transform.position.y >= edgeVisiblePositionAbove - offsetY && hasAboveTile == false)
                {
                    InstantiateNewTileVertical(1);
                    hasAboveTile = true;
                }
                else if (cam.transform.position.y <= edgeVisiblePositionBelow + offsetY && hasBelowTile == false)
                {
                    InstantiateNewTileVertical(-1);
                    hasBelowTile = true;
                }
            }
        }
        else if (parallaxCamera.scrollAxis == ParallaxCamera.ScrollAxis.horizontal || parallaxCamera == null)
        {

            if (hasLeftTile == false || hasRightTile == false)
            {

                float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;


                float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
                float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;


                if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasRightTile == false)
                {

                    InstantiateNewTileHorizontal(1);
                    hasRightTile = true;
                }
                else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasLeftTile == false)
                {
                    InstantiateNewTileHorizontal(-1);
                    hasLeftTile = true;
                }
            }
        }

    }

    void FixedUpdate()
    {
        Vector3 cameraMovement = cam.transform.position - lastCameraPosition;

        transform.position += cameraMovement * parallaxEffectMultiplier;

        lastCameraPosition = cam.transform.position;

        float cameraHeight = 2 * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;



        if (parallaxCamera.scrollAxis == ParallaxCamera.ScrollAxis.vertical && parallaxCamera != null)
        {
            if (Mathf.Abs(cam.transform.position.y - transform.position.y) >= cameraHeight * 2)
            {

                if (myAboveTile != null)
                {
                    myAboveTile.GetComponent<Parallax>().hasBelowTile = false;
                    myAboveTile.GetComponent<Parallax>().myBelowTile = null;
                }
                if (myBelowTile != null)
                {
                    myBelowTile.GetComponent<Parallax>().hasAboveTile = false;
                    myBelowTile.GetComponent<Parallax>().myAboveTile = null;
                }
                Destroy(this.gameObject);

            }
        }
        else if (parallaxCamera.scrollAxis == ParallaxCamera.ScrollAxis.horizontal || parallaxCamera == null)
        {
            if (Mathf.Abs(cam.transform.position.x - transform.position.x) >= cameraWidth * 2)
            {

                if (myRightTile != null)
                {
                    myRightTile.GetComponent<Parallax>().hasLeftTile = false;
                    myRightTile.GetComponent<Parallax>().myLeftTile = null;
                }
                if (myLeftTile != null)
                {
                    myLeftTile.GetComponent<Parallax>().hasRightTile = false;
                    myLeftTile.GetComponent<Parallax>().myRightTile = null;
                }
                Destroy(this.gameObject);

            }

        }



    }

    void InstantiateNewTileHorizontal(int rightOrLeft)
    {

        if (alternateSprites.Length != 0)
        {
            nextSprite = Random.Range(0, alternateSprites.Length);
            nextSpriteWidth = alternateSprites[nextSprite].bounds.size.x;

        }


        float getRandom = Random.Range(minInterval, maxInterval);

        Vector3 newPosition = new Vector3(myTransform.position.x + (((nextSpriteWidth + spriteWidth) / 2) + Mathf.Abs(getRandom)) * rightOrLeft, myTransform.position.y, myTransform.position.z);

        Transform newTile = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if (alternateSprites.Length != 0)
        {
            newTile.GetComponent<SpriteRenderer>().sprite = alternateSprites[nextSprite];

        }


        newTile.parent = myTransform.parent;
        if (rightOrLeft > 0)
        {
            myRightTile = newTile;
            myRightTile.GetComponent<Parallax>().hasLeftTile = true;
            myRightTile.GetComponent<Parallax>().myLeftTile = transform;

        }
        else
        {
            myLeftTile = newTile;
            myLeftTile.GetComponent<Parallax>().hasRightTile = true;
            myLeftTile.GetComponent<Parallax>().myRightTile = transform;
        }
    }
    void InstantiateNewTileVertical(int aboveOrBelow)
    {
        if (alternateSprites.Length != 0)
        {
            nextSprite = Random.Range(0, alternateSprites.Length);
            nextSpriteHeight = alternateSprites[nextSprite].bounds.size.y; //vertical
        }

        float getRandom = Random.Range(minInterval, maxInterval);

        Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + (((nextSpriteHeight + spriteHeight) / 2) + Mathf.Abs(getRandom)) * aboveOrBelow, myTransform.position.z);

        Transform newTile = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if (alternateSprites.Length != 0)
        {
            newTile.GetComponent<SpriteRenderer>().sprite = alternateSprites[nextSprite];

        }


        newTile.parent = myTransform.parent;
        if (aboveOrBelow > 0)
        {
            myAboveTile = newTile;
            myAboveTile.GetComponent<Parallax>().hasBelowTile = true;
            myAboveTile.GetComponent<Parallax>().myBelowTile = transform;

        }
        else
        {
            myBelowTile = newTile;
            myBelowTile.GetComponent<Parallax>().hasAboveTile = true;
            myBelowTile.GetComponent<Parallax>().myAboveTile = transform;
        }
    }
}
