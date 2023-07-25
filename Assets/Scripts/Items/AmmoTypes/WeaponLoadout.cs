using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponLoadout : MonoBehaviour
{
    [SerializeField] Transform _shootPivot;
    [SerializeField] GameObject[] _projectile;
    GameObject _currentWeapon;

    Coroutine _currentCoroutine;

    int _currentSpentAmmo;

    bool _reloading;

    private void Start()
    {
        _currentWeapon = _projectile[0];
    }

    #region - InputManager Buttons
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !_reloading)
        {
            StopAction();
            _currentCoroutine = StartCoroutine(StartShoot());
        }

        if(context.canceled)
        {
            StopAction();
        }
    }

    public void ChangeWeapon(int n)
    {
        if (!_reloading)
        {
            _currentSpentAmmo = 0;
            _currentWeapon = _projectile[n];
            StartCoroutine(ReloadingCurrentWeapon());
        }
    }

    #endregion

    void StopAction()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = null;
    }

    IEnumerator StartShoot()
    {
        if (_reloading) yield break;

        while (true)
        {
            if (_currentSpentAmmo < _currentWeapon.GetComponent<AmmoBase>().ammoLimit)
            {
                _currentWeapon.GetComponent<AmmoBase>().SpawnShot(_currentWeapon, _shootPivot);

                _currentSpentAmmo++;

                if (_currentSpentAmmo >= _currentWeapon.GetComponent<AmmoBase>().ammoLimit)
                {
                    StopAction();
                    StartCoroutine(ReloadingCurrentWeapon());
                }

                yield return new WaitForSeconds(_currentWeapon.GetComponent<AmmoBase>().coolDownShoots);
            }
        }
    }

    IEnumerator ReloadingCurrentWeapon()
    {
        _reloading = true;

        float t = 0;

        while(_currentSpentAmmo > 0)
        {
            t += Time.deltaTime;
            if(t >= _currentWeapon.GetComponent<AmmoBase>().rechargeTime)
            {
                _currentSpentAmmo--;
                t = 0;
            }
            yield return new WaitForEndOfFrame();
        }
            _reloading = false;
    }
}
