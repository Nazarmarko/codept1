using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilites;
public class GunManager : Singleton<GunManager>
{
    #region Weapon
    private KeyCode pickUpKey = KeyCode.F;
    private KeyCode dropKey = KeyCode.G;
    string weaponTag = "Weapon";

    public List<GameObject> weaponsInGame;
    private int maxWeapons = 3;
    #endregion
    public GameObject currentWeapon;

    #region Transforms
    public Transform cameraTransform;
    public Transform hand;
    public Transform GunsPosition;
    public Transform dropPoint;
    #endregion
    public bool isSwitch = true;
    #region InputsGetter
    void Update()
    {
        if (!isSwitch)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }

            if(Input.GetKeyDown(pickUpKey))
        {
            PickUpWeapon();
        }
          
        
        if (Input.GetKeyDown(dropKey) && currentWeapon != null)
        {
            ThrowWeapon();
        }
    }
    #endregion

    #region PickUp
    void PickUpWeapon()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.CompareTag(weaponTag)
                && weaponsInGame.Count < maxWeapons)
            {
                weaponsInGame.Add(hit.collider.gameObject);

                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                hit.collider.gameObject.SetActive(false);

                hit.transform.parent = hand;
                hit.transform.position = GunsPosition.position;
                hit.transform.rotation = GunsPosition.rotation;
                SelectWeapon(0);
            }
        }
    }
    #endregion
    #region Throw
    void ThrowWeapon()
    {
        currentWeapon.transform.parent = null;

        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;

        var weaponInstanceID = currentWeapon.GetInstanceID();
        for (int i = 0; i < weaponsInGame.Count; i++)
        {
            if (weaponsInGame[i].GetInstanceID() == weaponInstanceID)
            {
                weaponsInGame.RemoveAt(i);
                break;
            }
        }

        currentWeapon = null;
    }
    #endregion
    #region SellectWeapon
    void SelectWeapon(int index)
    {
        if (weaponsInGame.Count > index && weaponsInGame[index] != null)
        {
            if(currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(false);
            }
            currentWeapon = weaponsInGame[index];
            currentWeapon.SetActive(true);
        }
    }
    #endregion
}
