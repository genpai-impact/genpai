using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// 角色折叠名片
    /// </summary>
    public class CharaBannerHead : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public BattleSite PlayerSite;

        public Text title;

        public bool isFold = true;
        public Transform CharaBanner;
        public Chara chara;

        //CharaBanner的大小
        public int PanelHeight = 90;
        public int PanelWidth = 280;

        private Color OriColor;

        public void Start()
        {
            OriColor = gameObject.GetComponent<Image>().color;
        }

        public void Init(Chara _chara, BattleSite battleSite)
        {
            PlayerSite = battleSite;
            title.text = _chara.unitName;

            GameObject newCharaBanner;
            newCharaBanner = GameObject.Instantiate(PrefabsLoader.Instance.chara_BannerPrefab);
            newCharaBanner.transform.SetParent(this.transform.parent);
            newCharaBanner.GetComponent<RectTransform>().sizeDelta = new Vector3(PanelWidth, PanelHeight);

            //角色名片显示初始化
            newCharaBanner.GetComponent<CharaBannerDisplay>().Init(this, _chara, PlayerSite);

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
                        .CharaManager.HideAllBanners();
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
            HighLight();
        }

        public void OnPointerExit(PointerEventData e)
        {
            RestoreColor();
        }

        //草率的高亮实现
        public void HighLight()
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 110f / 255);
        }

        public void RestoreColor()
        {
            gameObject.GetComponent<Image>().color = OriColor;
        }
    }
}