using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject card;
    // Set indexes that are allowed to avoid duplicates
    List<int> frontIndexes = new List<int> { 0, 1, 2, 3, 0, 1, 2, 3 };
    // Randomize as randomly as posssible
    public static System.Random rnd = new System.Random();
    public int shuffleNumber = 0;
    // Keeps track of visible cards
    int[] visibleCards = { -1, -2 };

    // Position new cards in alignment with static card in game
    private void Start()
    {
        int originalLength = frontIndexes.Count;
        float yPosition = 2.3f;
        float xPosition = -2.2f;

        for (int i = 0; i < 7; i++)
        {
            shuffleNumber = rnd.Next(0, (frontIndexes.Count));
            var temp = Instantiate(card, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
            temp.GetComponent<CardScript>().frontIndex = frontIndexes[shuffleNumber];
            //Confirm sprite image is only assigned to 2 objects
            frontIndexes.Remove(frontIndexes[shuffleNumber]);

            xPosition = xPosition + 4;

            if (i == (originalLength/2 - 2))
            {
                yPosition = -2.3f;
                xPosition = -6.2f;
            }
        }
        // Makes sure index doesn't fall on default card
        card.GetComponent<CardScript>().frontIndex = frontIndexes[0];
    }

    // Only 2 cards can be front facing at the same time
    public bool TwoCardsVisible()
    {
        bool cardFacing = false;
        // If index 0 is greater than 0 and 1 then 2 cards are showing, show no more
        if(visibleCards[0] >= 0 && visibleCards[1] >= 0)
        {
            cardFacing = true;
        }
        return cardFacing;
    }

    // Assigns sprite to visible card
    public void AddShowingFace (int index)
    {
        if(visibleCards[0] == -1)
        {
            visibleCards[0] = index;
        }
        else if (visibleCards[1] == -2)
        {
            visibleCards[1] = index;
        }
    }

    // Ensures no duplicates
    public void RemoveShowingFace(int index)
    {
        if (visibleCards[0] == index)
        {
            visibleCards[0] = -1;
        }
        else if (visibleCards[1] == index)
        {
            visibleCards[1] = -2;
        }
    }

    // Let's check the match
    public bool CheckMatch ()
    {
        bool success = false;

        if(visibleCards[0] == visibleCards[1])
        {
            visibleCards[0] = -1;
            visibleCards[1] = -2;
            success = true;
        }
        return success;
    }

    private void Awake()
    {
        card = GameObject.Find("Card");
    }
}
