using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MemoryGameManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardGridParent;
    [SerializeField] private List<Sprite> cardSprites;   // unique sprites for each pair
	[SerializeField] private List<CardPair> newSprites;   // unique sprites for each pair
    [SerializeField] private float flipBackDelay = 0.8f; // seconds before flipping back
	[SerializeField] private GameObject winScreen;

    private MemoryCard firstCard;
    private MemoryCard secondCard;
    private bool isChecking = false;
	
	public static MemoryGameManager instance;

	void Awake()
	{
		instance = this;
	}

    void Start()
    {
        SetupBoard();
    }

    void SetupBoard()
    {
        // Each sprite becomes a pair (2 cards)
        List<int> cardIds = new List<int>();
		List<int> usedIds = new List<int>();

        for (int i = 0; i < newSprites.Count; i++)
        {
            cardIds.Add(i);
            cardIds.Add(i); // add pair
        }

        Shuffle(cardIds);

        // Instantiate cards
        foreach (int id in cardIds)
        {
			GameObject card = Instantiate(cardPrefab, cardGridParent);
			MemoryCard cardScript = card.GetComponent<MemoryCard>();
			// Decide which image to use
			if (usedIds.Contains(id))
			{
				cardScript.Init(id, newSprites[id].card2);
			}
            else
            {
				cardScript.Init(id, newSprites[id].card1);
				usedIds.Add(id);
			}
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
            // Check win condition
			var flag = false;
			foreach (Transform child in cardGridParent) 
			{
				var cardScript = child.gameObject.GetComponent<MemoryCard>();
				if (!cardScript.GetMatched())
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				// Win
				winScreen.SetActive(true);
			}
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

[Serializable]
public struct CardPair
{
	public Sprite card1;
	public Sprite card2;
}