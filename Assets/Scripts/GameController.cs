using UnityEngine;
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
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("You Are Clicking A Button named " + name);
    }
}
