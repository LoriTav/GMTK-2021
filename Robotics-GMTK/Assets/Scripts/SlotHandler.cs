using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlotHandler : MonoBehaviour
{
    private List<Transform> slotPieces;
    private GameObject currentSlotPiece;

    bool slotMoving;

    private void Awake()
    {
        slotPieces = new List<Transform>();

        foreach (Transform child in transform)
        {
            slotPieces.Add(child);
        }

        currentSlotPiece = slotPieces[0].gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePiecesUp()
    {
        if (slotMoving)
            return;

        slotMoving = true;
        currentSlotPiece = slotPieces[1].gameObject;

        for (int i = 0; i < slotPieces.Count; i++)
        {
            float currentYPos = slotPieces[i].localPosition.y;
            float newYPos = currentYPos + BossSelectionManager.instance.slotMoveAmount;

            if(i == 0)
            slotPieces[i].DOLocalMoveY(newYPos, BossSelectionManager.instance.slotMoveSpeed).OnComplete(ResetTopSlot);

            if (i != 0)
                slotPieces[i].DOLocalMoveY(newYPos, BossSelectionManager.instance.slotMoveSpeed).OnComplete(ResetSlot);
        }
    }

    public void MovePiecesDown()
    {
        if (slotMoving)
            return;

        slotMoving = true;

        slotPieces[slotPieces.Count - 1].localPosition = new Vector2(slotPieces[slotPieces.Count - 1].localPosition.x, slotPieces[0].localPosition.y + BossSelectionManager.instance.slotMoveAmount);
        slotPieces.Insert(0, slotPieces[slotPieces.Count - 1]);
        slotPieces.RemoveAt(slotPieces.Count - 1);
        currentSlotPiece = slotPieces[0].gameObject;

        for (int i = 0; i < slotPieces.Count; i++)
        {
            float currentYPos = slotPieces[i].localPosition.y;
            float newYPos = currentYPos - BossSelectionManager.instance.slotMoveAmount;

            slotPieces[i].DOLocalMoveY(newYPos, BossSelectionManager.instance.slotMoveSpeed).OnComplete(ResetSlot);
        }
    }

    void ResetTopSlot()
    {
        slotPieces[0].localPosition = new Vector2(slotPieces[0].localPosition.x, slotPieces[slotPieces.Count - 1].localPosition.y - BossSelectionManager.instance.slotMoveAmount);
        slotPieces.Add(slotPieces[0]);
        slotPieces.RemoveAt(0);
    }

    void ResetSlot()
    {
        slotMoving = false;
    }

    public Sprite GetCurrentSlotPieceImage()
    {
        return currentSlotPiece.GetComponent<Image>().sprite;
    }

    public BossPieceStats GetCurrentSlotPieceStats()
    {
        return currentSlotPiece.GetComponent<BossPieceStats>();
    }
}
