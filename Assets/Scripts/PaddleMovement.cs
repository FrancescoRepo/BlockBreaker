using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float screenWidthInUnit = 16f;

    [SerializeField] private float minX;

    [SerializeField] private float maxX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = (Input.mousePosition.x / Screen.width * screenWidthInUnit);
        float y = transform.position.y;
        Vector2 pos = new Vector2(Mathf.Clamp(x, minX, maxX), y);

        transform.position = pos;
    }
}
