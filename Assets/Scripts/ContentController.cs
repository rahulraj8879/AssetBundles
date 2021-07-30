using UnityEngine;

public class ContentController : MonoBehaviour {

    public API api;
    public GameObject loaingPanelObj; 

    public void LoadContent(string name) {
        DestroyAllChildren();
        loaingPanelObj.SetActive(true);
        api.GetBundleObject(name, OnContentLoaded, transform);
    }

    void OnContentLoaded(GameObject content) {
        loaingPanelObj.SetActive(false);
        //do something cool here
        Debug.Log("Loaded: " + content.name);
    }

    void DestroyAllChildren() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}
