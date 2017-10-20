using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public Image bar;
    public Text ratioText;
    private float HP;
    private float maxHP;
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void UpdateHP ()
    {
        float ratio = HP / maxHP;
        bar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + '%';
	}

    private void TakeDamage (float damage)
    {
        HP -= damage;
        if(HP <0)
        {
            HP = 0;
            Debug.Log("DEAD!");
        }
        UpdateHP();
    }

    private void HealDamage(float heal)
    {
        HP += heal;
        if (HP > maxHP)
        {
            HP = maxHP;
            Debug.Log("FullHealth");
        }
        UpdateHP();
    }
}
