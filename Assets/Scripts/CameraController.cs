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

    private float lastXPos;

    // Start is called before the first frame update
    void Start()
    {
        lastXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float campedY = Mathf.Clamp(target.position.y, minHeight, maxHeight);
        transform.position = new Vector3(target.position.x, campedY, transform.position.z);

        float amountToMoveX = transform.position.x - lastXPos;
        lastXPos = transform.position.x;

        farBackground.position += new Vector3(amountToMoveX, 0f, 0f);
        middleBackground.position += new Vector3(amountToMoveX * 0.5f, 0f, 0f);
    }
}
