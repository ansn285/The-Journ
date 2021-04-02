using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 1.5f;
    public float leftLimit;
    public float rightLimit;
    private Vector3 destination;

    private void Start()
    {
        destination = new Vector3(player.position.x + 21, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (player.transform.position.x > leftLimit && player.transform.position.x < rightLimit)
        {
            Vector3 pos = transform.position;
            destination = new Vector3(player.position.x + 21, transform.position.y, transform.position.z);
            pos.x = Mathf.Clamp(transform.position.x, -30, 30);
            transform.position = Vector3.Lerp(transform.position, destination, followSpeed * Time.deltaTime);
        }
    }

    /*private static CameraFollow _instance;
    public static CameraFollow instance;

    public GameObject player;

    Vector2 currMinBounds;
    Vector2 currMaxBounds;
    Vector3 targetPos;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        instance = _instance;
    }

    private void Start()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        SetTargetPos();
        transform.position = targetPos;
    }

    public void SetCamBounds(Vector2 minBounds, Vector2 maxBounds)
    {
        currMinBounds = minBounds;
        currMaxBounds = maxBounds;
    }

    private void SetTargetPos()
    {
        targetPos.x = player.transform.position.x;
        targetPos.y = player.transform.position.y;
        TestOutOfCamBounds();
    }

    private void TestOutOfCamBounds()
    {
        if (targetPos.x <= currMinBounds.x) { targetPos.x = currMinBounds.x; }
        if (targetPos.x >= currMaxBounds.x) { targetPos.x = currMaxBounds.x; }
        if (targetPos.y <= currMinBounds.y) { targetPos.y = currMinBounds.y; }
        if (targetPos.y >= currMaxBounds.y) { targetPos.y = currMaxBounds.y; }
    }*/
}
