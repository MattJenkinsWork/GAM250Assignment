using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviour: FireTimer
{
    public float speed;

    CharacterController controller;
    Ray ray;
    RaycastHit hit;

    public Text healthNumText;
    public GameObject youLoseText;

    int frame = 0;

    //Health variables
    int currentHealth;
    public int maxHealth;


    private void Awake()
    {        
        controller = GetComponent<CharacterController>();

        //Setting up health and updating the UI using TakeDamage()
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
        //Move the character controller if WASD is pressed
        if (Input.GetKey(KeyCode.W))
            controller.Move(Vector3.forward * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.A))
            controller.Move(Vector3.left * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.S))
            controller.Move(Vector3.back * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.D))
            controller.Move(Vector3.right * Time.deltaTime * speed);

        //Restart the scene
        if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }
            

    }

    //Triggers LookAtCursor when frame is even
    //Prevents a raycast being triggered every frame, thus is a small optimisation
    void CursorFrameCheck()
    {
        if (frame % 2 != 0)
            LookAtCursor();

        frame++;

        if (frame > 10)
            frame = 0;
    }

    //Make the player turn towards the point the ray hit
    void LookAtCursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if the ray hit something, get it's hit location and look at it
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Barrier")
            {
                //Store the player's rotation so it can be readded back
                //This prevents the player from looking up or down
                Quaternion rotStore = transform.rotation;
                transform.LookAt(hit.point);
                transform.SetPositionAndRotation(transform.position, new Quaternion(rotStore.x, transform.rotation.y, rotStore.z, transform.rotation.w));
            }

        }
    }


    //Called when the player is hit by a projectile
    //Updates the health as well as killing the player
    public void TakeDamage(int amount)
    {

        currentHealth -= amount;

        //Kill the player
        if (currentHealth <= 0)
        {
            youLoseText.SetActive(true);
            Time.timeScale = 0;
        }
            

        healthNumText.text = currentHealth.ToString();

    }



}  
