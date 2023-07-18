using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microgame_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject assets;
    void Start()
    {
        assets = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadMicrogame(){
        this.transform.position = FindObjectOfType<PlayerManager>().transform.position;
        assets.SetActive(true);
        foreach (Transform child in assets.transform){
            if (child.GetComponent<Microgame_Asset>() != null){
                child.GetComponent<Microgame_Asset>().mainCreate();
            }
        }
    }

    public void unloadMicrogame(){
        assets.SetActive(false);
    }

    public virtual void startMicrogame(){
        Debug.Log("Don't call this one");
    }

    public virtual void endMicrogame(){
        Debug.Log("Don't call this one");
    }
}
