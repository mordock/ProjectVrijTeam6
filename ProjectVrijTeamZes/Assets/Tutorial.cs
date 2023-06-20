using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Tutorial : MonoBehaviour
{
    public List<string> firstTutorialTexts;
    public List<string> enclosureTutorialTexts;
    public List<GameObject> firstTutorialArrows;
    public List<GameObject> enclosureTutorialArrows;

    public TextMeshProUGUI tutorialText;
    public bool enclosureTutorialDone = false;

    public GameObject tutorialCharacter;

    private int currentTutorialPlace = 0;
    private int currentEnclosureTutorialPlace = 0;
    [HideInInspector]public bool isPlayingFirstTutorial, isPlayingEnclosureTutorial;
    // Start is called before the first frame update
    void Start() {
        PlayTutorial();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isPlayingFirstTutorial)
            {
                EndTutorial();
                currentTutorialPlace = 12;
            }
            if (isPlayingEnclosureTutorial)
            {
                EndTutorial();
            }

        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            if (currentTutorialPlace < 12 && isPlayingFirstTutorial)
                currentTutorialPlace++;
            if (currentEnclosureTutorialPlace < 7 && isPlayingEnclosureTutorial)
                currentEnclosureTutorialPlace++;
            if ((currentTutorialPlace == 12 && isPlayingFirstTutorial) || (currentEnclosureTutorialPlace == 7 && isPlayingEnclosureTutorial)) {
                EndTutorial();
            }

            if (isPlayingFirstTutorial) {
                tutorialText.text = firstTutorialTexts[currentTutorialPlace];

                for (int i = 0; i < firstTutorialArrows.Count; i++) {
                    if (i.Equals(currentTutorialPlace)) {
                        if (firstTutorialArrows[i] != null) {
                            firstTutorialArrows[i].SetActive(true);
                        }
                    } else {
                        if (firstTutorialArrows[i] != null && firstTutorialArrows[i] != firstTutorialArrows[currentTutorialPlace]) {
                            firstTutorialArrows[i].SetActive(false);
                        }
                    }
                }
            }

            if (isPlayingEnclosureTutorial) {
                tutorialText.text = enclosureTutorialTexts[currentEnclosureTutorialPlace];

                for (int i = 0; i < enclosureTutorialArrows.Count; i++) {
                    if (i.Equals(currentEnclosureTutorialPlace)) {
                        if (enclosureTutorialArrows[i] != null) {
                            enclosureTutorialArrows[i].SetActive(true);
                        }
                    } else {
                        if (enclosureTutorialArrows[i] != null && enclosureTutorialArrows[i] != enclosureTutorialArrows[currentEnclosureTutorialPlace]) {
                            enclosureTutorialArrows[i].SetActive(false);
                        }
                    }
                }
            }
        }
    }

    private void EndTutorial() {
        GetComponent<TickManager>().timePaused = false;
        isPlayingFirstTutorial = false;
        isPlayingEnclosureTutorial = false;
        tutorialCharacter.SetActive(false);
        for (int i = 0; i < firstTutorialArrows.Count; i++)
        {
            if (firstTutorialArrows[i] != null)
            {
                firstTutorialArrows[i].SetActive(false);
            }
        }
        for (int i = 0; i < enclosureTutorialArrows.Count; i++)
        {
            if (enclosureTutorialArrows[i] != null)
            {
                enclosureTutorialArrows[i].SetActive(false);
            }
        }
    }

    public void PlayTutorial() {
        GetComponent<TickManager>().timePaused = true;
        isPlayingFirstTutorial = true;

        tutorialCharacter.SetActive(true);
    }
    public void PlayEnclosureTutorial()
    {
        GetComponent<TickManager>().timePaused = true;
        isPlayingEnclosureTutorial = true;
        tutorialCharacter.SetActive(true);
        tutorialText.text = enclosureTutorialTexts[currentEnclosureTutorialPlace];
    }
}
