using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BossSelectionManager : MonoBehaviour
{
    public static BossSelectionManager instance = null;

    [Header("Boss Selection Settings")]
    public List<GameObject> bossComponents;
    public int bossMaxHealth;
    public int bossMaxAtk;
    public int bossMaxDef;
    private int combinedHealth;
    private int combinedAtk;
    private int combinedDef;

    [Header("Boss Stats Settings")]
    public Image BossHealthBar;
    public Image BossAtkBar;
    public Image BossDefBar;

    [Header("Part Selector Settings")]
    public Transform partSelector;
    public float partSelectorMoveAmount;
    public float partSelectorMoveSpeed;
    private bool partSelectorMoving;

    [Header("Slot Settings")]
    public float slotMoveAmount;
    public float slotMoveSpeed;

    private List<SlotHandler> slots;
    private int currentSlotIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        slots = new List<SlotHandler>();
        combinedHealth = 0;

        slots.AddRange(transform.GetComponentsInChildren<SlotHandler>());
        currentSlotIndex = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            AssignSelectedBossPart(i);
        }

        AssignBossStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePartSelectorLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePartSelectorRight();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            slots[currentSlotIndex].MovePiecesUp();
            AssignSelectedBossPart(currentSlotIndex);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            slots[currentSlotIndex].MovePiecesDown();
            AssignSelectedBossPart(currentSlotIndex);
        }
    }

    void MovePartSelectorLeft()
    {
        if (partSelectorMoving || currentSlotIndex == 0)
            return;

        partSelectorMoving = true;
        currentSlotIndex--;

        float currentXPos = partSelector.localPosition.x;
        float newXPos = currentXPos - partSelectorMoveAmount;

        partSelector.DOLocalMoveX(newXPos, partSelectorMoveSpeed).OnComplete(ResetPartSelector);
    }

    void MovePartSelectorRight()
    {
        if (partSelectorMoving || currentSlotIndex == slots.Count-1)
            return;

        partSelectorMoving = true;
        currentSlotIndex++;

        float currentXPos = partSelector.localPosition.x;
        float newXPos = currentXPos + partSelectorMoveAmount;

        partSelector.DOLocalMoveX(newXPos, partSelectorMoveSpeed).OnComplete(ResetPartSelector);
    }

    void ResetPartSelector()
    {
        partSelectorMoving = false;
    }

    void AssignSelectedBossPart(int selectedIndex)
    {
        bossComponents[selectedIndex].GetComponent<Image>().sprite = slots[selectedIndex].GetCurrentSlotPieceImage();
        AssignBossStats();
    }

    void AssignBossStats()
    {
        CalculateCombinedHealth();
        CalculateCombinedAttack();
        CalculateCombinedDefense();
    }

    void CalculateCombinedHealth()
    {
        combinedHealth = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            combinedHealth += slots[i].GetCurrentSlotPieceStats().health;
        }

        if (combinedHealth > bossMaxHealth)
            combinedHealth = bossMaxHealth;

        BossHealthBar.fillAmount = Mathf.Clamp((float)combinedHealth / (float)bossMaxHealth, 0, 1f);
        GameManager.ChosenBossHealth = combinedHealth;
    }

    void CalculateCombinedAttack()
    {
        combinedAtk = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            combinedAtk += slots[i].GetCurrentSlotPieceStats().atk;
        }

        if (combinedAtk > bossMaxAtk)
            combinedAtk = bossMaxAtk;

        BossAtkBar.fillAmount = Mathf.Clamp((float)combinedAtk / (float)bossMaxAtk, 0, 1f);
        GameManager.ChosenBossAttack = combinedAtk;
    }

    void CalculateCombinedDefense()
    {
        combinedDef = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            combinedDef += slots[i].GetCurrentSlotPieceStats().def;
        }

        if (combinedDef > bossMaxDef)
            combinedDef = bossMaxDef;

        BossDefBar.fillAmount = Mathf.Clamp((float)combinedDef / (float)bossMaxDef, 0, 1f);
        GameManager.ChosenBossDef = combinedDef;
       }
}
