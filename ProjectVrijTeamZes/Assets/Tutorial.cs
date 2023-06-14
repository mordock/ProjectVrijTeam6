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

    public TextMeshProUGUI tutorialText;

    public GameObject tutorialCharacter, firstTutorialArrowList;

    private int currentTutorialPlace = 0;
    [HideInInspector]public bool isPlayingTutorial;
    // Start is called before the first frame update
    void Start() {
        PlayTutorial();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            currentTutorialPlace++;
            if (currentTutorialPlace > 8) {
                EndTutorial();
            }
        }
        if (isPlayingTutorial) {
            tutorialText.text = firstTutorialTexts[currentTutorialPlace];

            for (int i = 0; i < firstTutorialArrows.Count; i++) {
                if (i.Equals(currentTutorialPlace)) {
                    if (firstTutorialArrows[i] != null) {
                        firstTutorialArrows[i].SetActive(true);
                    }
                } else {
                    if (firstTutorialArrows[i] != null) {
                        firstTutorialArrows[i].SetActive(false);
                    }
                }
            }
        }
    }

    private void EndTutorial() {
        Time.timeScale = 1;
        isPlayingTutorial = false;

        tutorialCharacter.SetActive(false);
        firstTutorialArrowList.SetActive(false);
    }

    public void PlayTutorial() {
        Time.timeScale = 0;
        isPlayingTutorial = true;

        tutorialCharacter.SetActive(true);
        firstTutorialArrowList.SetActive(true);
    }
}
