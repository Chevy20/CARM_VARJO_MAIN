using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo.XR;

public class StartMixedReality : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VarjoMixedReality.StartRender();
    }

    // Update is called once per frame
    void Update()
    {
    }
/**
    
    private void OnEnable()
    {
        // Enable Depth Estimation.
        VarjoMixedReality.EnableDepthEstimation();
    }

    private void OnDisable()
    {
        // Disable Depth Estimation.
        VarjoMixedReality.DisableDepthEstimation();
    }
    **/
}
