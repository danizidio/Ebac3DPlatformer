using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CommonMethodsLibrary
{
    public class LifebarBase: MonoBehaviour
    {
        public delegate void UpdatingLifeBar(float currentLife, float maxLife);
        public UpdatingLifeBar onUpdateLifeBar;

        Animator anim;

        [SerializeField] Image _lifebarRed, _lifebarWhite;

        [SerializeField] Color fullLife, notFullLife, averageLife, criticalLife;

        [SerializeField] float whiteBarLossRate;

        protected void Init()
        {
            onUpdateLifeBar += UpdateLifeBar;

            anim = this.GetComponent<Animator>();

            _lifebarRed.fillAmount = 1;
            _lifebarWhite.fillAmount = 1;

            _lifebarRed.color = fullLife;
        }

        public void UpdateLifeBar(float currentLife, float maxLife)
        {
            anim.SetTrigger("HIT");

            float value = currentLife / maxLife;

            _lifebarRed.fillAmount = value;

            if (currentLife < 0)
            {
                _lifebarRed.fillAmount = 0;
            }

            StartCoroutine(RedBarUpdate(value, currentLife, maxLife));

            if (value <= .3f)
            {
                _lifebarRed.color = criticalLife;
            }
            else if (value >= .9f && value <= 1f)
            {
                _lifebarRed.color = notFullLife;
            }
            else if (value >= .3f && value <= .9f)
            {
                _lifebarRed.color = averageLife;
            }
            else
            {
                _lifebarRed.color = new Color32(50, 255, 0, 255);
            }
        }

        IEnumerator RedBarUpdate(float v, float currentLife, float maxLife)
        {
            yield return new WaitForSeconds(whiteBarLossRate);

            float value2 = currentLife / maxLife;

            _lifebarWhite.fillAmount = value2;

            if (value2 < 0)
            {
                value2 = 0;
            }
        }
    }
}
