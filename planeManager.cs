using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class planeManager : MonoBehaviour
{
    // Start is called before the first frame update
    ARPlaneManager pm;
    ARPlane myPlane;
    public GameObject cubePrefab;
    AudioSource aud;
    bool firstPlane;
    ARSessionOrigin sessionOrigin;
    ARRaycastManager raycastManager;
    GameObject myCube;
    
    public TextMeshProUGUI planeDetectGUI;
    void Awake()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        raycastManager = GetComponent<ARRaycastManager>();
        firstPlane=false;
    }

    void Start()
    {
        aud=GetComponent<AudioSource>();
        pm=GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.trackables.count>0&&!firstPlane){
            foreach (var plane in pm.trackables)
                {
                    if(!firstPlane){
                        myPlane=plane;//Get the first plane Generated
                        firstPlane=true;
                    }

                }
            
            planeDetectGUI.text="Plane Detected!";//Updates GUI
            planeDetectGUI.color=Color.green;

            
            myCube=Instantiate(cubePrefab,new Vector3(0f,cubePrefab.transform.localScale.y,0f),Quaternion.identity);
            aud.Play();
            sessionOrigin.MakeContentAppearAt(myCube.transform, myPlane.center, Quaternion.identity);//Places cube at centre of the found plane
            

            myPlane.boundaryChanged+=UpdateCube;//Repositions the cube whenever the plane is resized
            }

    }

    void UpdateCube(ARPlaneBoundaryChangedEventArgs args){
        sessionOrigin.MakeContentAppearAt(myCube.transform, myPlane.center, myCube.transform.rotation);//Repositions the cube whenever the plane is resized

    }
}
