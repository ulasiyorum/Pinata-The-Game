using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [SerializeField] Text error;
    [SerializeField] Text treeTypeString;
    float errortime;
    [SerializeField] GameObject warning;
    public static SkillTree Instance;
    [SerializeField] Player player;
    private int treeType;
    private int type;
    [SerializeField] Image[] backgrounds;
    private int[] levels1;
    private int[] levels2;
    private int[] levels3;
    [SerializeField] GameObject treeImage;
    private bool x;
    // level 5 = max
    void Start()
    {
        levels1 = new int[6];
        levels2 = new int[6];
        levels3 = new int[6];
        errortime = 3;
        treeType = 0;
        Close();
    }
    public int getTreeType() { return treeType; }
    public Image[] getBackgrounds() { return backgrounds; }
    private void Update()
    {

        errortime += Time.deltaTime;
        if(errortime >= 2f) { error.gameObject.SetActive(false); error.text = ""; }
    }
    public void AssignType(int type)
    {
        //sýfýrlama için warning eklemeyi unutma

        if (treeType == 0)
        {
            treeType = type;
            foreach(var b in backgrounds) { b.color = Color.gray; }
            backgrounds[treeType - 1].color = Color.green;
            for(int i = 0; i < levels1.Length; i++) { levels1[i] = 0; }
            for (int i = 0; i < levels2.Length; i++) { levels2[i] = 0; }
            for (int i = 0; i < levels3.Length; i++) { levels3[i] = 0; }
            player.resetTree();
        }
        else if (player.getBalance() >= 1000 && treeType != type) { player.withdraw(1000); treeType = type;
            player.resetTree();
            foreach (var b in backgrounds) { b.color = Color.gray; }
            backgrounds[treeType - 1].color = Color.green;
            for (int i = 0; i < levels1.Length; i++) { levels1[i] = 0; }
            for (int i = 0; i < levels2.Length; i++) { levels2[i] = 0; }
            for (int i = 0; i < levels3.Length; i++) { levels3[i] = 0; }
        }
        else { Debug.Log("Error: Not Enough Balance or Same Type"); }
    // 1 => Enerji Yolu
    // 2 => Looting Yolu
    // 3 => Þans Yolu
    }
    public void purchaseSkill(int skill)
    {
        if(treeType == 0)
        {       error.gameObject.SetActive(true);
                error.text = "you dont have requirements";
                errortime = 0;
            return; }
        else if(treeType == 1)
        {
            if (skill == 0 && player.getBalance() >= ((levels1[0] + 1) * 1200))
            {
                player.PurchaseSkill("energytime", levels1[0]);
                
                if (levels1[0] < 5)
                {
                    levels1[0]++;
                    player.withdraw(1200 * levels1[0]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
                // level 1 => energy refill 9
                // level 2 => energy refill 8
                // level 3 => energy refill 7
                // level 4 => energy refill 5
                // level 5 => energy refill 3
            }
            else if (skill == 1 && player.getBalance() >= ((levels1[1] + 1) * 1600) && levels1[0] == 5)
            {
                player.PurchaseSkill("energymultiplier", levels1[1]);
               
                if (levels1[1] < 5)
                {
                    levels1[1]++;
                    player.withdraw(1600 * levels1[1]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 2 && player.getBalance() >= ((levels1[2] + 1) * 1900) && levels1[1] == 5)
            {
                // harcadýðýn enerji sayýsý * makeitworth / 20 (max seviye => harcadýðýn enerjinin yarýsý balance'a eklenir)
                player.PurchaseSkill("makeitworth", levels1[2]);
                
                if (levels1[2] < 5)
                {
                    levels1[2]++;
                    player.withdraw(1900 * levels1[2]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 3 && player.getBalance() >= 3500 && levels1[2] == 5)
            {
                player.PurchaseSkill("wrapgod", levels1[3]);
                
                if (levels1[3] < 5)
                {
                    levels1[3] = 5;
                    player.withdraw(3500);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
                // wrapbot 2 kat hýzlý çalýþýr
                // wrapbot'a ekstra perk: her baþarýlý 3 vuruþ için automaton çalýþýr w/o resetting timer

            }
            else if (skill == 4 && player.getBalance() >= (2500 * (levels1[4] + 1)) && levels1[3] == 5)
            {
                // MiniGame'lerden aldýðýn enerjiyi
                // ilk 3sv => +5%
                // 4 => +10%
                // 5 => +15%
                player.PurchaseSkill("minigamer", levels1[4]);
                
                if (levels1[4] < 5)
                {
                    levels1[4]++;
                    player.withdraw(2500 * levels1[4]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 5 && player.getBalance() >= 20000 && levels1[4] == 5)
            {
                // Enerjini Yeniler (max enerjiye) 560 saniye cooldown
                player.PurchaseSkill("laststand", levels1[5]);
                
                if (levels1[5] < 5)
                {
                    levels1[5] = 5;
                    player.withdraw(20000);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else
            {
                error.gameObject.SetActive(true);
                error.text = "you dont have requirements";
                errortime = 0;
            }
        }
        else if(treeType == 2)
        {
            if(skill == 0 && player.getBalance() >= (levels2[0] + 1) * 1550)
            { 
                player.PurchaseSkill("lootcap", levels2[0]);
                
                //looting sýnýrýný 5'e kadar arttýrýr
                if (levels2[0] < 5)
                {
                    levels2[0]++;
                    player.withdraw(1550 * levels2[0]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if(skill == 1 && player.getBalance() >= (levels2[1] + 1) * 1980 && levels2[0] == 5)
            {
                player.PurchaseSkill("efficiency", levels2[1]);
                //looting'in etkisini %50'ye kadar arttýrýr, max level'inde looting'i 1 arttýrýr
                if (levels2[1] < 5)
                {
                    levels2[1]++;
                    player.withdraw(1980 * levels2[1]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if(skill == 2 && player.getBalance() >= ((levels2[2] + 1) * 2400) && levels2[1] == 5)
            {
                //looting'in 105'e oranýna göre attack speed'ini arttýrýr
                player.PurchaseSkill("attackspeed", levels2[2]);
                if (levels2[2] < 5)
                {
                    levels2[2]++;
                    player.withdraw(2400 * levels2[2]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if(skill == 3 && player.getBalance() >= 4000 && levels2[2] == 5)
            {
                // rhea'yý güçlendirir
                // Impatient perk'indeki sayýlarý x attack speed katýna çýkarýr
                // Fairy var ve son seviye ise fairy'nin tek atýnca þekeri arttýrma özelliðini kullanýr
                // Yeni Perk => Combo (6 saniye içerisinde patlattýðý her pinata için looting'i 1 arttýrýr 
                player.PurchaseSkill("rheagod", levels2[3]);
                if (levels2[3] < 5)
                {
                    levels2[3] = 5;
                    player.withdraw(4000);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if(skill == 4 && player.getBalance() >= (3000 * (levels2[4] + 1)) && levels2[3] == 5)
            {
                // minigamelerden aldýðýn þekeri ilk 3 sv => +5%
                // 4=> +%10
                // 5=> +%15 arttýrýr
                player.PurchaseSkill("minigamer2", levels2[4]);

                if (levels2[4] < 5)
                {
                    levels2[4]++;
                    player.withdraw(3000 * levels2[4]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if(skill == 5 && player.getBalance() >= 25000 && levels2[4] == 5)
            {
                player.PurchaseSkill("rush", levels2[5]);
                if (levels2[5] < 5)
                {
                    levels2[5] = 5;
                    player.withdraw(25000);
                    AudioManager.PlaySound("bought");
                    //enerji harcanmaz ve tüm statlarý fuller ve kendi kendine saldýrýr (12 sn)
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else
            {
                error.gameObject.SetActive(true);
                error.text = "you dont have requirements";
                errortime = 0;
            }
        }
        else if(treeType == 3)
        {
            if (skill == 0 && player.getBalance() >= (levels3[0] + 1) * 1900)
            {
                player.PurchaseSkill("clover", levels3[0]);

                //belirli bir þans ile (1/5, her sv +1) candies per hit'te max'a yakýn almaný saðlar
                if (levels3[0] < 5)
                {
                    levels3[0]++;
                    player.withdraw(1900 * levels3[0]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 1 && player.getBalance() >= (levels3[1] + 1) * 2200 && levels3[0] == 5)
            {
                player.PurchaseSkill("trickortreat", levels3[1]);
                // Bir sandýk düþüyor ve sandýðýn içinden þeker çýkabiliyor ya da canavar çýkýp þekerini çalabiliyor
                // her sv +%0.1x(SV/2) þans
                // her vuruþtan sonra = 4 * ln(0.00000000217x^{15}) ) + 40
                if (levels3[1] < 5)
                {
                    levels3[1]++;
                    player.withdraw(2200 * levels3[1]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 2 && player.getBalance() >= ((levels3[2] + 1) * 2700) && levels3[1] == 5)
            {
                // Coin düþürme þansý (10.000'de 1 x (4 * level))
                player.PurchaseSkill("treasure", levels3[2]);
                if (levels3[2] < 5)
                {
                    levels3[2]++;
                    player.withdraw(2700 * levels3[2]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 3 && player.getBalance() >= 5200 && levels3[2] == 5)
            {
                // ladybug'u güçlendirir
                // Durability harcamayý kaldýrýr
                // Secret treasure'lar her bulduðunda daha fazla kazandýrýr, ve networth'una eklenir
                player.PurchaseSkill("godofbugs", levels2[3]);
                if (levels3[3] < 5)
                {
                    levels3[3] = 5;
                    player.withdraw(5200);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 4 && player.getBalance() >= (3250 * (levels3[4] + 1)) && levels3[3] == 5)
            {
                // minigamelerden aldýðýn coini ilk 3 sv => +5%
                // 4=> +%10
                // 5=> +%15 arttýrýr
                player.PurchaseSkill("minigamer3", levels3[4]);

                if (levels3[4] < 5)
                {
                    levels3[4]++;
                    player.withdraw(3000 * levels3[4]);
                    AudioManager.PlaySound("bought");
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else if (skill == 5 && player.getBalance() >= 50000 && levels3[4] == 5)
            {
                player.PurchaseSkill("arachno", levels3[5]);
                if (levels3[5] < 5)
                {
                    levels3[5] = 5;
                    player.withdraw(50000);
                    AudioManager.PlaySound("bought");
                    //Bir Boss Spawnlanýr. Ve kestiðinde þansýna göre azalarak artan fonk x boss kesme sayýn kadar loot verir.
                    //Boss spawnlamadan önce ad'si yüksek eþya almak tavsiye edilir.
                }
                else
                {
                    error.gameObject.SetActive(true);
                    error.text = "Already Maxed Level";
                    errortime = 0;
                }
            }
            else
            {
                error.gameObject.SetActive(true);
                error.text = "you dont have requirements";
                errortime = 0;
            }
        }
        else
        {
            error.gameObject.SetActive(true);
            error.text = "you dont have requirements";
            errortime = 0;
        }
    }
    public int[] getLevels1() { return levels1; }
    public int[] getLevels2() { return levels2; }
    public int[] getLevels3() { return levels3; }
    public void setTreeType(int set) { treeType = set; }

    public void Open()
    {
        treeImage.SetActive(true);
    }
    public void Close()
    {
        treeImage.SetActive(false);
    }
    public void Switch(int t)
    {
        if(treeType == 0) { AssignType(t);return; }
        if(player.getBalance() >= 1000 && t != treeType)
        {
            warning.SetActive(true);
            type = t;
        }
        else
        {
            error.gameObject.SetActive(true);
            error.text = "not enough balance or already same type";
            errortime = 0f;
        }
    }
    public void ConfirmSwitch()
    {
        warning.SetActive(false);
        AssignType(type);
    }
    public void GoBack()
    {
        warning.SetActive(false);
    }
}
