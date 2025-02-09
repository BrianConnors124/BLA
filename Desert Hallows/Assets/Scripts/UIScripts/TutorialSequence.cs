using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialSequence : MonoBehaviour
{
    public GameObject[] arrow;
    public GameObject textBoard;
    public GameObject grayOut;
    public GameObject[] makeMagicHappen;

    private int sequence = 0;
    private float timer;

    public float sequenceLength;

    public int sequences;
    
    public string[] text;
    
    public TextMeshProUGUI visibleText;
    
    
    void Start()
    {
        InputSystemController.instance.nextSequence += Sequence;
    }
    

    private void OnEnable()
    {
        sequence = 0;
        textDone = true;
        GameObject.Find("Player").GetComponent<PlayerInput>().SwitchCurrentActionMap("TutorialSequence");
        Sequence();
        Time.timeScale = 0;
        timer = sequenceLength;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        GameObject.Find("Player").GetComponent<PlayerInput>().SwitchCurrentActionMap("Movement");
    }

    private void Sequence()
    {
        if (sequence > 0 && textDone)
        {
            arrow[sequence - 1].SetActive(false);
            makeMagicHappen[sequence - 1].SetActive(true);
        }

        if (sequence == sequences)
        {
            textBoard.SetActive(false);
            grayOut.SetActive(false);
            arrow[sequence -1].SetActive(false);
            foreach (var obj in makeMagicHappen)
                obj.SetActive(true);
            
            gameObject.SetActive(false);
        }
        else if(textDone)
        {
            textDone = false;
            StartCoroutine(TypeWriterText(text[sequence]));
            arrow[sequence].SetActive(true);
            textBoard.SetActive(true);
            grayOut.SetActive(true);
            makeMagicHappen[sequence].SetActive(false);
        
            sequence++;
            timer = sequenceLength;
        }
        else
        {
            var a = typeSpeed;
            typeSpeed = 0;
        }
    }
    


    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Sequence();
        }
    }


    #region TypeWriter

    private bool textDone;
    public float typeSpeed;

    private IEnumerator TypeWriterText(string setText)
    {
        visibleText.text = "";
        var a = typeSpeed;

        

        for (int i = 0; i < setText.Length;)
        {
            visibleText.text += setText.Substring(i, 1);
            yield return new WaitForSecondsRealtime(typeSpeed);
            i++;
        }

        textDone = true;
        typeSpeed = a;
    }
    

    #endregion
}
