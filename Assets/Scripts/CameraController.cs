using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Transform farBackground;
    public Transform middleBackground;

    public float minHeight = -1.5f;
    public float maxHeight = 2.5f;

    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float campedY = Mathf.Clamp(target.position.y, minHeight, maxHeight);
        transform.position = new Vector3(target.position.x, campedY, transform.position.z);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
        lastPos = transform.position;

        farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
    }
}
