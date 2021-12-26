using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    private Collider _fruitCollider = null;
    [field: SerializeField] public FruitType TypeOfFruit { get; private set; } = default;

    void Start()
    {
        _fruitCollider = GetComponent<Collider>();
        _fruitCollider.isTrigger = true;
        AssignFruitEventInfo Afei = new AssignFruitEventInfo(gameObject, "");
        EventManager.SendNewEvent(Afei);
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractFruit _interactFruit = other.GetComponent<IInteractFruit>();
        if (_interactFruit != null)
        {
            _interactFruit.PickUpFruit(gameObject);
        }
        else if (other.tag == "Choncc")
        {
            Debug.Log("Oh shit im inside the choncc");
        }
    }
}

public enum FruitType { Pumpkin, Radish , Pear, Carrot}
