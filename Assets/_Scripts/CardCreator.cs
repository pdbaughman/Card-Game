using UnityEngine;

public class CardCreator : MonoBehaviour
{

    [SerializeField] private GameObject[] cardPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = -4; x <= 4; x += 4) {
            for (int y = -4; y <= 4; y += 4) {
                GameObject card = Instantiate(cardPrefabs[0]);
                card.transform.position = new Vector2(x,y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
