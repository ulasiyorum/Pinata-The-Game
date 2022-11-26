using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    private string sceneID = "";
    private static int leaderboardposition;
    string playfabID;
    [SerializeField] Text Saving;
    [SerializeField] bool saving = false;
    [SerializeField] public Text message;
    [SerializeField] InputField emailInput;
    [SerializeField] InputField passwordInput;
    [SerializeField] Player player;
    [SerializeField] GameObject info;
    [SerializeField] Toggle rememberMeToggle;
    private ActualPlayerData playerData;
    public float messageTimer;
    bool open = false;
    public bool rememberMe;
    public void Start()
    {
        playerData = new ActualPlayerData(player);
        messageTimer = 0;
#if PLATFORM_ANDROID && !UNITY_EDITOR
        if (!PlayFabClientAPI.IsClientLoggedIn()) {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            string android_id = secure.CallStatic<string>("getString", contentResolver, "android_id");

            var request = new LoginWithAndroidDeviceIDRequest
            {
                TitleId = "DC5AD",
                AndroidDeviceId = android_id,
                AndroidDevice = SystemInfo.deviceModel,
                CreateAccount = false,
                OS = SystemInfo.operatingSystem
            };
            message.text = "Please wait..";
            PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnError);
        }
#endif
    }
    public void Update()
    {
        if (!open)
        {
            if (message.text.Contains("HTTP") || message.text.Contains("http"))
            {
                messageTimer = 20;
            }
            if (message.text == "Registered and logged in!" || message.text == "Logged in!" || message.text == "Please wait..")
            {
                messageTimer += Time.deltaTime;
            }
            if (messageTimer > 4)
            {
                open = true;
                messageTimer = 0;
            }
        }
        else if(saving)
        {
            Saving.text = "Saving game, please dont quit.";
        }
        else
        {
            messageTimer += Time.deltaTime;
            Saving.text = "";
            if (messageTimer > 4)
            {
                message.text = "";
                messageTimer = 0;
            }
        }
    }
    public void Register()
    {
        if (passwordInput.text.Length < 6) { message.text = "Password too short"; }
        else {
            var request = new RegisterPlayFabUserRequest
            {
                Username = emailInput.text,
                Password = passwordInput.text,
                RequireBothUsernameAndEmail = false
            };
            message.text = "Please wait..";
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
    }
    void LinkSuc(LinkAndroidDeviceIDResult result)
    {

    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        playfabID = result.PlayFabId;
        message.text = "Registered and logged in!";
#if PLATFORM_ANDROID && !UNITY_EDITOR
        if (rememberMe)
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            string android_id = secure.CallStatic<string>("getString", contentResolver, "android_id");
            var request0 = new LinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = android_id,
                AndroidDevice = SystemInfo.deviceModel.ToString(),
                ForceLink = true,
                OS = SystemInfo.operatingSystem
            };
            PlayFabClientAPI.LinkAndroidDeviceID(request0, LinkSuc, OnError);
        }
#endif
        player.LoginMenuFunction();
        info.GetComponent<Info>().activateButton();
        

    }
    void OnError(PlayFabError error)
    {
        if(error.ErrorMessage.ToLower().Contains("available"))
        {
            Login();
            return;
        }
        else if(error.ErrorMessage.ToLower().Contains("user not found"))
        {
            return;
        }
        message.text = error.ErrorMessage;
    }
    public void Login()
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = emailInput.text,
            Password = passwordInput.text
        };
        message.text = "Please wait..";
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }
    void OnLoginSuccess(LoginResult result)
    {
        playfabID = result.PlayFabId;
        message.text = "Logged in!";
#if PLATFORM_ANDROID && !UNITY_EDITOR
        if (rememberMe)
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            string android_id = secure.CallStatic<string>("getString", contentResolver, "android_id");
            var request0 = new LinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = android_id,
                AndroidDevice = SystemInfo.deviceModel.ToString(),
                ForceLink = true,
                OS = SystemInfo.operatingSystem
            };
            PlayFabClientAPI.LinkAndroidDeviceID(request0, LinkSuc, OnError);
        }
