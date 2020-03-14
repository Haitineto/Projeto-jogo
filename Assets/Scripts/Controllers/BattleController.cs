using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{        
    private BattleStatus _battleStatus;

    [SerializeField]
    public Timer Timer;

    [SerializeField]
    public Keyboard Keyboard;

    [SerializeField]
    public Player Player;

    [SerializeField]
    public Boss Boss;

    [SerializeField]
    public PowerUpBar PowerUpBar;

    [SerializeField]
    public VictoryPopUp VictoryPopUp;

    [SerializeField]
    public DefeatPopUp DefeatPopUp;

    [SerializeField]
    public LevelUpPopUp LevelUpPopUp;

    [SerializeField]
    public ComboStone ComboStone;

    [SerializeField]
    public CanvasFade CanvasFade;

    private BattleInfo BattleInfo;

    private List<IPowerUp> ActivePowerUps = new List<IPowerUp>();

    private void onFinishSelection(List<Runa> selectedRunas)
    {        
        ISpell spell = SpellFactory.Create(selectedRunas[0].Element, ComboStone.Element, selectedRunas, Player, Boss, Player.AttributePoints);
        if (spell!=null)
            spell.Execute();

        if (ComboStone.Element == null)
            ComboStone.SetElement(selectedRunas[0].Element);
        else                             
            ComboStone.SetElement(null);        
    }
   
    private void afterNewRunasAreCreated(List<Runa> newRunas)
    {
        executePowerUp();
    }

    private void onSelectPowerUp(PowerUpType powerUpType)
    {
        ActivePowerUps.Add(PowerUpFactory.Create(powerUpType, this));
        executePowerUp();
    }

    private void onTimeIsOver()
    {        
        Player.Health.SetAmount(0);
        _battleStatus = BattleStatus.Defeat;
    }

    private void incCurrentBattle()
    {        
        if (BattleInfo.Id == PlayerInfo.CurrentBattle)
        {
            PlayerInfo.CurrentBattle += 1;
        }        
    }

    private void rewardPlayer(int reward, int xp)
    {
        PlayerInfo.Coins += reward;
        LevelManager.AddXp(xp);
    }

    private void onBossDie()
    {
        if (_battleStatus != BattleStatus.Running) return;

        _battleStatus = BattleStatus.Victory;
        rewardPlayer(BattleInfo.Reward.CoinsToEarn, BattleInfo.Reward.XPToEarn);
        incCurrentBattle();
        VictoryPopUp.Show(BattleInfo.Reward.CoinsToEarn, BattleInfo.Reward.XPToEarn);        
    }

    private void onPlayerDie()
    {
        if (_battleStatus != BattleStatus.Running) return;
        _battleStatus = BattleStatus.Defeat;
        DefeatPopUp.Show(BattleInfo.CoinsToContinue(), PlayerInfo.Coins);
    }

    private void executePowerUp()
    {
        if (ActivePowerUps.Count == 0) return;

        foreach (var item in ActivePowerUps)
        {
            if (item.isValid())
                item.Apply(this);
            else
                item.Revert(this);
        }        
    }    

    private void initializePowerUps()
    {
        var powerUpButtons = new PowerUpButton[4];

        powerUpButtons[0] = Instantiate(CustomResources.Load<GameObject>("PowerUps/PowerUpButton")).GetComponent<PowerUpButton>();
        powerUpButtons[0].Initialize(PowerUpType.Fire, 1, onSelectPowerUp);

        powerUpButtons[1] = Instantiate(CustomResources.Load<GameObject>("PowerUps/PowerUpButton")).GetComponent<PowerUpButton>();
        powerUpButtons[1].Initialize(PowerUpType.Water, 1, onSelectPowerUp);

        powerUpButtons[2] = Instantiate(CustomResources.Load<GameObject>("PowerUps/PowerUpButton")).GetComponent<PowerUpButton>();
        powerUpButtons[2].Initialize(PowerUpType.Earth, 1, onSelectPowerUp);

        powerUpButtons[3] = Instantiate(CustomResources.Load<GameObject>("PowerUps/PowerUpButton")).GetComponent<PowerUpButton>();
        powerUpButtons[3].Initialize(PowerUpType.Air, 1, onSelectPowerUp);

        PowerUpBar.Initialize(powerUpButtons);
    }

    private void InitializePlayer()
    {               
        Player.Initialize(PlayerInfo.CalcHealth(), PlayerInfo.AttributePoints, Boss);
        Player.Health.OnDie = onPlayerDie;
    }

    private void InitializeBoss()
    {
        var gameObjectBoss = GameObject.FindWithTag("Boss");
        
        switch (BattleInfo.BossInfo.Type)
        {
            case BattleBossType.Mystic:                
                Boss = gameObjectBoss.AddComponent<BossMystic>();
                break;
            case BattleBossType.Fighter:
                Boss = gameObjectBoss.AddComponent<BossFighter>();
                break;
            default:
                break;
        }
        
        Boss.Initialize(BattleInfo.BossInfo.Health, BattleInfo.BossInfo.AttributePoints, Player, BattleInfo.BossInfo.MaxPower);
        Boss.Health.OnDie = onBossDie;        
    }

    private void initializeKeyboard()
    {
        Keyboard.BuildKeyboard();
        Keyboard.OnFinishSelection = onFinishSelection;
        Keyboard.AfterNewRunasAreCreated = afterNewRunasAreCreated;
    }

    private void InitializeTimer()
    {
        Timer.Initialize(BattleInfo.BattleTime);

        Timer.OnTimeIsOver = onTimeIsOver;
    }

    private void InitializeComboStone()
    {
        ComboStone.Initialize();
    }

    private void InitializePopUps()
    {
        VictoryPopUp.AfterPopUpClose = afterVictoryPopUpClose;
        DefeatPopUp.AfterPopUpClose = afterDefeatPopUpClose;
        LevelUpPopUp.AfterPopUpClose = afterLevelUpPopUpClose;
        DefeatPopUp.OnUseCoinsToContinue = onUseCoinsToContinue;
    }

    private void onUseCoinsToContinue(int coinsToContinue)
    {
        PlayerInfo.Coins -= coinsToContinue;
        Player.Health.SetAmount(Player.Health.MaxAmount);        
        Timer.Initialize(BattleInfo.BattleTime);
        _battleStatus = BattleStatus.Running;
    }

    private void afterVictoryPopUpClose()
    {
        if (PlayerInfo.NewLevelReached=="S")
        {
            PlayerInfo.NewLevelReached="N";
            LevelUpPopUp.Show(PlayerInfo.Level, PlayerInfo.AvaliableAttributePoints);
        }            
        else
            backToMap();
    }    

    private void afterDefeatPopUpClose()
    {
        if (!DefeatPopUp.CoinsUsed)
        {
            backToMap();
        }
    }

    private void afterLevelUpPopUpClose()
    {
        backToMap();
    }    

    private void backToMap()
    {
        CanvasFade.StartFadeIn(loadMapScene);
    }

    private void loadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void Start()
    {        
        InitializaBattle();
    }

    public void InitializaBattle()
    {
        CanvasFade.StartFadeOut(null);

        BattleInfo = BattleInfoFactory.CreateBattleInfo(GameData.BattleToLoad);
        if (BattleInfo.BattleMusic != null)
            SoundManager.instance.PlayMusic(BattleInfo.BattleMusic);

        InitializeComboStone();

        InitializePlayer();

        InitializeBoss();

        InitializeTimer();

        initializeKeyboard();

        initializePowerUps();

        InitializePopUps();

        _battleStatus = BattleStatus.Running;
    }    

    public void Update()
    {
        if (_battleStatus == BattleStatus.Running)
        {
            Timer.Execute();

            PowerUpBar.CheckClick();

            Keyboard.HandleUserGestures();            

            Boss.DoYourStuff();

            ComboStone.ShowCurrentPreviewSpell(Keyboard.CurrentSelectedElement());
        }        
    }    
}