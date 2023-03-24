using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    GameObject gameManagerScript;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer[] cardFronts;
    public Sprite cardBack;
    // Map the default cards by indexing
    public int frontIndex;
    // Checking for matched cards
    public bool matched = false;


    public void OnMouseDown()
    {
        if (matched == false)
        {
            // If back of card is visible, show front
            if (spriteRenderer.sprite == cardBack)
            {
                // Security check: references other script to check for visibility, if false then flip over card
                if (gameManagerScript.GetComponent<GameManager>().TwoCardsVisible() == false)
                {
                    spriteRenderer.sprite = cardFronts[frontIndex].sprite;
                    gameManagerScript.GetComponent<GameManager>().AddShowingFace(frontIndex);
                    matched = gameManagerScript.GetComponent<GameManager>().CheckMatch();
                }
            }
            else
            {
                spriteRenderer.sprite = cardBack;
                gameManagerScript.GetComponent<GameManager>().RemoveShowingFace(frontIndex);
            }
        }
    }

    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}