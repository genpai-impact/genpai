using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{

    /// <summary>
    /// 角色显示控件
    /// </summary>
    public class CharaBannerDisplay : BaseClickHandle, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        /// <summary>
        /// 待显示卡牌
        /// </summary>
        public NewChara chara;



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

        private CharaBannerHead Title;
        private Color OriColor;


        void Start()
        {
            OriColor = gameObject.transform.Find("布局").gameObject.GetComponent<Image>().color;
        }

        public void Init(CharaBannerHead _title, NewChara _chara, BattleSite _site)
        {
            chara = _chara;
            PlayerSite = _site;
            Title = _title;

            charaName.text = chara.unitName;
            atkText.text = chara.ATK.ToString();
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

        // todo 正确的写法是，在CharaPlayerController中使用OnMouseDown
        public void OnMouseDown()
        {
            GenpaiMouseDown();
        }

        public override void DoGenpaiMouseDown()
        {
            if (GameContext.Instance.GetPlayerBySite(PlayerSite).CharaCD == 0)
            {
                SummonChara(false);
                GameContext.Instance.GetPlayerBySite(PlayerSite).CharaCD = GameContext.MissionConfig.CharaCD;
                GameContext.Instance.GetPlayerBySite(PlayerSite).HandCharaManager.CDRefresh();
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
            RestoreColor();
        }

        /// <summary>
        /// 召唤（替换）角色
        /// </summary>
        /// <param name="isPassive"></param>
        // todo 重写，这部分代码过于混乱了，整个角色部分都要重写
        public void SummonChara(bool isPassive)
        {

            GameObject unit = GameContext.Instance.GetPlayerBySite(PlayerSite).CharaObj;
            BucketEntity Bucket = GameContext.Instance.GetPlayerBySite(PlayerSite).CharaBucket;

            // 当前场上角色
            NewChara tempChara = GameContext.Instance.GetPlayerBySite(PlayerSite).CharaComponent;


            // >>> 调整部分
            NewBucket newBucket = NewBattleFieldManager.Instance.GetBucketBySerial(Bucket.serial);
            NewUnit newUnit = new NewChara(CardLoader.Instance.GetCardById(chara.unit.unitID) as UnitCard, newBucket);
            newUnit.owner.CharaComponent = newUnit as NewChara;
            unit.GetComponent<UnitDisplay>().FreshUnitUI(newUnit.GetView());
            // >>> 


            if (tempChara != null && tempChara.HP > 0)
            {

                GameContext.Instance.GetPlayerBySite(PlayerSite).HandCharaManager.CharaToCard(tempChara);
            }
            unit.gameObject.SetActive(true);
            SetImage();


            UnitEntity unitEntity = unit.GetComponent<UnitEntity>();
            BattleFieldManager.Instance.SetBucketCarryFlag(Bucket.serial, unitEntity);

            CharaBannerDisplay CharaOnBattle = GameContext.Instance.
                GetPlayerBySite(PlayerSite).HandCharaManager.CharaOnBattle.GetComponent<CharaBannerDisplay>();

            CharaOnBattle.Init(null, newUnit as NewChara, PlayerSite);
            CharaOnBattle.transform.localScale = Vector3.one;


            BanOperations(CharaOnBattle);

            if (!isPassive)
            {
                ISkill skill = chara.Warfare;
                if (skill.GetSkillType() == SkillType.Coming)
                {
                    MagicManager.Instance.SkillRequest(unitEntity, skill);
                }
            }

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
                string imgPath = "UnitModel/ModelImage/profileimage/" + chara.unitName;


                float imageSizeScale = 1.5f;

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

        public void RestoreColor()
        {
            GameObject LayOut = gameObject.transform.Find("布局").gameObject;
            LayOut.GetComponent<Image>().color = OriColor;
        }

        /// <summary>
        /// 禁止角色标签的操作（应用于最下方在场角色展示
        /// </summary>
        /// <param name="s"></param>
        public static void BanOperations(CharaBannerDisplay s)
        {
            //s.gameObject.GetComponent<Image>().raycastTarget = false;
            s.GetComponent<BoxCollider2D>().enabled = false;
            s.GetComponent<Image>().enabled = false;
        }

        /// <summary>
        /// 实时更新角色UI接口
        /// </summary>
        /// <param name="CurState">当前角色状态</param>
        public void RefreshUI(UnitEntity CurState)
        {
            atkText.text = CurState.ATK + "";
            hpText.text = CurState.HP + "";
            Chara s = CurState.unit as Chara;
            engText.text = s.MP + "";
            //TODO: 改变角色标签的各种条
        }

        /// <summary>
        /// 实时更新角色UI接口
        /// </summary>
        public void RefreshUI(int CurHP, int CurATK, int CurEng)
        {
            hpText.text = CurHP + "";
            atkText.text = CurATK + "";
            engText.text = CurEng + "";
            //TODO: 改变角色标签的各种条
        }

    }
}