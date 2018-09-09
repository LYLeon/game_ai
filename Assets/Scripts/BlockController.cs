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
    private GameObject selectedBlockPlaceHolder;

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
                Destroy(selectedBlockPlaceHolder);
                selectedBlockPlaceHolder = null;
            }
            selectedBlock = InstantiateBlock(newTypeSelected);
            typeSelected = newTypeSelected;
        }

        if (selectedBlock != null && selectedBlock.activeSelf) {
            selectedBlock.transform.position = GetMousePosition();
            RaycastHit2D hit = Physics2D.Raycast(selectedBlock.transform.position, Vector2.down, 3, 1<<LayerMask.NameToLayer("Ground"));
            if (hit.collider != null)
            {
                if (selectedBlockPlaceHolder == null) {
                    selectedBlockPlaceHolder = InstantiateBlock(PlaceHolderType(newTypeSelected));
                }
                selectedBlockPlaceHolder.transform.position = new Vector2(hit.point.x, hit.point.y + selectedBlockPlaceHolder.GetComponent<Renderer>().bounds.size.y / 2);
            }
            else if (selectedBlockPlaceHolder != null) {
                Destroy(selectedBlockPlaceHolder);
                selectedBlockPlaceHolder = null;
            }
        }
    }

    private void PlaceBlock(int blockType) {
        if (selectedBlockPlaceHolder != null) {
            GameObject block = InstantiateBlock(blockType);
            if (block != null) {
                Rigidbody2D rigidbody = block.AddComponent<Rigidbody2D>();
                rigidbody.bodyType = RigidbodyType2D.Kinematic;
                block.transform.position = selectedBlockPlaceHolder.transform.position;
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


}
