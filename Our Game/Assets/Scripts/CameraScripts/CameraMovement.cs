using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [Header("Move Var")]
    public float speed;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, speed * Time.deltaTime);
    }
}
