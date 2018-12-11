using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviour: FireTimer
{

    CharacterController controller;
    public float speed;
    Ray ray;
    RaycastHit hit;
    int frame = 0;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        DoMovement();

        CursorFrameCheck();
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireCheck();
        }


    }

    void DoMovement()
    {
        if (Input.GetKey(KeyCode.W))
            controller.Move(new Vector3(0, 0, 1) * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.A))
            controller.Move(new Vector3(-1, 0, 0) * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.S))
            controller.Move(new Vector3(0, 0, -1) * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.D))
            controller.Move(new Vector3(1, 0, 0) * Time.deltaTime * speed);

    }

    void CursorFrameCheck()
    {
        if (frame % 2 != 0)
            LookAtCursor();

        frame++;

        if (frame > 10)
            frame = 0;
    }


    void LookAtCursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Barrier")
            {
                Quaternion rotStore = transform.rotation;
                transform.LookAt(hit.point);
                transform.SetPositionAndRotation(transform.position, new Quaternion(rotStore.x, transform.rotation.y, rotStore.z, transform.rotation.w));
            }

        }
    }

}  
