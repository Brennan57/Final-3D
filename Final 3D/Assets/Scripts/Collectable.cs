using UnityEngine;

// Add an enum to represent an item's rarity.

public class Collectable : MonoBehaviour
{
    // Add a member varible to represent each collectable's rarity. Make sure
    //      it can be tuned from the Inspector.
    [SerializeField] string com = "common";
    [SerializeField] string uncom = "uncommon";
    [SerializeField] string rare = "rare";
    private Collider[] _colliders;
    private Rigidbody _rigidbody;
    private GameController _gameController;
    private BoxCollider trigger;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform ogparent;
    [SerializeField] private Katamari kata;

    // ------------------------------------------------------------------------
    private void Start ()
    {
        _gameController = FindFirstObjectByType<GameController>();

        _colliders = GetComponents<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
/*
        BoxCollider originalCollider = GetComponent<BoxCollider>();
        trigger = gameObject.AddComponent<BoxCollider>();

        trigger = gameObject.AddComponent<BoxCollider>();

        trigger.isTrigger = true;
        trigger.size = originalCollider.size * 1.2f;
        trigger.center = originalCollider.center;
        */
        //ogparent = ;
    }

    // ------------------------------------------------------------------------
    // You might want to add parameters to this method.
    public void Collect ()
    {
        // Set the new parent and position of the Collectable.
        // Disable all of this object's colliders.
        for(int i = 0; i<_colliders.Length;i++){
            _colliders[i].enabled = false;
        }
        transform.SetParent(parent);

        // This line sets the rigidbody to kinematic so it'll stop using physics.
        // (You don't need to change this.)
        _rigidbody.isKinematic = true;

        // Tell the GameController that we collected this item.
        //GameController.CollectItem(string rarity);
    }

    // ------------------------------------------------------------------------
    // You might want to add parameters to this method.
    public void Drop (Vector3 droppos)
    {
        // Reset this object's parent so it's no longer attached to the Katamari,
        //  and give it a new position ABOVE the Katamari sphere.
        //transform.SetParent(ogparent);
        _rigidbody.transform.position = droppos;

        // Enable all of this object's colliders.
        for(int i = 0; i<_colliders.Length;i++){
            _colliders[i].enabled = true;
        }

        // This line sets the rigidbody to kinematic so it'll start using physics again.
        // (You don't need to change this.)
        _rigidbody.isKinematic = false;

        // Add a force to the rigidbody to make it fly upwards.
        _rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        // Tell the UI that we removed this item.
        
    }
}