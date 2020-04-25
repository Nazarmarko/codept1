using System.Collections;
using UnityEngine;

public class SniperScope : MonoBehaviour
{
    public Animator anim;

    
     private GameObject Targets,weaponCamera, Scope;

    public Camera mainCamera;

    private float scopeFOv = 15f;
    private float normalFOV;

    bool isScoped = false;
     void Awake()
    {
        Scope = GameObject.FindGameObjectWithTag("ScopeSet");
        mainCamera = GameObject.FindGameObjectWithTag("fpsCamera").GetComponent<Camera>();
        weaponCamera = GameObject.FindGameObjectWithTag("gunCamera");
        Targets = GameObject.FindGameObjectWithTag("targets");       
    }
    void Update()
    {
        if (Input.GetButtonDown("Aim"))
        {
            isScoped = !isScoped;
            anim.SetBool("isScoped", isScoped);

            Targets.SetActive(!isScoped);
            GunManager.instance.isSwitch = !isScoped;

            if (isScoped)
               StartCoroutine(OnScoped());
            else
                OnUnscoped();
        }
    }
    #region ScopeSetter
    void OnUnscoped()
    {
        Scope.SetActive(false);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }
    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        Scope.SetActive(true);
        weaponCamera.SetActive(false);

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopeFOv;
    }
    #endregion
}
