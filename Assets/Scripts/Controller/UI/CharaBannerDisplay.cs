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
        public Chara chara;

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

        public void Init(CharaBannerHead _title, Chara _chara, BattleSite _site)
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
                GameContext.Instance.GetPlayerBySite(PlayerSite).CharaManager.CDRefresh();
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
        public void SummonChara(bool isPassive)
        {
            // Debug.Log("Summon Chara" + chara.GetView().unitName);

            // 预存场上角色
            Chara tempChara = GameContext.Instance.GetPlayerBySite(PlayerSite).Chara;

            // 场上角色回手
            if (tempChara != null && tempChara.HP > 0)
            {
                GameContext.Instance.GetPlayerBySite(PlayerSite).CharaManager.CharaReturnHand(tempChara);
            }


            // 储存单位绑定上场
            // TODO：进一步分离
            // Debug.Log("Change" + BattleFieldManager.Instance.GetBucketBySerial(chara.carrier.serial).unitCarry.unitName + "To" + chara.unitName);
            BattleFieldManager.Instance.SetBucketCarryFlag(chara.carrier.serial);
            chara.Init();

            GameContext.Instance.GetPlayerBySite(PlayerSite).Chara = chara;


            // 显示角色
            GameObject unitSeat = GameContext.Instance.GetPlayerBySite(PlayerSite).CharaObj;
            unitSeat.gameObject.SetActive(true);
            unitSeat.GetComponent<UnitDisplay>().Init(chara.GetView());


            // 调整角色实体
            BucketEntity Bucket = GameContext.Instance.GetPlayerBySite(PlayerSite).CharaBucket;

            UnitEntity unitEntity = unitSeat.GetComponent<UnitEntity>();
            unitEntity.Init(PlayerSite, Bucket);
            BucketEntityManager.Instance.SetBucketCarryFlag(Bucket.serial, unitEntity);


            // 调整主Banner
            CharaBannerDisplay CharaBanner = GameContext.Instance.
                GetPlayerBySite(PlayerSite).CharaManager.CurrentCharaBanner.GetComponent<CharaBannerDisplay>();


            CharaBanner.Init(null, chara, PlayerSite);
            CharaBanner.transform.localScale = Vector3.one;

            BanOperations(CharaBanner);


            GameContext.Instance.GetPlayerBySite(PlayerSite).CharaManager.RefreshCharaUI(chara.GetView());


            // 出场技唤醒
            if (!isPassive)
            {
                ISkill skill = chara.Warfare;
                if (skill.GetSkillType() == SkillType.Coming)
                {
                    MagicManager.Instance.SkillRequest(unitEntity, skill);
                }
            }


            // 删除对应收起标题框
            GameContext.Instance.GetPlayerBySite(PlayerSite).CharaManager.Remove(Title);



            Destroy(Title.gameObject);
            // 删除自身
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
        public void RefreshUI()
        {
            atkText.text = chara.ATK + "";
            hpText.text = chara.HP + "";
            engText.text = chara.MP + "";
            //TODO: 改变角色标签的各种条
        }

        /// <summary>
        /// 实时更新角色UI接口
        /// </summary>
        public void RefreshUI(UnitView unitView)
        {
            charaName.text = chara.unitName;
            hpText.text = unitView.HP.ToString();
            atkText.text = unitView.ATK.ToString();
            engText.text = unitView.MP.ToString();
            //TODO: 改变角色标签的各种条
            SetImage();
        }

    }
}