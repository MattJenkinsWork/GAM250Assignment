using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    //All of the cutscene dialogue
    public string[] dialogue;

    int currentLine = 0;

    public Text textbox;
    public GameObject textboxImage;

    private void Start()
    {
        UpdateLine();
    }


    // Update is called once per frame
    void Update () {
		
        if (Input.anyKeyDown)
        {
            currentLine++;

            //Set the image to active if the current line isn't the first one
            textboxImage.SetActive(currentLine >= 1);

            if (currentLine >= dialogue.Length)
                SceneManager.LoadScene(2);
            else
                UpdateLine();
        }


	}
    
    
    void UpdateLine()
    {
        textbox.text = dialogue[currentLine];
    }

}
