﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>();

    private bool firstGuess, secendGuess;

    private int countGuess;
    private int countCorrectGuess;
    private int gameGuess;

    private int firstGuessIndex, secendGuessIndex;

    private string firstGuessPuzzle, secendGuesPuzzle;

    void Awake() {
        puzzles = Resources.LoadAll<Sprite>("Sprites/Puzzles");
    }

	void Start () {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuess = gamePuzzles.Count / 2;
    }

    void GetButtons() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for(int i = 0; i < objects.Length; i++) {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles() {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++) {
            if(index == looper / 2) {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners() {
        foreach(Button btn in btns) {
            btn.onClick.AddListener(()=>PickAPuzzle());
        }
    }

    public void PickAPuzzle() {
        int puzzleName = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        if(!firstGuess) {
            firstGuess = true;
            firstGuessIndex = puzzleName;
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].interactable = false;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        } else if(!secendGuess) {
            secendGuess = true;
            secendGuessIndex = puzzleName;
            secendGuesPuzzle = gamePuzzles[secendGuessIndex].name;

            btns[secendGuessIndex].interactable = false;
            btns[secendGuessIndex].image.sprite = gamePuzzles[secendGuessIndex];

            countGuess++;

            StartCoroutine(CheckIfPuzzlesMatch());
        }
    }

    IEnumerator CheckIfPuzzlesMatch() {
        yield return new WaitForSeconds(1f);

        if(firstGuessPuzzle == secendGuesPuzzle) {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secendGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0,0,0,0);
            btns[secendGuessIndex].image.color = new Color(0,0,0,0);

            CheckIfGameFinished();
        } else {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = true;
            btns[secendGuessIndex].interactable = true;

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secendGuessIndex].image.sprite = bgImage;
        }
        yield return new WaitForSeconds(.5f);

        firstGuess = secendGuess = false;
    }

    void CheckIfGameFinished() {
        countCorrectGuess++;

        if(countCorrectGuess == gameGuess) {
            Debug.Log("Game Finished!");
            Debug.Log("You Took " + countGuess + " Time(s) To Finished!");
        }
    }

    void Shuffle(List<Sprite> List) {
        for(int i = 0; i < List.Count; i++) {
            Sprite temp = List[i];
            int ramdonIndex = Random.Range(i,List.Count);
            List[i] = List[ramdonIndex];
            List[ramdonIndex] = temp;
        }
    }
}
