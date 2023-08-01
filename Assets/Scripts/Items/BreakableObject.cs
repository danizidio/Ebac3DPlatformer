using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamageable
{
    [SerializeField] float _objLife;

    [SerializeField] GameObject[] _fxObjs;
    [SerializeField] GameObject _objMesh;

    [SerializeField] Mesh[] _meshes;

    [SerializeField] GameObject[] _possibleDrops;

    [SerializeField] float _minItems, _maxItems;

    private void Start()
    {
        _objMesh.GetComponent<MeshFilter>().mesh = _meshes[0];

        foreach (GameObject obj in _fxObjs)
        {
            obj.SetActive(false);
        }
    }

    public void DamageOutput(int dmg, Vector3 pullFeedback)
    {
        _objLife -= dmg;

        if(_objLife <= 0)
        {
            _objMesh.GetComponent<MeshFilter>().mesh = _meshes[1];

            foreach(GameObject obj in _fxObjs) 
            {
                obj.SetActive(true);
            }

            float v = Random.Range(_minItems, _maxItems);

            for (int i = 0; i < v; i++)
            {
                DropItems();
            }

            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(Dissolving());
        }
    }

    void DropItems()
    {
        GameObject temp = Instantiate(_possibleDrops[Random.Range(0, _possibleDrops.Length)],this.transform.position,Quaternion.identity);
        temp.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), 
                                                                                                   Random.Range(7, 10), 
                                                                                                   Random.Range(-1, 1)), 
                                                                                                   ForceMode.Impulse);

    }

    IEnumerator Dissolving()
    {
        float t = 1;

        while (t >= 0)
        {
            t -= Time.deltaTime/5;

            _objMesh.GetComponent<MeshRenderer>().material.SetFloat("_Fade", t);

            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);

        yield return null;
    }
}
