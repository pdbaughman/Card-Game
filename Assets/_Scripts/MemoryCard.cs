using UnityEngine;
using UnityEngine.UI;

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
        if (frontFace != null) frontFace.SetActive(true);
        if (backFace != null) backFace.SetActive(false);
        isFaceUp = true;
    }

    public void ShowBack()
    {
        if (frontFace != null) frontFace.SetActive(false);
        if (backFace != null) backFace.SetActive(true);
        isFaceUp = false;
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
