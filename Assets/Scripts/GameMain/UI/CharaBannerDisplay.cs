using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Messager;
using UnityEngine.Events;
using System.IO;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 卡牌显示，通过UnityEngine.UI修改卡牌模板
    /// TODO：拆分点击控件
    /// </summary>
    public class CharaBannerDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// 待显示卡牌
        /// </summary>
        public Chara chara;

        [SerializeField]
        private CharaCardDisplay CharaCard;

        public BattleSite PlayerSite;

        /// <summary>
        /// 基础卡牌信息
        /// </summary>
        [Header("基础卡牌信息")]
        public Text charaName;
        public Text charaInfoText;
        public Image charaImage;
        [SerializeField]
        private Image CDImage;

        /// <summary>
        /// 单位卡信息容器显示
        /// </summary>
        [Header("单位卡信息显示")]
        public Text atkText;
        public Text hpText;
        public Text engText;
        [SerializeField]
        private Text CDText;
        //public Image atkElement;

        private CharaCardDisplay Title;
        private Color OriColor;

           
        void Start()
        {
            OriColor = gameObject.transform.Find("布局").gameObject.GetComponent<Image>().color;
        }

        public void Init(CharaCardDisplay _title, Chara _chara, BattleSite _site)
        {
            chara = _chara;
            PlayerSite = _site;
            Title = _title;

            charaName.text = chara.unitName;
            atkText.text = chara.baseATK.ToString();
            hpText.text = chara.HP.ToString();
            engText.text = chara.MP.ToString();

            SetImage();
        }

        public void CDDisplay()
        {
            if (GameContext.Instance.GetPlayerBySite(PlayerSite).CharaCD == 0)
            {
                CDImage.gameObject.SetActive(false);
                CDText.gameObject.SetActive(false);
            }
            else
            {
                CDImage.gameObject.SetActive(true);
                CDText.gameObject.SetActive(true);
                CDText.text = " CD:\n" + GameContext.Instance.GetPlayerBySite(PlayerSite).CharaCD + "回合";
            }
        }

        public void OnMouseDown()
        {
            //if (角色CD到了)，切换
            if (GameContext.Instance.GetPlayerBySite(PlayerSite).CharaCD == 0)
            {
                SummonChara();
                GameContext.Instance.GetPlayerBySite(PlayerSite).HandCharaManager.CDDisplay();
                GameContext.Instance.GetPlayerBySite(PlayerSite).CharaCD = GameContext.MissionConfig.CharaCD;
            }
            else
            {
                // todo 给个动画或者弹窗告诉玩家现在cd还没到
            }
        }

        public void OnPointerEnter(PointerEventData e)
        {
            HighLight();
        }

        public void OnPointerExit(PointerEventData e)
        {
            ReturnColor();
        }

        /// <summary>
        /// 召唤并从牌库移除该角色
        /// </summary>
        // todo 重写，这部分代码过于混乱了，整个角色部分都要重写
        public void SummonChara()
        {
            GameObject unit = GameContext.Instance.GetPlayerBySite(PlayerSite).Chara;
            BucketEntity Bucket = GameContext.Instance.GetPlayerBySite(PlayerSite).CharaBucket;

            // 暂存场上单位
            Chara tempChara = unit.GetComponent<UnitEntity>().unit as Chara;

            unit.AddComponent<CharaComponent>();
            unit.GetComponent<CharaComponent>().Init(chara);

            // 根据己方单位更新
            unit.GetComponent<UnitEntity>().Init(chara, PlayerSite, Bucket);
            unit.GetComponent<UnitDisplay>().Init();

            if (tempChara != null && tempChara.HP > 0)
            {
                GameContext.Instance.GetPlayerBySite(PlayerSite).HandCharaManager.Update(tempChara,PlayerSite);
            }
            unit.gameObject.SetActive(true);
            SetImage();


            UnitEntity unitEntity = unit.GetComponent<UnitEntity>();
            unitEntity.AddCharaCompment(PlayerSite);
            BattleFieldManager.Instance.SetBucketCarryFlag(Bucket.serial, unitEntity);

            //更新显示,实现拖沓，望优化
            GameObject BannerOnBattle = GameObject.Instantiate(PrefabsLoader.Instance.chara_BannerPrefab);
            if (PlayerSite == BattleSite.P1)
            {
                if (PrefabsLoader.Instance.charaBannerOnBattle.transform.childCount != 0)
                    Destroy(PrefabsLoader.Instance.charaBannerOnBattle.transform.GetChild(0).gameObject);
                BannerOnBattle.transform.SetParent(PrefabsLoader.Instance.charaBannerOnBattle.transform);
                BannerOnBattle.transform.localScale = Vector3.one;
                BannerOnBattle.transform.position = PrefabsLoader.Instance.charaBannerOnBattle.transform.position;
            }
            else
            {
                if (PrefabsLoader.Instance.charaBanner2OnBattle.transform.childCount != 0)
                    Destroy(PrefabsLoader.Instance.charaBanner2OnBattle.transform.GetChild(0).gameObject);
                BannerOnBattle.transform.SetParent(PrefabsLoader.Instance.charaBanner2OnBattle.transform);
                BannerOnBattle.transform.localScale = Vector3.one;
                BannerOnBattle.transform.position = PrefabsLoader.Instance.charaBanner2OnBattle.transform.position;
            }

            BannerOnBattle.GetComponent<CharaBannerDisplay>().Init(null, chara, PlayerSite);
            BannerOnBattle.GetComponent<CharaBannerDisplay>().SetImage();
            GameContext.Instance.GetPlayerBySite(PlayerSite).HandCharaManager.CharaOnBattle = BannerOnBattle.GetComponent<CharaBannerDisplay>();
            //禁止最下面角色面板操作
            BannerOnBattle.GetComponent<BoxCollider2D>().enabled = false;
            BannerOnBattle.GetComponent<Image>().enabled = false;

            //TODO:实现在场角色名片的实时更新
            GameContext.Instance.GetPlayerBySite(PlayerSite).HandCharaManager.Remove(Title.gameObject);
            Destroy(Title.gameObject);
            Destroy(this.gameObject);
        }

        /// <summary>
        /// 显示卡牌：将卡牌数据与UI绑定
        /// </summary>
        public void SetImage()
        {
            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "UnitModel/ModelImage/" + chara.unitName;

                float imageSizeScale = 0.5f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                charaImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                charaImage.overrideSprite = sprite;
            }
            catch
            {
                //Debug.Log(card.cardName + " 无模型");
                Debug.LogError("ERROR HERE");
            }

        }

        //草率的高亮实现
        public void HighLight()
        {
            GameObject LayOut = gameObject.transform.Find("布局").gameObject;
            LayOut.GetComponent<Image>().color = new Color(1, 66f / 255, 78f / 255);
        }

        public void ReturnColor()
        {
            GameObject LayOut = gameObject.transform.Find("布局").gameObject;
            LayOut.GetComponent<Image>().color = OriColor;
        }

        public void DestoryThis()
        {
            Destroy(this.gameObject);
        }
    }
}