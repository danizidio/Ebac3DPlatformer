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
        [SerializeField] Image img, img2;

        [SerializeField] Color fullLife, notFullLife, averageLife, criticalLife;

        [SerializeField] float whiteBarLossRate;

        protected void Init()
        {
            onUpdateLifeBar += UpdateLifeBar;

            anim = this.GetComponent<Animator>();

            img.fillAmount = 1;
            img2.fillAmount = 1;

            img.color = fullLife;
        }

        public void UpdateLifeBar(float currentLife, float maxLife)
        {
            anim.SetTrigger("HIT");

            float value = currentLife / maxLife;

            img.fillAmount = value;

            if (currentLife < 0)
            {
                img.fillAmount = 0;
            }

            StartCoroutine(RedBarUpdate(value, currentLife, maxLife));

            if (value <= .3f)
            {
                img.color = criticalLife;
            }
            else if (value >= .9f && value <= 1f)
            {
                img.color = notFullLife;
            }
            else if (value >= .3f && value <= .9f)
            {
                img.color = averageLife;
            }
            else
            {
                img.color = new Color32(50, 255, 0, 255);
            }
        }

        IEnumerator RedBarUpdate(float v, float currentLife, float maxLife)
        {
            yield return new WaitForSeconds(whiteBarLossRate);

            float value2 = currentLife / maxLife;

            img2.fillAmount = value2;

            if (value2 < 0)
            {
                value2 = 0;
            }
        }
    }
}
