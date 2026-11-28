using System.Collections.Generic;
using UnityEngine;

public class Katamari : MonoBehaviour
{
    [SerializeField] private AudioSource _equipAudioSource;
    [SerializeField] private AudioSource _bonkAudioSource;
    [SerializeField] private AudioSource _unequipAudioSource;
    [SerializeField] private float _radiusGrowAmount = 1.0f;
    [SerializeField] private Collider _katamariCollider;
    [SerializeField] private Transform _sphereTransform;
    [SerializeField] private GameObject player;

    // Add a member variable to represent a LIST of all of the Collectable 
    //      objects we've collected named _katamariCollection.
    // Then, uncomment the code in SetSpherePosition().
    public List<Collectable> _katamariCollection = new List<Collectable>();
    public Vector3 newpos;
    // ------------------------------------------------------------------------
    // There's probably a member variable you need to declare in a built-in
    //      Unity method that runs once, before Update starts...
    
    private void Start(){
        SetSpherePosition();
    }
    private void Update(){
        keepcollectables();
    }
    // ------------------------------------------------------------------------
    private void OnTriggerEnter(Collider otherCollider)
    {
        
        // Find the Collectable Component on the object we collided with.
        // Modify this if statement to ALSO check and make sure the Collectable
        //      actually exists, since we might collide with other kinds of objects too.

        if(otherCollider.bounds.extents.magnitude <= _katamariCollider.bounds.extents.magnitude && otherCollider!=null) {
            //Debug.Log("test");
            // Grow the SPHERE OBJECT, which should be a CHILD of the Katamari object,
            //      by _radiusGrowAmount.
            // AFTER growing the sphere, call SetSpherePosition() to update the sphere's position.
            _sphereTransform.localScale += new Vector3(_radiusGrowAmount,_radiusGrowAmount,_radiusGrowAmount);
            SetSpherePosition();
            // Add the new Collectable to our list of hit objects.
            _katamariCollection.Add(otherCollider.GetComponentInParent<Collectable>());

            // Tell the Collectable that we want to collect it.
            otherCollider.GetComponentInParent<Collectable>().Collect();
            // If the object has an NPC Component, let it know about being collected
            //      by calling PickUp on the NPC
            if(otherCollider.GetComponentInParent<Collectable>().tag=="lizard"){
                otherCollider.GetComponentInParent<NPC>().PickUp();
            }
            
            _equipAudioSource.Play();
            
        }
        
    }

    // ------------------------------------------------------------------------
    private void OnCollisionEnter (Collision collision)
    {
        // Modify this if statement to ALSO check if we have any entries in our
        //  katamari collection.
        if(collision.gameObject.tag.Equals("Obstacle") && _katamariCollection.Count>0)
        {
            // SHRINK the katamari SPHERE by _radiusGrowAmount.
            // AFTER changing its size, call SetSpherePosition() to fix its position.
            _sphereTransform.localScale -= new Vector3(_radiusGrowAmount,_radiusGrowAmount,_radiusGrowAmount);
            SetSpherePosition();
            // Grab a RANDOM member of our katamari collection.
            int random = Random.Range(0, _katamariCollection.Count);
            // Generate a position to drop it at- preferably by launching it far above the katamari sphere.
            newpos = _sphereTransform.position+new Vector3(0,2,0);
            // Then, DROP that Collectable.
            _katamariCollection[random].Drop(newpos);
            // Finally, remove the entry from the _katamariCollection.
            _katamariCollection.RemoveAt(random);
            _unequipAudioSource.Play();

        }
        //Debug.Log("ball collided with " + collision.gameObject.name);
    }

    // ------------------------------------------------------------------------
    // Uncomment this method by removing the /* and */ AFTER you've added the
    //      _katamariCollection member variable.
    // You don't need to code anything else in this method :)
    // This method sets up the sphere to be in the corret position in front
    //      of the player, and ensures all of the objects inside the katamari
    //      remain in its center.
    
    private void SetSpherePosition ()
    {
        // place the sphere in front of the player
        _sphereTransform.position = transform.parent.position +
                transform.parent.up * _sphereTransform.lossyScale.x/2.0f +
                transform.parent.forward * (_sphereTransform.lossyScale.x/2.0f + 1.0f);

        // move all of the katamari objects to the new katamari center
        foreach(Collectable collectable in _katamariCollection)
        {
            if(collectable != null)
            {
                collectable.transform.position = _sphereTransform.position;   
            }
            else
            {
                Debug.LogError("null item in katamari collectables list");
            }
        }
    }

    private void keepcollectables(){
        foreach(Collectable collectable in _katamariCollection)
        {
            if(collectable != null)
            {
                collectable.transform.position = _sphereTransform.position;   
            }
            else
            {
                Debug.LogError("null item in katamari collectables list");
            }
        }
    }

    
}
