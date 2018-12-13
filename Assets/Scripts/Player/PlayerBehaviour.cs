using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviour: FireTimer
{

    CharacterController controller;
    public float speed;
    Ray ray;
    RaycastHit hit;
    int frame = 0;
    public Text healthNum;
    public GameObject youLose;

    int currentHealth;
    public int maxHealth = 10;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        TakeDamage(0);
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
            controller.Move(Vector3.forward * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.A))
            controller.Move(Vector3.left * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.S))
            controller.Move(Vector3.back * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.D))
            controller.Move(Vector3.right * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }
            

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

    public void TakeDamage(int amount)
    {

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            youLose.SetActive(true);
            Time.timeScale = 0;
        }
            

        healthNum.text = currentHealth.ToString();

    }



}  
