using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class API : MonoBehaviour {

    const string BundleFolder = "http://192.168.0.104:8080/AssetBundles/";
    //string url;
      

    public void GetBundleObject(string assetName, UnityAction<GameObject> callback, Transform bundleParent) {
        StartCoroutine(GetDisplayBundleRoutine(assetName, callback, bundleParent));
    }

    IEnumerator GetDisplayBundleRoutine(string assetName, UnityAction<GameObject> callback, Transform bundleParent) {



        //if (assetName== "whale")
        //{
        //    url = "https://download2282.mediafire.com/erg5vw8781ig/26xhulyymittwhh/whale-Android";
        //}

        //if (assetName == "spider")
        //{
        //    url = "https://download2279.mediafire.com/4agsgq9yi6og/8vflpfg60d3fxpa/spider-Android";
        //}
        //if (assetName == "avatar")
        //{
        //    url = "https://download1350.mediafire.com/a6y5t150csbg/keprzv6yfoygvkz/avatar-Android";

        //}


        string bundleURL = BundleFolder + assetName + "-";

        //append platform to asset bundle name
#if UNITY_ANDROID
        bundleURL += "Android";
#else
                bundleURL += "IOS";
#endif

        Debug.Log("Requesting bundle at " + bundleURL);

        //request asset bundle
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError) {
            Debug.Log("Network error");
        } else {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null) {
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject,bundleParent);
                bundle.Unload(false);
                callback(arObject);
            } else {
                Debug.Log("Not a valid asset bundle");
            }
        }
    }
}
