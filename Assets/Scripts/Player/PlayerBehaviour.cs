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



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        DoMovement();

        LookAtCursor();


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




    void LookAtCursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                Quaternion rotStore = transform.rotation;
                transform.LookAt(hit.point);
                transform.SetPositionAndRotation(transform.position, new Quaternion(rotStore.x, transform.rotation.y, rotStore.z, transform.rotation.w));
            }

        }
    }

}  
