using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus player;

    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerCombat playerCombat;
    [HideInInspector] public PlayerGroundCollision playerGround;
    [HideInInspector] public AudioSource audioSource;

    public Transform damageUISpawnPoint;
    public Transform followTransform;

    public int maxDamage = 50;
    public int maxEnergy = 50;
    public int maxHealth = 50;

    public int damage = 50;
    public int energy = 50;
    public int health = 50;

    public delegate void AttUI(int damage, UISliderController.SliderType slider);
    public AttUI attUI;

    void Awake()
    {
        if (player != null) {
            player = null;
        }
        player = this;

        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
        playerGround = GetComponent<PlayerGroundCollision>();
        audioSource = GetComponent<AudioSource>();

        damage = maxDamage;
        energy = maxEnergy;
        health = maxHealth;
    }

    void Update() {
        playerCombat.Attack();
        playerCombat.TimeBubbling();
        playerGround.FallPlataform();
        playerMovement.Movement();
    }


    public void AddHealth(int heal) {

        UISliderController.SliderType sliderType = UISliderController.SliderType.Health;

        if (health + heal <= maxHealth) {
            health += heal; 
        }
        else {
            health = maxHealth;
        }
        attUI?.Invoke(health, sliderType);

    }

    public void AddEnergy(int ener) {

        UISliderController.SliderType sliderType = UISliderController.SliderType.Energy;

        if (energy + ener <= maxEnergy) {
            energy += ener;
        }
        else {
            energy = maxEnergy;
        }
        attUI?.Invoke(energy, sliderType);

    }

    public void WithdrawEnergy(int ener) {

        UISliderController.SliderType sliderType = UISliderController.SliderType.Energy;

        if (energy - ener >= 0) {
            energy -= ener;
        }
        else {
            energy = 0;
        }
        attUI?.Invoke(energy, sliderType);

    }

}
