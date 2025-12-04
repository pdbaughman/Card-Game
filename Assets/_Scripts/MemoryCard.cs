using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemoryCard : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject frontFace;   // assign FrontImage object
    public GameObject backFace;    // assign BackImage object
    public Image frontImage;       // Image component on FrontImage

    [HideInInspector] public int id;   // pair id
    
	private MemoryGameManager manager;

    private bool isFaceUp = false;
    private bool isMatched = false;

    private bool isFlipping = false;

    IEnumerator Flip(bool showFront)
    {
        isFlipping = true;

        // 1. rotate 0 → 90
        for (float t = 0; t < 1; t += Time.deltaTime * 6f)
        {
            float y = Mathf.Lerp(0, 90, t);
            transform.localRotation = Quaternion.Euler(0, y, 0);
            yield return null;
        }

        // 2. swap images at the halfway point
        frontFace.SetActive(showFront);
        backFace.SetActive(!showFront);

        // 3. rotate 90 → 180
        for (float t = 0; t < 1; t += Time.deltaTime * 6f)
        {
            float y = Mathf.Lerp(90, 180, t);
            transform.localRotation = Quaternion.Euler(0, y, 0);
            yield return null;
        }

        // 4. reset rotation (so it ends at normal 0)
        transform.localRotation = Quaternion.identity;

        isFaceUp = showFront;
        isFlipping = false;
    }


    void Awake()
    {
        // Hook up the button click to our handler
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnCardClicked);
        }

        ShowBack();
    }
	
	void Start() 
	{
		// Get game manager
		manager = MemoryGameManager.instance;
	}

    public void Init(int id, Sprite sprite)
    {
        this.id = id;

        if (frontImage != null && sprite != null)
        {
            frontImage.sprite = sprite;
        }

        ShowBack();
        isMatched = false;
    }

    public void OnCardClicked()
    {
        if (isMatched || isFaceUp)
            return;

        manager.OnCardSelected(this);
    }

    public void ShowFront()
    {
        if (!isFlipping)
            StartCoroutine(Flip(true));
    }

    public void ShowBack()
    {
        if (!isFlipping)
            StartCoroutine(Flip(false));
    }


    public void SetMatched()
    {
        isMatched = true;
        // Optionally disable button so it can't be clicked
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.interactable = false;
    }
	
	public bool GetMatched()
	{
		return isMatched;
	}
}
