using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    public PlayerStats p;
    public Image hp_bar;
    public Image stamina_bar;
    public Image mana_bar;

    public Color firstStaminaColor;
    public Color secondStaminaColor;
    
    public Color firstManaColor;
    public Color secondManaColor;
    private float lerpAmount;
    // Start is called before the first frame update
    void Start()
    {
        p.hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        lerpAmount = 3f * Time.deltaTime;
        ChangeSize();
        ChangeColor();
    }

    void ChangeSize()
    {
        hp_bar.fillAmount = Mathf.Lerp(hp_bar.fillAmount, (p.hp / p.hp_max), lerpAmount);
        stamina_bar.fillAmount = Mathf.Lerp(stamina_bar.fillAmount, (p.stamina / p.max_stamina), lerpAmount);
        mana_bar.fillAmount = Mathf.Lerp(mana_bar.fillAmount, (p.mana / p.manaCapacity), lerpAmount);
    }

    void ChangeColor()
    {
        hp_bar.color = Color.Lerp(Color.red, Color.green, hp_bar.fillAmount);
        Color staminaColor = Color.Lerp(firstStaminaColor, secondStaminaColor, stamina_bar.fillAmount);
        Color manaColor = Color.Lerp(firstManaColor, secondManaColor, mana_bar.fillAmount);
        staminaColor.a = 255f; manaColor.a = 255f;
        stamina_bar.color = staminaColor; mana_bar.color = manaColor;
    }
}
