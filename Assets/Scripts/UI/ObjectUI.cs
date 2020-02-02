using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObjectUI : MonoBehaviour
    {
        public Canvas m_canvas;
        public Image m_lifeBar;
        public Image m_progressBar;

        public GameObject m_lifeBarContainer;
        public GameObject m_progressBarContainer;

        void Start()
        {
        }

        void Update()
        {

        }

        public void SetDisplayed(bool isDisplayed)
        {
            m_canvas.enabled = isDisplayed;
        }

        /// <summary>
        ///     Set the fill amount of the life bar
        /// </summary>
        /// 
        /// <param name="fillAmount">
        ///     Fill amount between 1 (filled) and 0(not filled)
        /// </param>
        public void SetLifeBarFill(float fillAmount)
        {
            m_lifeBar.fillAmount = fillAmount;
        }

        /// <summary>
        ///     Set the progression of the progress bar
        /// </summary>
        /// 
        /// <param name="fillAmount">
        ///     Progression between 1 (100%) and 0(0%)
        /// </param>
        public void SetProgressBar(float progression)
        {
            m_progressBar.fillAmount = progression;
        }
    }
}
