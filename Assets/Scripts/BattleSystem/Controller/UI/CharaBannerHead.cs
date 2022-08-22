using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using BattleSystem.Service.Unit;
using BattleSystem.Controller.Unit;
using DataScripts.DataLoader;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace BattleSystem.Controller.UI
{	
	
    /// <summary>
    /// 角色折叠名片
    /// </summary>
    public class CharaBannerHead : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        
		public BattleSite PlayerSite;

        public Text title;

        public bool isFold = true;
        public CharaBannerDisplay CharaBanner;

        //CharaBanner大小
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
            title.text = _chara.UnitName;

            // 创建Banner
            GameObject newCharaBanner = GameObject.Instantiate(PrefabsLoader.Instance.chara_BannerPrefab);

            newCharaBanner.transform.SetParent(this.transform.parent);
            newCharaBanner.GetComponent<RectTransform>().sizeDelta = new Vector3(PanelWidth, PanelHeight);

            // 角色名片显示初始化
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
                    // 隐藏其它名片
                    GameContext.GetPlayerBySite(CharaBanner.GetComponent<CharaBannerDisplay>().PlayerSite)
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


        // 草率高亮实现
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