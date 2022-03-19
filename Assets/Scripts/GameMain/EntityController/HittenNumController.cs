using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Genpai
{

    /// <summary>
    /// ÉËº¦Êý×Ö¿ØÖÆ½Å±¾
    /// </summary>
    public class HittenNumController : MonoBehaviour
    {

        public GameObject DamageNum;
        public GameObject ReactionText;

        private PlayableDirector Playable;


        // Start is called before the first frame update
        void Awake()
        {
            Playable = GetComponentInChildren<PlayableDirector>();

        }

        public void Play(Damage damage)
        {
            Dictionary<string, Color> ColorDic = GetColorByElement(damage.damageStructure.Element);

            SetColor(ColorDic);


            DamageNum.GetComponent<Text>().text = damage.damageStructure.DamageValue.ToString();
            ReactionText.GetComponent<Text>().text = damage.damageReaction.ToString();

            Vector3 newPos = Camera.main.WorldToScreenPoint(damage.GetTarget().carrier.gameObject.transform.position);

            // ¸ß¶ÈÆ«ÒÆ
            newPos.y += 250;

            // Ëæ»úÆ«ÒÆ
            newPos += new Vector3(Random.Range(-50, 50), Random.Range(-50, 50));

            DamageNum.GetComponent<RectTransform>().position = newPos;
            ReactionText.GetComponent<RectTransform>().position = new Vector3(30, -40, 10) + newPos;

            if (damage.damageReaction == ElementReactionEnum.None)
            {
                ReactionText.SetActive(false);
            }

            // ¼¤»î
            Playable.enabled = true;
            Invoke("ShutUp", 3f);
        }

        public void ShutUp()
        {
            Destroy(gameObject);
        }

        public void SetColor(Dictionary<string, Color> ColorDic)
        {
            DamageNum.GetComponent<Text>().color = ColorDic["inner"];
            ReactionText.GetComponent<Text>().color = ColorDic["inner"];
            DamageNum.GetComponent<Outline>().effectColor = ColorDic["bound"];
            ReactionText.GetComponent<Outline>().effectColor = ColorDic["bound"];
        }

        public Dictionary<string, Color> GetColorByElement(ElementEnum element)
        {
            Dictionary<string, Color> ColorDic = new Dictionary<string, Color>();
            switch (element)
            {
                case ElementEnum.None:
                    ColorDic.Add("inner", new Color(255 / 255f, 255 / 255f, 255 / 255f));
                    ColorDic.Add("bound", new Color(169 / 255f, 168 / 255f, 140 / 255f));
                    break;
                case ElementEnum.Anemo:
                    ColorDic.Add("inner", new Color(86 / 255f, 237 / 255f, 200 / 255f));
                    ColorDic.Add("bound", new Color(80 / 255f, 188 / 255f, 165 / 255f));
                    break;
                case ElementEnum.Cryo:
                    ColorDic.Add("inner", new Color(128 / 255f, 232 / 255f, 253 / 255f));
                    ColorDic.Add("bound", new Color(76 / 255f, 133 / 255f, 155 / 255f));
                    break;
                case ElementEnum.Electro:
                    ColorDic.Add("inner", new Color(227 / 255f, 166 / 255f, 255 / 255f));
                    ColorDic.Add("bound", new Color(159 / 255f, 124 / 255f, 251 / 255f));
                    break;
                case ElementEnum.Geo:
                    ColorDic.Add("inner", new Color(254 / 255f, 202 / 255f, 100 / 255f));
                    ColorDic.Add("bound", new Color(196 / 255f, 137 / 255f, 52 / 255f));
                    break;
                case ElementEnum.Hydro:
                    ColorDic.Add("inner", new Color(33 / 255f, 193 / 255f, 254 / 255f));
                    ColorDic.Add("bound", new Color(47 / 255f, 138 / 255f, 198 / 255f));
                    break;
                case ElementEnum.Pyro:
                    ColorDic.Add("inner", new Color(254 / 255f, 156 / 255f, 0 / 255f));
                    ColorDic.Add("bound", new Color(211 / 255f, 138 / 255f, 10 / 255f));
                    break;
            }
            return ColorDic;

        }

    }
}