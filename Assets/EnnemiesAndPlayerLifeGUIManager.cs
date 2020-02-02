using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemiesAndPlayerLifeGUIManager : MonoBehaviour
{
	[Header("GUI")]
	public Image m_enemiesLifeBar;
	public Image m_playerLifeBar;
	public Text m_enemiesLifeText;
	public Text m_playerLifeText;

	public void SetEnemiesHP(int hp, int maxhp)
	{
		m_enemiesLifeBar.fillAmount = (float)((float)hp / (float)maxhp);
		m_enemiesLifeText.text = "Enemies : " + hp + "/" + maxhp;
	}

	public void SetPlayerHP(int hp, int maxhp)
	{
		m_playerLifeBar.fillAmount = (float)((float)hp / (float)maxhp);
		m_playerLifeText.text = "Your boat : " + hp + "/" + maxhp;
	}
}
