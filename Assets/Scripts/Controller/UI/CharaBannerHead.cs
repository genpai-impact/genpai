using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// ��ɫ�۵���Ƭ
    /// </summary>
    public class CharaBannerHead : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public BattleSite PlayerSite;

        public Text title;

        public bool isFold = true;
        public CharaBannerDisplay CharaBanner;

        //CharaBanner�Ĵ�С
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

            // ����Banner
            GameObject newCharaBanner = GameObject.Instantiate(PrefabsLoader.Instance.chara_BannerPrefab);

            newCharaBanner.transform.SetParent(this.transform.parent);
            newCharaBanner.GetComponent<RectTransform>().sizeDelta = new Vector3(PanelWidth, PanelHeight);

            //��ɫ��Ƭ��ʾ��ʼ��
            CharaBanner = newCharaBanner.GetComponent<CharaBannerDisplay>();
            CharaBanner.Init(this, _chara, PlayerSite);

            newCharaBanner.transform.localScale = Vector3.one;
            newCharaBanner.SetActive(false);

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
                    //����������ɫ��Ƭ
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

        //���ʵĸ���ʵ��
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