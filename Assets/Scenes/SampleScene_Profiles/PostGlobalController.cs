using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostGlobalController : MonoBehaviour
{
    // Start is called before the first frame update
    private PostProcessVolume globalVolume;

    private void Awake()
    {
        globalVolume = GetComponent<PostProcessVolume>();
    }
    void Start()
    {
        AnimationAndoMovementController.onKeyNear += statusColorFX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void statusColorFX(bool status)
    {
        ColorGrading colorFx;
        globalVolume.profile.TryGetSettings(out colorFx);
        colorFx.active = status;

    }
}
