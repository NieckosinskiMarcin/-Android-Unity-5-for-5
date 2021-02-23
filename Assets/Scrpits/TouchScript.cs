using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    public List<GameObject> listOfLetters = new List<GameObject>();
    public List<TryClass> tries = new List<TryClass>();
    private SpriteRenderer theSR;
    private int letterCounter = 0, tryCounter = 0;
    private bool winFlag = false;
    private int guessedWordCounter = 0;
    private List<int> goodLetters = new List<int>();
    private List<int> notInPlaceLetters = new List<int>();
    private List<string> guessedLetters = new List<string>();
    private string word;

    void Start()
    {
        word = Word.randomWordGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                GameObject touchedObject = hitInformation.transform.gameObject;
                foreach (GameObject letter in listOfLetters)
                {
                    if (touchedObject.tag == letter.tag)
                    {
                        //Debug.Log(letter.tag);
                        theSR = letter.GetComponent<SpriteRenderer>();
                        tries[tryCounter].fields[letterCounter].GetComponent<SpriteRenderer>().sprite = theSR.sprite;

                        if(letter.tag.Equals("as"))
                            guessedLetters.Add("ą");
                        else if (letter.tag.Equals("cs"))
                            guessedLetters.Add("ć");
                        else if (letter.tag.Equals("es"))
                            guessedLetters.Add("ę");
                        else if (letter.tag.Equals("ls"))
                            guessedLetters.Add("ł");
                        else if (letter.tag.Equals("ns"))
                            guessedLetters.Add("ń");
                        else if (letter.tag.Equals("os"))
                            guessedLetters.Add("ó");
                        else if (letter.tag.Equals("ss"))
                            guessedLetters.Add("ś");
                        else if (letter.tag.Equals("zs"))
                            guessedLetters.Add("ź");
                        else if (letter.tag.Equals("zsp"))
                            guessedLetters.Add("ż");
                        else
                            guessedLetters.Add(letter.tag);

                        letterCounter++;
                        if(letterCounter == 5)
                        {
                            for(int i = 0; i < 5; i++)
                            {
                                if(word[i].ToString().Equals(guessedLetters[i]))
                                {
                                    goodLetters.Add(i);
                                }
                            }
                            string hehe = "";
                            foreach(int number in goodLetters)
                            {
                                hehe += number.ToString();
                                hehe += " ";
                            }
                            Debug.Log(hehe);
                            for(int i = 0; i < 5; i++)
                            {
                                if (word.Contains(guessedLetters[i]) && !goodLetters.Contains(i))
                                {
                                    notInPlaceLetters.Add(i);
                                }
                            }
                            hehe = "";
                            foreach (int number in notInPlaceLetters)
                            {
                                hehe += number.ToString();
                                hehe += " ";
                            }
                            Debug.Log(hehe);
                            guessedLetters.Clear();
                            foreach(int number in goodLetters)
                            {
                                Debug.Log(word);
                                tries[tryCounter].fields[number].GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f);
                            }
                            foreach (int num in notInPlaceLetters)
                            {
                                Debug.Log(word);
                                tries[tryCounter].fields[num].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 0f);
                            }
                            
                            if (goodLetters.Count == 5)
                            {
                                word = Word.randomWordGenerator();
                                tryCounter = -1;
                                foreach (TryClass gameTry in tries)
                                {
                                    foreach (GameObject field in gameTry.fields)
                                    {
                                        field.GetComponent<SpriteRenderer>().sprite = gameTry.emptySprite;
                                        field.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);
                                    }
                                }
                            }
                            if (tryCounter == 5 && letterCounter == 5 && winFlag == false)
                            {
                                //tu ekran przegranej z wynikiem
                            }
                            goodLetters.Clear();
                            notInPlaceLetters.Clear();
                            tryCounter++;
                            letterCounter = 0;
                        }
                        //Debug.Log(theSR.sprite.name);
                    }
                }
                foreach (TryClass gameTry in tries)
                {
                    foreach(GameObject field in gameTry.fields)
                    {
                        if(field.tag == touchedObject.tag && field == gameTry.fields[letterCounter-1])
                        {
                            field.GetComponent<SpriteRenderer>().sprite = gameTry.emptySprite;
                            letterCounter--;
                        }
                    }
                }
            }

        }
    }
}
