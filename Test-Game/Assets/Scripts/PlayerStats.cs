using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //hp
    public float hp=100f;
    public float hp_max = 100f;
    
    // i-franes after getting hurt
    private float immunityTime;
    private float immunityTimer;
    
    // mana system
    public float mana;
    public float manaCapacity;

    private float manaSteal;
    // cast system
    public float castCost;
    public float castCouldownTime;
    public float castCouldownTimer;
    //atack system
    private float atackSpeed;
    private float atackCounter;
    
    // stamina
    public float stamina=100f, max_stamina=100f;
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (hit())
        {
            // lose hp
        }
        if (hp <= 0.0f)
        {
            //dies
        }
        cast();
        // valid mana hp stamina system 
        if (mana < 0f)
        {
            mana = 0f;
        } else if (mana > manaCapacity)
        {
            mana = manaCapacity;
        }
        
        if (hp < 0f)
        {
            hp = 0f;
        } else if (hp > hp_max)
        {
            hp = hp_max;
        }
        
        if (stamina < 0f)
        {
            stamina = 0f;
        } else if (stamina > max_stamina)
        {
            stamina = max_stamina;
        }
    }

    void die()
    {
        
    }
    
    void heal() 
    {
        
    }

    void cast()
    {
        if (castCouldownTimer <=0)
        {
            if (Input.GetButton("Cast"))
            {
                if (mana >= castCost)
                {
                    mana -= castCost;
                    castCouldownTimer = castCouldownTime;
                }
            }
        }
        else
        {
            castCouldownTimer -= Time.deltaTime;
        }
        
    }
    bool hit()
    {
        // check collision with other obj on fild then return if player was hitted 
        bool state = false;
        return state;
    }
}
