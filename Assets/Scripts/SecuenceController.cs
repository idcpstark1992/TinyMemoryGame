using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript;
using TouchScript.Behaviors;
using System.Linq;
public class SecuenceController : MonoBehaviour
{
    public static SecuenceController Instance = null;

    [SerializeField] UIPresenters                       Game_UI;
    [SerializeField] SecuenceGenerationAndStorage       Secuence_GeneratorAndStorage;
    [SerializeField] private List<MemoryItem>           TotalCubesOnGame;
    [SerializeField] private int                        NumbersOfInitialCubes;
    [SerializeField] private int                        ThresholdPerRound;

    private readonly Dictionary<string, MemoryItem>     ObjectsInArray = new Dictionary<string, MemoryItem>();
    private float                                       SecuenceSpeed;
    public   bool                                       PlayerTurn { get; private set; }
    private  int                                        CurrentAnswerIndexer;
    private int                                         LevelIncreaserCounter;
    private int                                         CurrentDificulLevel;
    

    private void    Awake()
    {
        Instance = this;
    }
    private void    Start()
    {
        PlayerTurn = false;
        SecuenceSpeed = 1f;
        Secuence_GeneratorAndStorage = new SecuenceGenerationAndStorage();
        //Secuence trigger ID =  0
        Game_UI.SubscribeCallBackActions(OnCallGameStartFromUI);
        DelegatesHelpers.Register_OnHideAllObjects?.Invoke();
        CurrentDificulLevel = NumbersOfInitialCubes;
    }

    private void    OnEnable()
    {
        DelegatesHelpers.Register_OnReset += OnResetGame;
    }
    private void    OnDisable()
    {
        DelegatesHelpers.Register_OnReset -= OnResetGame;
    }
    
    private void    OnResetGame()
    {
        
        Secuence_GeneratorAndStorage.OnResetGame();
        CurrentAnswerIndexer    = 0;
        LevelIncreaserCounter   = 0;
        CurrentDificulLevel     = NumbersOfInitialCubes;
        Game_UI.OnNewRoundPrint("0");
    }
    public  void    RegisterElement(string _elementName, MemoryItem _ItemMemory)
    {
        if (!ObjectsInArray.ContainsKey(_elementName))
            ObjectsInArray.Add(_elementName, _ItemMemory);
    }
    public  void    RemoveElement(string _elementName)
    {
        Debug.Log($"Removing element  {_elementName} ");
        if (ObjectsInArray.ContainsKey(_elementName))
            ObjectsInArray.Remove(_elementName);
    }
    private void    CheckLevelIncreasing()
    {

        if (CurrentDificulLevel < TotalCubesOnGame.Count)
        {
            LevelIncreaserCounter++;
            if (LevelIncreaserCounter >= ThresholdPerRound)
            {
                LevelIncreaserCounter = 0;


                TotalCubesOnGame[CurrentDificulLevel].Emerge(0);
                CurrentDificulLevel++;
            }
        }

       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CheckLevelIncreasing();
    }
    public  void    CheckUserAnswerInput(string _InputName)
    {
        if (PlayerTurn)
        {
            var mSecuence = Secuence_GeneratorAndStorage.GetSecuences();
            if (_InputName.Equals(mSecuence[CurrentAnswerIndexer]))
            {
                CurrentAnswerIndexer++;
                Game_UI.OnNewRoundPrint(CurrentAnswerIndexer.ToString());
            }
            else
            {
                Game_UI.OnFailedQuestion();
                PlayerTurn = false;
            }
            if (CurrentAnswerIndexer >= mSecuence.Count)
            {
                CheckLevelIncreasing();
                PlayerTurn = false;
                CurrentAnswerIndexer = 0;
                StartCoroutine(C_NextRutine());
            }
        }
        
    }
    public void     OnNextLevel()
    {
        
        List<string> mAviableArrays = ObjectsInArray.Keys.ToList();
        Secuence_GeneratorAndStorage.AddToSecuence(mAviableArrays[Random.Range(0, mAviableArrays.Count)]);
        StartCoroutine(C_ReproduceSecuence());
    }
    public void     OnCallGameStartFromUI()
    {
        StartCoroutine(C_OnStartNewRound());
    }

    IEnumerator     C_NextRutine()
    {
        yield return new WaitForSeconds(1.1f);
        OnNextLevel();
    }
    IEnumerator     C_ReproduceSecuence()
    {
        var mSecuence = Secuence_GeneratorAndStorage.GetSecuences();
        for (int i = 0; i < mSecuence.Count; i++)
        {
            AnimateSecuence(mSecuence[i]);
            yield return new WaitForSeconds(SecuenceSpeed);
        }
        PlayerTurn = true;
    }
    IEnumerator     C_OnStartNewRound()
    {
        PlayerTurn = false;
        float _initialDelay = 0;
        for (int i = 0; i < NumbersOfInitialCubes; i++)
        {
            _initialDelay += .2f;
            TotalCubesOnGame[i].Emerge(_initialDelay);
        }

        int InitialCounter = 3;

        while (InitialCounter>0)
        {
            Game_UI.OnCountDown(InitialCounter.ToString());
            InitialCounter--;
            yield return new WaitForSeconds(1f);
        }
        OnNextLevel();
    }
    private void    AnimateSecuence(string _MemoryItemTag)
    {
        if (ObjectsInArray.TryGetValue(_MemoryItemTag, out MemoryItem _memoryItem))
            _memoryItem.BlinkOnSelected();
    }

  
}
