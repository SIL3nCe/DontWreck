using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShoppingManager : MonoBehaviour
{
    public Button BuyCrewmanButton;
    public Button BuyWoodButton;
    public Button BuyCoalButton;
    public Button LeaveButton;

    public int m_priceCrew = 500;
    public int m_priceWood = 50;
    public int m_priceCoal = 100;

    // Start is called before the first frame update
    void Start()
    {
        // AddListener
        BuyCrewmanButton.onClick.AddListener(BuyCrewman);
        BuyWoodButton.onClick.AddListener(BuyWood);
        BuyCoalButton.onClick.AddListener(BuyCoal);
        LeaveButton.onClick.AddListener(Leave);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BuyCrewman()
    {
        if (GameManager.m_instance.m_resourcesManager.GetjambonBoursinCount() >= m_priceCrew)
        {
            GameManager.m_instance.m_resourcesManager.AddJambonBoursin(-m_priceCrew);
            GameManager.m_instance.m_resourcesManager.AddCrew(1);
            if (GameManager.m_instance.m_resourcesManager.GetjambonBoursinCount() < m_priceCrew)
            {
                BuyCrewmanButton.interactable = false;
            }
        }
        else
        {
            BuyCrewmanButton.GetComponentInChildren<Text>().text = "Not enough JambonBoursin";
            BuyCrewmanButton.interactable = false;
        }
    }

    void BuyWood()
    {
        if (GameManager.m_instance.m_resourcesManager.GetjambonBoursinCount() >= m_priceWood)
        {
            GameManager.m_instance.m_resourcesManager.AddJambonBoursin(-m_priceWood);
            GameManager.m_instance.m_resourcesManager.AddWood(1);
            if (GameManager.m_instance.m_resourcesManager.GetjambonBoursinCount() < m_priceWood)
            {
                BuyWoodButton.interactable = false;
            }
        }
        else
        {
            BuyWoodButton.GetComponentInChildren<Text>().text = "Not enough JambonBoursin";
            BuyWoodButton.interactable = false;
        }
    }

    void BuyCoal()
    {
        if (GameManager.m_instance.m_resourcesManager.GetjambonBoursinCount() >= m_priceCoal)
        {
            GameManager.m_instance.m_resourcesManager.AddJambonBoursin(-m_priceCoal);
            GameManager.m_instance.m_resourcesManager.AddCoal(1);
            if (GameManager.m_instance.m_resourcesManager.GetjambonBoursinCount() < m_priceCoal)
            {
                BuyCoalButton.interactable = false;
            }
        }
        else
        {
            BuyCoalButton.GetComponentInChildren<Text>().text = "Not enough JambonBoursin";
            BuyCoalButton.interactable = false;
        }
    }

    void Leave()
    {

    }

	public void OnGUI()
	{
	}
}
