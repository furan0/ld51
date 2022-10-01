using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CanFlip : DisbaledOnDeath
{
    public enum E_flipType {
        MANUAL,
        WITH_MOVEMENT,
        WITH_ROTATION
    }

    private CanMove moveCpnt;
    private Vector3 lastDir;
    public bool isFlipped = false;
    [SerializeField] protected E_flipType flipType = E_flipType.MANUAL;
    [SerializeField] Vector3 flipTransformation = new Vector3(-1,1,1);

    void Start()
    {
        if (flipType == E_flipType.WITH_MOVEMENT) {
            moveCpnt = GetComponentInParent<CanMove>();
            Assert.IsNotNull(moveCpnt);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (flipType) {
            case E_flipType.WITH_MOVEMENT:
                if (moveCpnt != null) {
                    float dotProduct = Vector3.Dot(moveCpnt.direction, Vector3.right);
                    if ((dotProduct > 0) && isFlipped) {
                        // Going from left to right -> flip needed
                        flipTransform();
                    } else if ((dotProduct < 0) && !isFlipped) {
                        // Going from right to left -> flip
                        flipTransform();
                    }
                }
                break;

            case E_flipType.WITH_ROTATION:
            float angle = Mathf.Abs(transform.eulerAngles.z) % 360;
                bool isPointingRight = (angle <= 90) || (angle >= 270);
                if (isPointingRight && isFlipped) {
                    // Going from left to right -> flip needed
                    flipTransform();
                } else if (!isPointingRight && !isFlipped) {
                    // Going from right to left -> flip
                    flipTransform();
                }
                break;

            case E_flipType.MANUAL:
                // Nothing to do for MANUAL case
                break;
        }
    }

    private void flipTransform() {
        isFlipped = !isFlipped;
        transform.localScale =  new Vector3(transform.localScale.x * flipTransformation.x, transform.localScale.y * flipTransformation.y, transform.localScale.z * flipTransformation.z);
    }
}
