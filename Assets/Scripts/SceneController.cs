using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public int gridRows = 4;
    public int gridCols = 2;
    public float offsetX = 7f;
    public float offsetY = 2.5f;
    public AudioSource victorySound;
    public AudioSource matchSound;


    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] drinken;
    [SerializeField] private Sprite[] ontbijt;
    [SerializeField] private Sprite[] avondeten;

    public PictureState Field;

    public Sprite[] Changepictures()
    {
        Sprite[] images;
        switch (Field.State)
        {
            default:
            case (0):
                images = drinken;
                break;
            case (1):
                images = ontbijt;
                break;
            case (2):
                images = avondeten;
                break;
        }
        return images;
    }


 private void Start()//Zet kaarten op spel
    {
        Field = GameObject.Find("PictureCase").GetComponent<PictureState>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };

        if (sceneName == "Level1")
        {
            numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3 };

        }
        else if (sceneName == "Level2")
        {
            numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
            gridRows = 4;
            gridCols = 3;
            offsetX = 6f;
            offsetY = 2f;
}
        else if (sceneName == "Level3")
        {
            numbers = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8,8,9,9,10,10,11,11 };
        }
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                Sprite[] images = Changepictures();
                card.ChangeSprite(id, images[id]);

                float postX = (offsetX * i) + startPos.x;
                float postY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(postX, postY, startPos.z);
            }
        }
    }
    private int[] ShuffleArray(int[] numbers)//Kaarten schudden
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    //Score en spelregels

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    private int _win = 0;
    [SerializeField] private TextMesh scoreLabel;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id)
        {
            _win++;
            matchSound.Play();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
            _score++;
            scoreLabel.text = "Fouten: " + _score;
        }

        _firstRevealed = null;
        _secondRevealed = null;

        if (_win == Changepictures().Length)
        {
            victorySound.Play();
        }
    }


    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level1")
        {
            SceneManager.LoadScene("Level1");
        }
        else if (sceneName == "Level2")
        {
            SceneManager.LoadScene("Level2");
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}   
