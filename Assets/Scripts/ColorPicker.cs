using UnityEngine;
using Color = UnityEngine.Color;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{

    public GameObject UI_Element;
    public GameObject ShopPanel;

    // 1
    public Color BlueColor, BlueColorText;
    public Color BlueColorPick, BlueColorPickText;

    // 2
    public Color RedColor, RedColorText;
    public Color RedColorPick, RedColorPickText;

    // 3
    public Color YellowColor, YellowColorText;
    public Color YellowColorPick, YellowColorPickText;

    // 4
    public Color PinkColor, PinkColorText;
    public Color PinkColorPick, PinkColorPickText;

    // 5
    public Color GreenColor, GreenColorText;
    public Color GreenColorPick, GreenColorPickText;

    // 6
    public Color OrangeColor, OrangeColorText;
    public Color OrangeColorPick, OrangeColorPickText;

    //7
    public Color PurpleColor, PurpleColorText;
    public Color PurpleColorPick, PurpleColorPickText;

    Player thePlayer;

    public GameObject BluePanel;
    public GameObject RedPanel;

    public Image BlueP, RedP, YellowP, PinkP, GreenP, OrangeP, PurpleP;

    public Image Rect;
    public Image Circle;

    public int ColorPrice = 500;

    public static ColorPicker _Instance { get; private set; }

    public bool StopGame;

    public AudioSource OpenShopSound;
    public AudioSource WariningSound;
    public AudioSource SelectSound;

    private void Awake()
    {
        if (_Instance != null & _Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _Instance = this;
        }
    }


    private void Start()
    {
        if (PlayerPrefs.HasKey("Color_Pick"))
        {
            int currentColor = PlayerPrefs.GetInt("Color_Pick");
            SetColor(currentColor);
        }
        else
        {
            UI_Element.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Color_Pick", 1);

            PlayerPrefs.SetInt("Blue_Key", 1);
            PlayerPrefs.SetInt("Red_Key", 1);

            int color = GetCurrentColor();

            if (color == 1)
            {
                BluePanel.GetComponent<Image>().color = Color.green;
                BluePanel.transform.GetChild(1).gameObject.SetActive(true);

                RedPanel.GetComponent<Image>().color = Color.gray;
                RedPanel.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (color == 2)
            {
                BluePanel.GetComponent<Image>().color = Color.gray;
                BluePanel.transform.GetChild(1).gameObject.SetActive(false);

                RedPanel.GetComponent<Image>().color = Color.green;
                RedPanel.transform.GetChild(1).gameObject.SetActive(true);
            }

            Time.timeScale = 0;
            StopGame = true;
        }
    }

    public void Color_Picker(int color)
    {
        int currentColor = GetCurrentColor();
        if (currentColor == color) return;

        if (color == 3)
        {
            if (!PlayerPrefs.HasKey("Yellow_Key"))
            {
                if (LevelManager._Instance.CurrentCoin >= ColorPrice)
                {
                    PlayerPrefs.SetInt("Yellow_Key", 1);
                    LevelManager._Instance.CurrentCoin -= ColorPrice;
                    LevelManager._Instance.UpdateCoin();

                    YellowP.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    WariningSound.Play();
                    return;
                }
                    
            }
        }
        else if (color == 4)
        {
            if (!PlayerPrefs.HasKey("Pink_Key"))
            {
                if (LevelManager._Instance.CurrentCoin >= ColorPrice)
                {
                    PlayerPrefs.SetInt("Pink_Key", 1);
                    LevelManager._Instance.CurrentCoin -= ColorPrice;
                    LevelManager._Instance.UpdateCoin();
                    PinkP.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    WariningSound.Play();
                    return;
                }
            }
        }
        else if (color == 5)
        {
            if (!PlayerPrefs.HasKey("Green_Key"))
            {
                if (LevelManager._Instance.CurrentCoin >= ColorPrice)
                {
                    PlayerPrefs.SetInt("Green_Key", 1);
                    LevelManager._Instance.CurrentCoin -= ColorPrice;
                    LevelManager._Instance.UpdateCoin();
                    GreenP.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    WariningSound.Play();
                    return;
                }
            }
        }
        else if (color == 6)
        {
            if (!PlayerPrefs.HasKey("Orange_Key"))
            {
                if (LevelManager._Instance.CurrentCoin >= ColorPrice)
                {
                    PlayerPrefs.SetInt("Orange_Key", 1);
                    LevelManager._Instance.CurrentCoin -= ColorPrice;
                    LevelManager._Instance.UpdateCoin();
                    OrangeP.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    WariningSound.Play();
                    return;
                }
            }
        }
        else if (color == 7)
        {
            if (!PlayerPrefs.HasKey("Purple_Key"))
            {
                if (LevelManager._Instance.CurrentCoin >= ColorPrice)
                {
                    PlayerPrefs.SetInt("Purple_Key", 1);
                    LevelManager._Instance.CurrentCoin -= ColorPrice;
                    LevelManager._Instance.UpdateCoin();
                    PurpleP.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    WariningSound.Play();
                    return;
                }
            }
        }

        if (LevelManager._Instance.LearningLevels)
        {
            if (color == 1)
            {
                BluePanel.GetComponent<Image>().color = Color.green;
                BluePanel.transform.GetChild(1).gameObject.SetActive(true);

                RedPanel.GetComponent<Image>().color = Color.gray;
                RedPanel.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (color == 2)
            {
                BluePanel.GetComponent<Image>().color = Color.gray;
                BluePanel.transform.GetChild(1).gameObject.SetActive(false);

                RedPanel.GetComponent<Image>().color = Color.green;
                RedPanel.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            switch (color)
            {
                case 1:
                    BlueP.color = Color.green;
                    BlueP.transform.GetChild(1).gameObject.SetActive(true);

                    RedP.color = Color.gray;
                    RedP.transform.GetChild(1).gameObject.SetActive(false);

                    YellowP.color = Color.gray;
                    YellowP.transform.GetChild(1).gameObject.SetActive(false);

                    PinkP.color = Color.gray;
                    PinkP.transform.GetChild(1).gameObject.SetActive(false);

                    GreenP.color = Color.gray;
                    GreenP.transform.GetChild(1).gameObject.SetActive(false);

                    OrangeP.color = Color.gray;
                    OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                    PurpleP.color = Color.gray;
                    PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                    Rect.color = BlueColor;
                    Circle.color = BlueColorPick;
                    break;
                case 2:
                    BlueP.color = Color.gray;
                    BlueP.transform.GetChild(1).gameObject.SetActive(false);

                    RedP.color = Color.green;
                    RedP.transform.GetChild(1).gameObject.SetActive(true);

                    YellowP.color = Color.gray;
                    YellowP.transform.GetChild(1).gameObject.SetActive(false);

                    PinkP.color = Color.gray;
                    PinkP.transform.GetChild(1).gameObject.SetActive(false);

                    GreenP.color = Color.gray;
                    GreenP.transform.GetChild(1).gameObject.SetActive(false);

                    OrangeP.color = Color.gray;
                    OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                    PurpleP.color = Color.gray;
                    PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                    Rect.color = RedColor;
                    Circle.color = RedColorPick;
                    break;
                case 3:
                    BlueP.color = Color.gray;
                    BlueP.transform.GetChild(1).gameObject.SetActive(false);

                    RedP.color = Color.gray;
                    RedP.transform.GetChild(1).gameObject.SetActive(false);

                    YellowP.color = Color.green;
                    YellowP.transform.GetChild(1).gameObject.SetActive(true);

                    PinkP.color = Color.gray;
                    PinkP.transform.GetChild(1).gameObject.SetActive(false);

                    GreenP.color = Color.gray;
                    GreenP.transform.GetChild(1).gameObject.SetActive(false);

                    OrangeP.color = Color.gray;
                    OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                    PurpleP.color = Color.gray;
                    PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                    Rect.color = YellowColor;
                    Circle.color = YellowColorPick;
                    break;
                case 4:
                    BlueP.color = Color.gray;
                    BlueP.transform.GetChild(1).gameObject.SetActive(false);

                    RedP.color = Color.gray;
                    RedP.transform.GetChild(1).gameObject.SetActive(false);

                    YellowP.color = Color.gray;
                    YellowP.transform.GetChild(1).gameObject.SetActive(false);

                    PinkP.color = Color.green;
                    PinkP.transform.GetChild(1).gameObject.SetActive(true);

                    GreenP.color = Color.gray;
                    GreenP.transform.GetChild(1).gameObject.SetActive(false);

                    OrangeP.color = Color.gray;
                    OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                    PurpleP.color = Color.gray;
                    PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                    Rect.color = PinkColor;
                    Circle.color = PinkColorPick;
                    break;
                case 5:
                    BlueP.color = Color.gray;
                    BlueP.transform.GetChild(1).gameObject.SetActive(false);

                    RedP.color = Color.gray;
                    RedP.transform.GetChild(1).gameObject.SetActive(false);

                    YellowP.color = Color.gray;
                    YellowP.transform.GetChild(1).gameObject.SetActive(false);

                    PinkP.color = Color.gray;
                    PinkP.transform.GetChild(1).gameObject.SetActive(false);

                    GreenP.color = Color.green;
                    GreenP.transform.GetChild(1).gameObject.SetActive(true);

                    OrangeP.color = Color.gray;
                    OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                    PurpleP.color = Color.gray;
                    PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                    Rect.color = GreenColor;
                    Circle.color = GreenColorPick;
                    break;
                case 6:
                    BlueP.color = Color.gray;
                    BlueP.transform.GetChild(1).gameObject.SetActive(false);

                    RedP.color = Color.gray;
                    RedP.transform.GetChild(1).gameObject.SetActive(false);

                    YellowP.color = Color.gray;
                    YellowP.transform.GetChild(1).gameObject.SetActive(false);

                    PinkP.color = Color.gray;
                    PinkP.transform.GetChild(1).gameObject.SetActive(false);

                    GreenP.color = Color.gray;
                    GreenP.transform.GetChild(1).gameObject.SetActive(false);

                    OrangeP.color = Color.green;
                    OrangeP.transform.GetChild(1).gameObject.SetActive(true);

                    PurpleP.color = Color.gray;
                    PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                    Rect.color = OrangeColor;
                    Circle.color = OrangeColorPick;
                    break;
                case 7:
                    BlueP.color = Color.gray;
                    BlueP.transform.GetChild(1).gameObject.SetActive(false);

                    RedP.color = Color.gray;
                    RedP.transform.GetChild(1).gameObject.SetActive(false);

                    YellowP.color = Color.gray;
                    YellowP.transform.GetChild(1).gameObject.SetActive(false);

                    PinkP.color = Color.gray;
                    PinkP.transform.GetChild(1).gameObject.SetActive(false);

                    GreenP.color = Color.gray;
                    GreenP.transform.GetChild(1).gameObject.SetActive(false);

                    OrangeP.color = Color.gray;
                    OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                    PurpleP.color = Color.green;
                    PurpleP.transform.GetChild(1).gameObject.SetActive(true);

                    Rect.color = PurpleColor;
                    Circle.color = PurpleColorPick;
                    break;
            }
        }

        SelectSound.Play();

        SetColor(color);
    }

    private int GetCurrentColor()
    {
        return PlayerPrefs.GetInt("Color_Pick");
    }

    private void GetCurrentPlayer()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    public void SetColor(int toColor)
    {
        GetCurrentPlayer();

        thePlayer.BlueColor = BlueColor;
        thePlayer.BlueColorText = BlueColorText;
        thePlayer.BlueColorPick = BlueColorPick;
        thePlayer.BlueColorPickText = BlueColorPickText;

        thePlayer.RedColor = RedColor;
        thePlayer.RedColorText = RedColorText;

        thePlayer.YellowColor = YellowColor;
        thePlayer.YellowColorText = YellowColorText;

        thePlayer.PinkColor = PinkColor;
        thePlayer.PinkColorText = PinkColorText;

        thePlayer.GreenColor = GreenColor;
        thePlayer.GreenColorText = GreenColorText;

        thePlayer.OrangeColor = OrangeColor;
        thePlayer.OrangeColorText = OrangeColorText;

        thePlayer.LastColor = PurpleColor;
        thePlayer.LastColorText = PurpleColorText;

        LevelManager._Instance.BlueScore.color = BlueColor;
        LevelManager._Instance.RedScore.color = RedColor;
        LevelManager._Instance.YellowScore.color = YellowColor;
        LevelManager._Instance.PinkScore.color = PinkColor;
        LevelManager._Instance.GreenScore.color = GreenColor;
        LevelManager._Instance.OrangeScore.color = OrangeColor;
        LevelManager._Instance.PurpleScore.color = PurpleColor;

        SetPrimiaryColorToSecondColor(toColor, thePlayer);
        SetSecondColorToPrimiaryColor(toColor, thePlayer);
       

        LevelManager._Instance.UpdateColors();
        PlayerPrefs.SetInt("Color_Pick", toColor);
    }

    private void SetPrimiaryColorToSecondColor(int toColor, Player theplayer)
    {
        if (toColor == 1)
        {
            thePlayer.BlueColor = BlueColor;
            thePlayer.BlueColorText = BlueColorText;
            thePlayer.BlueColorPick = BlueColorPick;
            thePlayer.BlueColorPickText = BlueColorPickText;

            LevelManager._Instance.BlueScore.color = BlueColor;
        }
        else if (toColor == 2)
        {
            thePlayer.BlueColor = RedColor;
            thePlayer.BlueColorText = RedColorText;
            thePlayer.BlueColorPick = RedColorPick;
            thePlayer.BlueColorPickText = RedColorPickText;

            LevelManager._Instance.BlueScore.color = RedColor;
        }
        else if (toColor == 3)
        {
            thePlayer.BlueColor = YellowColor;
            thePlayer.BlueColorText = YellowColorText;
            thePlayer.BlueColorPick = YellowColorPick;
            thePlayer.BlueColorPickText = YellowColorPickText;

            LevelManager._Instance.BlueScore.color = YellowColor;
        }
        else if (toColor == 4)
        {
            thePlayer.BlueColor = PinkColor;
            thePlayer.BlueColorText = PinkColorText;
            thePlayer.BlueColorPick = PinkColorPick;
            thePlayer.BlueColorPickText = PinkColorPickText;

            LevelManager._Instance.BlueScore.color = PinkColor;
        }
        else if (toColor == 5)
        {
            thePlayer.BlueColor = GreenColor;
            thePlayer.BlueColorText = GreenColorText;
            thePlayer.BlueColorPick = GreenColorPick;
            thePlayer.BlueColorPickText = GreenColorPickText;

            LevelManager._Instance.BlueScore.color = GreenColor;
        }
        else if (toColor == 6)
        {
            thePlayer.BlueColor = OrangeColor;
            thePlayer.BlueColorText = OrangeColorText;
            thePlayer.BlueColorPick = OrangeColorPick;
            thePlayer.BlueColorPickText = OrangeColorPickText;

            LevelManager._Instance.BlueScore.color = OrangeColor;
        }
        else if (toColor == 7)
        {
            thePlayer.BlueColor = PurpleColor;
            thePlayer.BlueColorText = PurpleColorText;
            thePlayer.BlueColorPick = PurpleColorPick;
            thePlayer.BlueColorPickText = PurpleColorPickText;

            LevelManager._Instance.BlueScore.color = PurpleColor;
        }
    }
    private void SetSecondColorToPrimiaryColor(int toColor, Player theplayer)
    {
        if (toColor == 1)
        {
            thePlayer.BlueColor = BlueColor;
            thePlayer.BlueColorText = BlueColorText;
            thePlayer.BlueColorPick = BlueColorPick;
            thePlayer.BlueColorPickText = BlueColorPickText;

            LevelManager._Instance.BlueScore.color = BlueColor;
        }
        else if (toColor == 2)
        {
            thePlayer.RedColor = BlueColor;
            thePlayer.RedColorText = BlueColorText;

            LevelManager._Instance.RedScore.color = BlueColor;
        }
        else if (toColor == 3)
        {
            thePlayer.YellowColor = BlueColor;
            thePlayer.YellowColorText = BlueColorText;

            LevelManager._Instance.YellowScore.color = BlueColor;
        }
        else if (toColor == 4)
        {
            thePlayer.PinkColor = BlueColor;
            thePlayer.PinkColorText = BlueColorText;

            LevelManager._Instance.PinkScore.color = BlueColor;
        }
        else if (toColor == 5)
        {
            thePlayer.GreenColor = BlueColor;
            thePlayer.GreenColorText = BlueColorText;

            LevelManager._Instance.GreenScore.color = BlueColor;
        }
        else if (toColor == 6)
        {
            thePlayer.OrangeColor = BlueColor;
            thePlayer.OrangeColorText = BlueColorText;

            LevelManager._Instance.OrangeScore.color = BlueColor;
        }
        else if (toColor == 7)
        {
            thePlayer.LastColor = BlueColor;
            thePlayer.LastColorText = BlueColorText;

            LevelManager._Instance.PurpleScore.color = BlueColor;
        }
    }

    public void ProcessTutorialPart()
    {
        UI_Element.gameObject.SetActive(false);
        StopGame = false;
        Time.timeScale = 1;
    }

    public void ShopPanelCloser()
    {
        ShopPanel.gameObject.SetActive(false);
    }

    public void ShopPanelButton()
    {
        OpenShopSound.Play();
        ShopPanel.gameObject.SetActive(true);

        if (PlayerPrefs.HasKey("Yellow_Key"))
        {
            YellowP.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Pink_Key"))
        {
            PinkP.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Green_Key"))
        {
            GreenP.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Orange_Key"))
        {
            OrangeP.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Purple_Key"))
        {
            PurpleP.transform.GetChild(2).gameObject.SetActive(false);
        }

        int color = GetCurrentColor();

        switch (color)
        {
            case 1:
                BlueP.color = Color.green;
                BlueP.transform.GetChild(1).gameObject.SetActive(true);

                RedP.color = Color.gray;
                RedP.transform.GetChild(1).gameObject.SetActive(false);

                YellowP.color = Color.gray;
                YellowP.transform.GetChild(1).gameObject.SetActive(false);

                PinkP.color = Color.gray;
                PinkP.transform.GetChild(1).gameObject.SetActive(false);

                GreenP.color = Color.gray;
                GreenP.transform.GetChild(1).gameObject.SetActive(false);

                OrangeP.color = Color.gray;
                OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                PurpleP.color = Color.gray;
                PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                Rect.color = BlueColor;
                Circle.color = BlueColorPick;
                break;
            case 2:
                BlueP.color = Color.gray;
                BlueP.transform.GetChild(1).gameObject.SetActive(false);

                RedP.color = Color.green;
                RedP.transform.GetChild(1).gameObject.SetActive(true);

                YellowP.color = Color.gray;
                YellowP.transform.GetChild(1).gameObject.SetActive(false);

                PinkP.color = Color.gray;
                PinkP.transform.GetChild(1).gameObject.SetActive(false);

                GreenP.color = Color.gray;
                GreenP.transform.GetChild(1).gameObject.SetActive(false);

                OrangeP.color = Color.gray;
                OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                PurpleP.color = Color.gray;
                PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                Rect.color = RedColor;
                Circle.color = RedColorPick;
                break;
            case 3:
                BlueP.color = Color.gray;
                BlueP.transform.GetChild(1).gameObject.SetActive(false);

                RedP.color = Color.gray;
                RedP.transform.GetChild(1).gameObject.SetActive(false);

                YellowP.color = Color.green;
                YellowP.transform.GetChild(1).gameObject.SetActive(true);

                PinkP.color = Color.gray;
                PinkP.transform.GetChild(1).gameObject.SetActive(false);

                GreenP.color = Color.gray;
                GreenP.transform.GetChild(1).gameObject.SetActive(false);

                OrangeP.color = Color.gray;
                OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                PurpleP.color = Color.gray;
                PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                Rect.color = YellowColor;
                Circle.color = YellowColorPick;
                break;
            case 4:
                BlueP.color = Color.gray;
                BlueP.transform.GetChild(1).gameObject.SetActive(false);

                RedP.color = Color.gray;
                RedP.transform.GetChild(1).gameObject.SetActive(false);

                YellowP.color = Color.gray;
                YellowP.transform.GetChild(1).gameObject.SetActive(false);

                PinkP.color = Color.green;
                PinkP.transform.GetChild(1).gameObject.SetActive(true);

                GreenP.color = Color.gray;
                GreenP.transform.GetChild(1).gameObject.SetActive(false);

                OrangeP.color = Color.gray;
                OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                PurpleP.color = Color.gray;
                PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                Rect.color = PinkColor;
                Circle.color = PinkColorPick;
                break;
            case 5:
                BlueP.color = Color.gray;
                BlueP.transform.GetChild(1).gameObject.SetActive(false);

                RedP.color = Color.gray;
                RedP.transform.GetChild(1).gameObject.SetActive(false);

                YellowP.color = Color.gray;
                YellowP.transform.GetChild(1).gameObject.SetActive(false);

                PinkP.color = Color.gray;
                PinkP.transform.GetChild(1).gameObject.SetActive(false);

                GreenP.color = Color.green;
                GreenP.transform.GetChild(1).gameObject.SetActive(true);

                OrangeP.color = Color.gray;
                OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                PurpleP.color = Color.gray;
                PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                Rect.color = GreenColor;
                Circle.color = GreenColorPick;
                break;
            case 6:
                BlueP.color = Color.gray;
                BlueP.transform.GetChild(1).gameObject.SetActive(false);

                RedP.color = Color.gray;
                RedP.transform.GetChild(1).gameObject.SetActive(false);

                YellowP.color = Color.gray;
                YellowP.transform.GetChild(1).gameObject.SetActive(false);

                PinkP.color = Color.gray;
                PinkP.transform.GetChild(1).gameObject.SetActive(false);

                GreenP.color = Color.gray;
                GreenP.transform.GetChild(1).gameObject.SetActive(false);

                OrangeP.color = Color.green;
                OrangeP.transform.GetChild(1).gameObject.SetActive(true);

                PurpleP.color = Color.gray;
                PurpleP.transform.GetChild(1).gameObject.SetActive(false);

                Rect.color = OrangeColor;
                Circle.color = OrangeColorPick;
                break;
            case 7:
                BlueP.color = Color.gray;
                BlueP.transform.GetChild(1).gameObject.SetActive(false);

                RedP.color = Color.gray;
                RedP.transform.GetChild(1).gameObject.SetActive(false);

                YellowP.color = Color.gray;
                YellowP.transform.GetChild(1).gameObject.SetActive(false);

                PinkP.color = Color.gray;
                PinkP.transform.GetChild(1).gameObject.SetActive(false);

                GreenP.color = Color.gray;
                GreenP.transform.GetChild(1).gameObject.SetActive(false);

                OrangeP.color = Color.gray;
                OrangeP.transform.GetChild(1).gameObject.SetActive(false);

                PurpleP.color = Color.green;
                PurpleP.transform.GetChild(1).gameObject.SetActive(true);

                Rect.color = PurpleColor;
                Circle.color = PurpleColorPick;
                break;
        }
    }

    public void LimitScrollBar(ScrollRect sc)
    {
        if (sc.horizontalScrollbar.value >= 1) sc.horizontalScrollbar.value = 1;
        if (sc.horizontalScrollbar.value <= 0) sc.horizontalScrollbar.value = 0;

        sc.horizontalNormalizedPosition = sc.horizontalScrollbar.value;
    }
}
