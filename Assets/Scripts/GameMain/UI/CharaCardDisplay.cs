using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Genpai
{
    public class CharaCardDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Text title;

        public bool isFold = true; // 是否是折叠状态
        public Transform CharaBanner;

        //CharaBanner的大小
        public int PanelHeight = 90;
        public int PanelWidth = 280;

        private bool onCard = false;
        private float DelayTime = 1.2f;
        private Color OriColor;

        public void Start()
        {
            OriColor = gameObject.GetComponent<Image>().color;
        }

        public void Init(Chara _chara, BattleSite _site)
        {
            title.text = _chara.unitName;

            GameObject newCharaBanner;
            newCharaBanner = GameObject.Instantiate(PrefabsLoader.Instance.chara_BannerPrefab);
            newCharaBanner.transform.SetParent(this.transform.parent);

            newCharaBanner.GetComponent<RectTransform>().sizeDelta = new Vector3(PanelWidth, PanelHeight);
            //角色名片显示初始化
            newCharaBanner.GetComponent<CharaBannerDisplay>().Init(this, _chara, _site);

            newCharaBanner.transform.localScale = Vector3.one;
            newCharaBanner.SetActive(false);

            CharaBanner = newCharaBanner.transform;
        }

        public void HideBanner()
        {
            CharaBanner.gameObject.SetActive(false);
            isFold = true;
        }

        public void OnPointerClick(PointerEventData e)
        {

            if (isFold)
            {
                if (CharaBanner != null)
                {
                    //隐藏其他角色名片
                    GameContext.Instance.GetPlayerBySite(CharaBanner.GetComponent<CharaBannerDisplay>().PlayerSite)
                        .HandCharaManager.HideAllBanners();
                    CharaBanner.gameObject.SetActive(true);
                }
                isFold = false;
            }
            else
            {
                if (CharaBanner != null)
                {
                    CharaBanner.gameObject.SetActive(false);
                }
                isFold = true;
            }
        }

        public void OnPointerEnter(PointerEventData e)
        {
            onCard = true;

            HighLight();
        }

        public void OnPointerExit(PointerEventData e)
        {
            ReturnColor();
            onCard = false;
        }

        //草率的高亮实现
        public void HighLight()
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 110f / 255);
        }

        public void ReturnColor()
        {
            gameObject.GetComponent<Image>().color = OriColor;
        }
    }
}