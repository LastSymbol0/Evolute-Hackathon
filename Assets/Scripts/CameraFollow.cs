using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 Offset;

    private Vector3 Change;

    public float Speed = 0.4f;

    Camera cam;

    public float MaxZoom = 6f;
    public float MinZoom = 3f;

    public float ZoomSpeed = 1f;

    public float ZoomController = 2f;

    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Zoom();
    }

    void Zoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, GetZoom(), ZoomSpeed);
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinZoom, MaxZoom);
    }

    float GetZoom()
    {
        MassSpawner ms = MassSpawner.instance;
        Bounds bounds = new Bounds(ms.Players[0].transform.position, Vector3.zero);

        for (int i = 0; i < ms.Players.Count; i++)
        {
            bounds.Encapsulate(ms.Players[i].transform.position);
        }

        return (bounds.size.x + bounds.size.y) / ZoomController;
    }

    void Move()
    {
        Vector3 position = GetCenter() + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, position, ref Change, Speed);
    }

    Vector3 GetCenter()
    {
        MassSpawner ms = MassSpawner.instance;
        Bounds bounds = new Bounds(ms.Players[0].transform.position, Vector3.zero);

        for (int i = 0; i < ms.Players.Count; i++)
        {
            bounds.Encapsulate(ms.Players[i].transform.position);
        }

        return bounds.center;
    }
}
