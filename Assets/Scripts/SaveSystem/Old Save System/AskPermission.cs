using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif



public class AskPermission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);

        }
#endif
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
#endif
    }

    private void OnGUI()
    {
    }
}
