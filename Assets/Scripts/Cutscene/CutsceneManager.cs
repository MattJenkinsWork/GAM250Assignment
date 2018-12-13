using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public string[] dialogue;

    int currentLine = 0;

    public Text textbox;
    public GameObject textboxImage;

    private void Start()
    {
        NextLine();
    }


    // Update is called once per frame
    void Update () {
		
        if (Input.anyKeyDown)
        {
            currentLine++;

            textboxImage.SetActive(currentLine >= 1);

            //if (currentline >= 1)
            //    textboximage.setactive(true);
            //else
            //    textboximage.setactive(false);


            if (currentLine >= dialogue.Length)
                SceneManager.LoadScene(2);
            else
                NextLine();
        }


	}
    
    void NextLine()
    {
        
        textbox.text = dialogue[currentLine];

    }

}
