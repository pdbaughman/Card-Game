using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    [Header("Setup")]
    public MemoryCard cardPrefab;
    public Transform cardGridParent;
    public List<Sprite> cardSprites;   // unique sprites for each pair
    public float flipBackDelay = 0.8f; // seconds before flipping back

    private MemoryCard firstCard;
    private MemoryCard secondCard;
    private bool isChecking = false;

    void Start()
    {
        SetupBoard();
    }

    void SetupBoard()
    {
        // Each sprite becomes a pair (2 cards)
        List<int> cardIds = new List<int>();

        for (int i = 0; i < cardSprites.Count; i++)
        {
            cardIds.Add(i);
            cardIds.Add(i); // add pair
        }

        Shuffle(cardIds);

        // Instantiate cards
        foreach (int id in cardIds)
        {
            MemoryCard card = Instantiate(cardPrefab, cardGridParent);
            card.Init(id, cardSprites[id], this);
        }
    }

    void Shuffle(List<int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    public void OnCardSelected(MemoryCard card)
    {
        if (isChecking)
            return;

        // First pick
        if (firstCard == null)
        {
            firstCard = card;
            card.ShowFront();
        }
        // Second pick
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            card.ShowFront();
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;

        // Wait so the player can see the second card
        yield return new WaitForSeconds(flipBackDelay);

        if (firstCard.id == secondCard.id)
        {
            // Match: keep them face up
            firstCard.SetMatched();
            secondCard.SetMatched();
            // Optional: check for win condition here
        }
        else
        {
            // Not a match: flip both back
            firstCard.ShowBack();
            secondCard.ShowBack();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }
}
