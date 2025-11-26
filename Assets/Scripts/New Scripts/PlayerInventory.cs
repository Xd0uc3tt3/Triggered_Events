using UnityEngine;

public enum BodyPart
{
    None,
    Heart,
    Lungs,
    Spine,
    Ribs
}

public class PlayerInventory : MonoBehaviour
{
    public BodyPart currentPart = BodyPart.None;

    public bool HasPart()
    {
        return currentPart != BodyPart.None;
    }

    public void PickUpPart(BodyPart part)
    {
        currentPart = part;
    }

    public void RemovePart()
    {
        currentPart = BodyPart.None;
    }
}
