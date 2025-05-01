using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    public void UpdateCurrentGold(int amount = 1)
    {
        currentGold += amount;
        RefreshUI();
    }

    public bool HasEnoughGold(int amount)
    {
        return currentGold >= amount;
    }

    public void SpendGold(int amount)
    {
        currentGold -= amount;
        currentGold = Mathf.Max(currentGold, 0);
        RefreshUI();
    }

    public int GetCurrentGold()
    {
        return currentGold;
    }

    private void RefreshUI()
    {
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT)?.GetComponent<TMP_Text>();
        }

        if (goldText != null)
        {
            goldText.text = currentGold.ToString("D3");
        }
    }
}