#endif
        Load();
    }
    public ActualPlayerData getPlayerData()
    {
        return playerData;
    }
    public void Save()
    {
        saving = true;
        System.DateTimeOffset exitDateTime = System.DateTimeOffset.Now;
        int exitTime = (exitDateTime.Year
            * 365 * 24 * 60 * 60 + exitDateTime.Month * 30 * 24 * 60 * 60 + exitDateTime.Day * 24 * 60 * 60 + exitDateTime.Hour
            * 60 * 60 + exitDateTime.Minute * 60 + exitDateTime.Second);
        playerData = new ActualPlayerData(player);
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { 
                {"timer", "" + playerData.getTimer() },
                {"respawntimer", "" + playerData.getRespawnTimer() },
                {"attackrange", "" + playerData.getAttackRange() },
                {"enemyhealth", "" + playerData.getEnemyHealth() },
                {"balance", "" + playerData.getBalance() },
                {"coins", "" + playerData.getCoins() },
                {"looting", "" + playerData.getLooting() },
                {"playerDamage", "" + playerData.getPlayerDamage() },
                {"attackDamage", "" + playerData.getAttackDamage() },
                {"attackSpeed", "" + playerData.getAttackSpeed() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"toolDurability", "" + playerData.getToolDurability() },
                {"energy", "" + playerData.getEnergy() },
                {"maxEnergy", "" + playerData.getMaxEnergy() },
                {"epa", "" + playerData.getEPA() },
                {"toolEPA", "" + playerData.getToolEPA() },
                {"isDead", "" + playerData.getIsDead() },
                {"locationx", "" + playerData.getInventoryScenesString() },
                {"locationy", "" + playerData.getNetworth() },
                {"locationz", "" + AdsManager.adCount },
                {"exitTime", "" + exitTime },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"playForFree", "" + playerData.getPlayForFree()},
                {"freeTimer", "" + playerData.getFreeTimer() },
                {"playfabID", "" + playerData.getPlayFabID() },
                {"miniGameMultiplier3", "" + playerData.getMiniGameMultiplier3()  },
                {"bossLevel", "" + playerData.getBossLevel() },
                {"treeBonus", "" + playerData.getTreeBonus() },
                {"objTimer",  "" + playerData.getObjTimer() },
                {"dailyObj", "" + playerData.getDailyObj() },
                {"coinChance", "" + playerData.getCoinChance() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"treeType", "" + playerData.getTreeType()},
                {"levels1", "" + playerData.getLevels1String() },
                {"levels2", "" + playerData.getLevels2String() },
                {"levels3", "" + playerData.getLevels3String() },
                {"popped", "" + playerData.getPopped() }
           
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"energyAttribute", "" + playerData.getEnergyAttribute()  },
                {"energyTimer", "" + playerData.getEnergyTimer() },
                {"makeitworth", "" + playerData.getMakeItWorth() },
                {"wrapgod", "" + playerData.getWrapGod() },
                {"skills", "" + playerData.getSkillsString() },
                {"skillTimer", "" + playerData.getSkillTimer() },
                {"lootingCap", "" + playerData.getLootingCap() },
                {"lootEfficiency", "" + playerData.getEfficiency() },
                {"extraLooting", "" + playerData.getExtraLooting() },
                {"extraAttackSpeed", "" + playerData.getExtraAs() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"rheagod", "" + playerData.getRheaGod()  },
                {"miniGameMultiplier", "" + playerData.getMiniGameMultiplier() },
                {"miniGameMultiplier2", "" + playerData.getMiniGameMultiplier2() },
                {"cloverChance", "" + playerData.getCloverChance() },
                {"trickOrTreat", "" + playerData.getTrickOrTreat() },
                {"trickOrTreatChance", "" + playerData.getTrickOrTreatChance() },
                {"trickOrTreatRandom", "" + playerData.getTrickOrTreatRandom() },
                {"totCount", "" + playerData.getTotCount() },
                {"godOfBugs", "" + playerData.getGodOfBugs() },
                {"treasureCount", "" + playerData.getTreasureCount() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"loot", "" + playerData.getLoot()  },
                {"lootrange0", "" + playerData.getLootRange0() },
                {"lootrange1", "" + playerData.getLootRange1() },
                {"health", "" + playerData.getHealth() },
                {"_this", "" + playerData.getThis() },
                {"respawnTime", "" + playerData.getRespawnTime() },
                {"tempRespawnTime", "" + playerData.getTempRespawnTime() },
                {"hasTool", "" + playerData.getHasTool() },
                {"toolID", "" + playerData.getToolID() },
                {"tempAD", "" + playerData.getTempAD() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"inventoryPinata", "" + playerData.getInventoryPinataString() },
                {"inventoryPet", "" + playerData.getInventoryPetString() },
                {"levels", "" + playerData.getLevelsString() },
                {"equipped", "" + playerData.getEquippedString() },
                {"oncePet3", "" + playerData.getOncePet3() },
                {"moneyToPay", "" + playerData.getMoneyToPay() },
                {"loanCount", "" + playerData.getLoanCount() },
                {"bonusCounter", "" + playerData.getBonusCounter() },
                {"bonusTimer", "" + playerData.getBonusTimer() } //9 oldu
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSendFinal, OnError);
        Debug.Log("Saving");
    }
    public void SaveScene(int a)
    {
        saving = true;
        System.DateTimeOffset exitDateTime = System.DateTimeOffset.Now;
        int exitTime = (exitDateTime.Year
            * 365 * 24 * 60 * 60 + exitDateTime.Month * 30 * 24 * 60 * 60 + exitDateTime.Day * 24 * 60 * 60 + exitDateTime.Hour
            * 60 * 60 + exitDateTime.Minute * 60 + exitDateTime.Second);
        playerData = new ActualPlayerData(player);
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"timer", "" + playerData.getTimer() },
                {"respawntimer", "" + playerData.getRespawnTimer() },
                {"attackrange", "" + playerData.getAttackRange() },
                {"enemyhealth", "" + playerData.getEnemyHealth() },
                {"balance", "" + playerData.getBalance() },
                {"coins", "" + playerData.getCoins() },
                {"looting", "" + playerData.getLooting() },
                {"playerDamage", "" + playerData.getPlayerDamage() },
                {"attackDamage", "" + playerData.getAttackDamage() },
                {"attackSpeed", "" + playerData.getAttackSpeed() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"toolDurability", "" + playerData.getToolDurability() },
                {"energy", "" + playerData.getEnergy() },
                {"maxEnergy", "" + playerData.getMaxEnergy() },
                {"epa", "" + playerData.getEPA() },
                {"toolEPA", "" + playerData.getToolEPA() },
                {"isDead", "" + playerData.getIsDead() },
                {"locationx", "" + playerData.getInventoryScenesString() },
                {"locationy", "" + playerData.getNetworth() },
                {"locationz", "" + AdsManager.adCount },
                {"exitTime", "" + exitTime },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"playForFree", "" + playerData.getPlayForFree()},
                {"freeTimer", "" + playerData.getFreeTimer() },
                {"playfabID", "" + playerData.getPlayFabID() },
                {"miniGameMultiplier3", "" + playerData.getMiniGameMultiplier3()  },
                {"bossLevel", "" + playerData.getBossLevel() },
                {"treeBonus", "" + playerData.getTreeBonus() },
                {"objTimer",  "" + playerData.getObjTimer() },
                {"dailyObj", "" + playerData.getDailyObj() },
                {"coinChance", "" + playerData.getCoinChance() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"treeType", "" + playerData.getTreeType()},
                {"levels1", "" + playerData.getLevels1String() },
                {"levels2", "" + playerData.getLevels2String() },
                {"levels3", "" + playerData.getLevels3String() },
                {"popped", "" + playerData.getPopped() }

            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"energyAttribute", "" + playerData.getEnergyAttribute()  },
                {"energyTimer", "" + playerData.getEnergyTimer() },
                {"makeitworth", "" + playerData.getMakeItWorth() },
                {"wrapgod", "" + playerData.getWrapGod() },
                {"skills", "" + playerData.getSkillsString() },
                {"skillTimer", "" + playerData.getSkillTimer() },
                {"lootingCap", "" + playerData.getLootingCap() },
                {"lootEfficiency", "" + playerData.getEfficiency() },
                {"extraLooting", "" + playerData.getExtraLooting() },
                {"extraAttackSpeed", "" + playerData.getExtraAs() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"rheagod", "" + playerData.getRheaGod()  },
                {"miniGameMultiplier", "" + playerData.getMiniGameMultiplier() },
                {"miniGameMultiplier2", "" + playerData.getMiniGameMultiplier2() },
                {"cloverChance", "" + playerData.getCloverChance() },
                {"trickOrTreat", "" + playerData.getTrickOrTreat() },
                {"trickOrTreatChance", "" + playerData.getTrickOrTreatChance() },
                {"trickOrTreatRandom", "" + playerData.getTrickOrTreatRandom() },
                {"totCount", "" + playerData.getTotCount() },
                {"godOfBugs", "" + playerData.getGodOfBugs() },
                {"treasureCount", "" + playerData.getTreasureCount() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"loot", "" + playerData.getLoot()  },
                {"lootrange0", "" + playerData.getLootRange0() },
                {"lootrange1", "" + playerData.getLootRange1() },
                {"health", "" + playerData.getHealth() },
                {"_this", "" + playerData.getThis() },
                {"respawnTime", "" + playerData.getRespawnTime() },
                {"tempRespawnTime", "" + playerData.getTempRespawnTime() },
                {"hasTool", "" + playerData.getHasTool() },
                {"toolID", "" + playerData.getToolID() },
                {"tempAD", "" + playerData.getTempAD() },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
                {"inventoryPinata", "" + playerData.getInventoryPinataString() },
                {"inventoryPet", "" + playerData.getInventoryPetString() },
                {"levels", "" + playerData.getLevelsString() },
                {"equipped", "" + playerData.getEquippedString() },
                {"oncePet3", "" + playerData.getOncePet3() },
                {"moneyToPay", "" + playerData.getMoneyToPay() },
                {"loanCount", "" + playerData.getLoanCount() },
                {"bonusCounter", "" + playerData.getBonusCounter() },
                {"bonusTimer", "" + playerData.getBonusTimer() } //9 oldu
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSendFinal, OnError);
        Debug.Log("Saving");
        sceneID = "" + a;
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        
    }
    void OnDataSendFinalScene(UpdateUserDataResult r)
    {
        SendLeaderboard(player.getNetworth());
        saving = false;
        SceneManager.LoadScene("MiniGame"+sceneID);
    }
    void OnDataSendFinal(UpdateUserDataResult r)
    {
        SendLeaderboard(player.getNetworth());
        saving = false;
    }
    public void Load()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }
    void OnDataReceived(GetUserDataResult result)
    {
        info.GetComponent<Info>().activateButton();
        Debug.Log("User data loaded");
        Debug.Log(result.Data.Count);
        playerData.readData(result);
        if (playerData.getPlayFabID() != "") { playfabID = playerData.getPlayFabID(); }
        player.StartMenuFunction();
    }

    public void toggle()
    {
        if (rememberMeToggle.isOn)
        {
            rememberMe = true;
        }
        else
        {
            rememberMe = false;
        }
    }

    public void SignOut()
    {
#if PLATFORM_ANDROID // && !UNITY_EDITOR
        var request = new UnlinkAndroidDeviceIDRequest
        {

        };
        PlayFabClientAPI.UnlinkAndroidDeviceID(request, UnlinkSuc, OnErrorUnlink);
#endif
    }
    void OnErrorUnlink(PlayFabError error)
    {
        PlayFabClientAPI.ForgetAllCredentials();
        messageTimer = 0;
        message.text = "Successfully signed out, please restart the game.";
    }

    void UnlinkSuc(UnlinkAndroidDeviceIDResult res) {
        PlayFabClientAPI.ForgetAllCredentials();
        messageTimer = 0;
        message.text = "Successfully signed out, please restart the game.";
    }

    // Leaderboard Methods

    public void SendLeaderboard(double score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {

                new StatisticUpdate
                {
                StatisticName = "Networth",
                Value = (int)score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
       
        
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("lb sent");
    }

    public void GetLeaderboard()
    {
        //SendLeaderboard(player.getNetworth());
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Networth",
            StartPosition = 0,
            MaxResultsCount = 39,
            
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach(var item in result.Leaderboard)
        {
            if (item.PlayFabId == playfabID)
            {
                leaderboardposition = item.Position;
                break;
            }
            else
            {
                leaderboardposition = -1;
            }
        }
        Debug.Log(leaderboardposition);
    }
    public static int getLeaderboardPosition()
    {
        return leaderboardposition + 1;
    }
    public string getPlayFabID() { return this.playfabID; }
}
