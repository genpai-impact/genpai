using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Genpai
{
    public class CharaCardDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Text title;

        public bool isFold = true; // �Ƿ����۵�״̬
        public Transform CharaBanner;

        //CharaBanner�Ĵ�С
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
            //��ɫ��Ƭ��ʾ��ʼ��
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
                    //����������ɫ��Ƭ
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

        //���ʵĸ���ʵ��
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