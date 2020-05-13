using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public float flickerMaxTime;
    public float flickerMinTime;

    private float timeToNextFlicker;
    private int hashFlicker;

    private Animator animLamp;
    // Start is called before the first frame update
    void Start()
    {
        animLamp = GetComponent<Animator>();


        hashFlicker = Animator.StringToHash("Malfunction");

        SetFlcikeringTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToNextFlicker <= 0 && !animLamp.GetBool(hashFlicker)) {
            animLamp.SetBool(hashFlicker,true);
            SetFlcikeringTime();
        }

        if (!animLamp.GetBool(hashFlicker)) {
            timeToNextFlicker -= Time.deltaTime;
        }
    }

    private void SetFlcikeringTime() {
        timeToNextFlicker = Random.Range(flickerMinTime, flickerMaxTime);
    }

    private void UnsetFlickering() {
        animLamp.SetBool(hashFlicker, false);
    }
}
