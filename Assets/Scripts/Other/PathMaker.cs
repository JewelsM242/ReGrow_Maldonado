using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker : MonoBehaviour
{   
    public List<GameObject> markers;
    public int max = 80;

    int curSize = 0;

    public GameObject marker;

    public bool buildingPath = false;

    public bool travelingPath = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public bool startTeleport()
    {
        if(travelingPath) {return false;}
        buildingPath = true;
        startSpawn(marker);
        return true;
    }

    public void startSpawn(GameObject obj){
        StartCoroutine(Spawn(obj));
    }

    private IEnumerator Spawn(GameObject obj){
        while (buildingPath){
            addMarker(obj);
            yield return new WaitForSeconds(.05f);
        }
    }

    private void addMarker(GameObject obj){
        markers.Insert(curSize, Instantiate(obj,obj.transform.position,Quaternion.identity));
        curSize++;
    }

    public void endTeleport(){
        StartCoroutine(teleportBack());
    }

    private IEnumerator teleportBack(){
        buildingPath = false;
        travelingPath = true;
        while (curSize>0 && travelingPath){
            curSize--;
            transform.position = markers[curSize].transform.position;
            Destroy(markers[curSize].gameObject);
            markers.RemoveAt(curSize);
            yield return new WaitForFixedUpdate();
        }
        clear();
        buildingPath = false;
        travelingPath = false;
        yield return new WaitForSeconds(2f);
    }

    public bool getBuildingPath(){
        return buildingPath;
    }

    public bool getTravelingPath(){
        return travelingPath;
    }

    public void interuptPath(){
        travelingPath = false;
    }

    public void clear(){
        curSize = 0;
        foreach (GameObject mark in markers){
            Destroy(mark.gameObject);
        }
        markers.Clear();
    }
}
