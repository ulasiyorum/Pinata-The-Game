using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScript : MonoBehaviour
{

    [SerializeField] GameObject[] _buttons;
    [SerializeField] GameObject _image;
    [SerializeField] Player _player;
    [SerializeField] GameObject _shop;
    [SerializeField] GameObject[] _shopTs;
    [SerializeField] GameObject[] _Tpage;
    int replaceID;
    // tools = 0, pinatas = 1, pets = 2, scenes = 3, skins = 4
    [SerializeField] GameObject[] _Ttools;
    [SerializeField] GameObject[] _Tpinatas;
    [SerializeField] GameObject[] _Tscenes;
    [SerializeField] GameObject[] _Tskins;
    [SerializeField] GameObject[] _Tpets;
    public static bool isPets = false;
    public static int currentPage;






    // Start is called before the first frame update
    void Start()
    {
        _image.SetActive(false);
        foreach(var page in _shopTs) { page.SetActive(false); }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.getPinata() != null) {
            if (_player.openShop) { _player.getPinata().SetActive(false); }
        else { _player.getPinata().SetActive(true); }
    }
        if (!_player.openShop)
        {
            if (Vector2.Distance(_player.transform.position,_shop.transform.position) <= 3.5)
            {
                _player.transform.position = Vector2.zero;
                ImageAc();
                GameObject.Find("SkillUse").SetActive(false);
                GameObject.Find("OpenTree").SetActive(false);
            }
        }
        _shop.SetActive(!_player.openShop);
        if (currentPage >= _Tpage.Length)
        {
            if (replaceID == 4) { _buttons[0].SetActive(false); }
        }


        if (currentPage == 0)
        {
            isPets = false;
        }

    }


   public void ImageAc()
    {
        _image.SetActive(true);
        currentPage = 0;
        _player.shopOpen();
        AudioManager.PlaySound("pageflip");
        foreach (var page in _Tpage) { page.SetActive(false); }
        foreach(var button in _buttons) { button.SetActive(false); }
        
    }

    public void GoBack()
    {
        _image.SetActive(false);
        AudioManager.PlaySound("pageflip");
        foreach (var page in _shopTs) { page.SetActive(false); }
        _player.shopClose();
        
    }
    public void AssignOpen(int id)
    {
        foreach (var page in _Tpage) { page.SetActive(false); }
        currentPage = 0;
        replaceID = id;
        if(replaceID == 0)
        {
            _Tpage = _Ttools;
            isPets = false;
        }
        else if(replaceID == 1)
        {
            _Tpage = _Tpinatas;
            isPets = false;
        }
        else if(replaceID == 2)
        {
            _Tpage = _Tpets;
            isPets = true;
        }
        else if(replaceID == 3)
        {
            _Tpage = _Tscenes;
            isPets = false;
        }
        else if(replaceID == 4)
        {
            _Tpage = _Tskins;
            isPets = false;
        }
        foreach(var page in _Tpage) { page.SetActive(false); }
        foreach(var button in _buttons) { button.SetActive(true); }
        _shopTs[replaceID].SetActive(true);
        NextPage();
    }
    public void NextPage()
    {
        AudioManager.PlaySound("pageflip");
        if (currentPage >= _Tpage.Length) { if (replaceID < 4) { AssignOpen(++replaceID); return; } }
        _Tpage[currentPage].SetActive(true);
        if(currentPage != 0) { _Tpage[currentPage-1].SetActive(false); }
        currentPage++;
    }
    protected void PreviousPage()
    {
        AudioManager.PlaySound("pageflip");
        foreach (var button in _buttons) { button.SetActive(true); }
        if (currentPage <= 1) { ImageAc(); return; }
        currentPage--;
        _Tpage[currentPage].SetActive(false);
        _Tpage[currentPage - 1].SetActive(true);
    }
}
