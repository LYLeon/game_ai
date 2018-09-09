using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    public GameObject HeartBlock;
    public GameObject HeartBlockPlaceHolder;
    public GameObject AttackBlock;
    public GameObject AttackBlockPlaceHolder;

    //TODO get type from the block?
    private const int BlOCK_NONE = 0;
    private const int BLOCK_HEART = 1;
    private const int BLOCK_HEART_PLACE_HOLDER = 10;
    private const int BLOCK_ATTACK = 2;
    private const int BLOCK_ATTACK_PLACE_HOLDER = 20;

    private int typeSelected = BlOCK_NONE;
    private GameObject selectedBlock;
    private bool isLegalPlacement;

    private List<GameObject> blocks;
	// Use this for initialization
	void Start () {
        blocks = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        int newTypeSelected = typeSelected;
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBlock(typeSelected);
        }
        if (Input.GetMouseButtonDown(1)) {
            // secondary mouse to cancel.
            newTypeSelected = BlOCK_NONE;
        }
        if (Input.GetKeyDown("1")) {
            newTypeSelected = BLOCK_HEART;
        }
        if (Input.GetKeyDown("2")) {
            newTypeSelected = BLOCK_ATTACK;
        }

        bool isTheSameType = newTypeSelected == typeSelected;
        if (!isTheSameType) {
            if (selectedBlock != null)
            {
                //TODO use object pooling
                Destroy(selectedBlock);
                selectedBlock = null;
            }
            selectedBlock = InstantiateBlock(PlaceHolderType(newTypeSelected));
            typeSelected = newTypeSelected;
        }

        if (selectedBlock != null && selectedBlock.activeSelf) {
            Bounds bounds = selectedBlock.GetComponent<Renderer>().bounds;
            Vector2 mousePos = GetMousePosition();
            Vector2 rayRight = new Vector2(mousePos.x + bounds.size.x / 2, mousePos.y);
            Vector2 rayLeft = new Vector2(mousePos.x - bounds.size.x / 2, mousePos.y);
            RaycastHit2D hitR = Physics2D.Raycast(rayRight, Vector2.down, 3, 1 << LayerMask.NameToLayer("Ground"));
            RaycastHit2D hitL = Physics2D.Raycast(rayLeft, Vector2.down, 3, 1 << LayerMask.NameToLayer("Ground"));
            if (hitR.collider != null || hitL.collider != null)
            {
                RaycastHit2D hit;
                if (hitR.collider == null)
                {
                    hit = hitL;
                    Debug.DrawLine(rayLeft, hit.point);
                }
                else if (hitL.collider == null)
                {
                    hit = hitR;
                    Debug.DrawLine(rayRight, hit.point);
                }
                else {
                    hit = hitR.point.y > hitL.point.y ? hitR : hitL;
                    Debug.DrawLine(rayLeft, hitL.point);
                    Debug.DrawLine(rayRight, hitR.point);
                }

                isLegalPlacement = true;
                selectedBlock.transform.position = new Vector2(mousePos.x, hit.point.y + bounds.size.y / 2);
            }
            else {
                isLegalPlacement = false;
                selectedBlock.transform.position = mousePos;
            }
        }
    }

    private void PlaceBlock(int blockType) {
        if (isLegalPlacement) {
            GameObject block = InstantiateBlock(blockType);
            if (block != null) {
                Rigidbody2D rigidbody = block.AddComponent<Rigidbody2D>();
                rigidbody.bodyType = RigidbodyType2D.Kinematic;
                Log("Layer: "+LayerMask.LayerToName(block.layer));
                block.transform.position = selectedBlock.transform.position;
                blocks.Add(block);
            }
        }
    }

    private GameObject InstantiateBlock(int blockType) {
        GameObject block= null; // default to BLOCK_NONE
        switch (blockType)
        {
            case BLOCK_HEART:
                block = Instantiate(HeartBlock);
                break;
            case BLOCK_HEART_PLACE_HOLDER:
                block = Instantiate(HeartBlockPlaceHolder);
                break;
            case BLOCK_ATTACK:
                block = Instantiate(AttackBlock);
                break;
            case BLOCK_ATTACK_PLACE_HOLDER:
                block = Instantiate(AttackBlockPlaceHolder);
                break;
        }

        return block;
    }

    private int PlaceHolderType(int blockType) {
        switch (blockType) {
            case BLOCK_HEART:
                return BLOCK_HEART_PLACE_HOLDER;
            case BLOCK_ATTACK:
                return BLOCK_ATTACK_PLACE_HOLDER;
        }
        return BlOCK_NONE;
    }

    private Vector2 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Log(string msg) {
        Debug.Log(msg);
    }
}
