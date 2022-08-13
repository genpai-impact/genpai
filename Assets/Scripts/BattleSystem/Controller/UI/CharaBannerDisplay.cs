using System.Linq;
using BattleSystem.Controller.Animator;
using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.Unit;
using BattleSystem.Controller.Unit.UnitView;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using BattleSystem.Service.Unit;
using DataScripts;
using DataScripts.DataLoader;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

namespace BattleSystem.Controller.UI
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
        
        //public UnitView.UnitView unitView;
        
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

		private UnitInfoDisplay unitinfodisplay = null;
        void Start()
        {
            OriColor = gameObject.transform.Find("布局").gameObject.GetComponent<Image>().color;
        }

        public void Init(CharaBannerHead _title, Chara _chara, BattleSite _site)
        {
            chara = _chara;
            PlayerSite = _site;
            Title = _title;

            charaName.text = chara.UnitName;
            atkText.text = chara.Atk.ToString();
            hpText.text = chara.Hp.ToString();
            engText.text = chara.MP.ToString();
            
            //unitView = GetComponent<CharaBanner>().UnitView;

            SetImage();

        }

        public void CDDisplay()
        {
            if (GameContext.GetPlayerBySite(PlayerSite).CharaCD == 0)
            {
                CDImage.gameObject.SetActive(false);
                CDText.gameObject.SetActive(false);
            }
            else
            {
                CDImage.gameObject.SetActive(true);
                CDText.gameObject.SetActive(true);
                CDText.text = " CD:\n" + GameContext.GetPlayerBySite(PlayerSite).CharaCD + "回合";
            }
        }

        // todo 正确的写法是，在CharaPlayerController中使用OnMouseDown
        public void OnMouseDown()
        {
            GenpaiMouseDown();
        }

        //鼠标掠过实现卡牌放大
        public void OnMouseOver()
        {
            Vector3 maxCardSize = new Vector3(1.25f, 1.25f, 1.25f);
            //放大的最大倍数，1.25倍
            if (charaImage.transform.localScale.x <= 1.25f)
            {
                charaImage.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                //想实现缓慢放大的效果，不太会DOTWeen，不太清楚这样写行不行
            }
            else
            {
                charaImage.transform.localScale = maxCardSize;
                //大于1.25之后，值直接设置为1.25
            }
        }

        protected override void DoGenpaiMouseDown()
        {
            if (GameContext.GetPlayerBySite(PlayerSite).CharaCD == 0)
            {
                SummonChara(false);
                GameContext.GetPlayerBySite(PlayerSite).CharaCD = GameContext.MissionConfig.CharaCD;
                GameContext.GetPlayerBySite(PlayerSite).CharaManager.CDRefresh();
               
            }
            else
            {
                Debug.Log("CD未结束");
                // todo 给个动画或者弹窗告诉玩家现在cd还没到
            }
        }


        public void OnPointerEnter(PointerEventData e)
        {
            HighLight();
			Invoke("ShowInfo", 0.1f);
        }
		


        public void OnPointerExit(PointerEventData e)
        {
            RestoreColor();
            Invoke("UnShowInfo",0.01f);

        }


        /// <summary>
        /// 召唤（替换）角色
        /// </summary>
        /// <param name="isPassive"></param>
        public void SummonChara(bool isPassive)
        {
            
            // 预存场上角色
            Chara tempChara = GameContext.GetPlayerBySite(PlayerSite).Chara;

            // 场上角色回手
            if (tempChara != null && tempChara.Hp > 0)
            {
                GameContext.GetPlayerBySite(PlayerSite).CharaManager.CharaReturnHand(tempChara);
            }
            
            // 储存单位绑定上场
            BattleFieldManager.Instance.SetBucketCarryFlag(chara.Carrier.serial);
            chara.Init();

            GameContext.GetPlayerBySite(PlayerSite).Chara = chara;
            GameObject charaObj = GameContext.GetPlayerBySite(PlayerSite).CharaObj;

            // 主动切换才更新，被动切换等FallAnimator调用
            if (!isPassive)
            {
                AnimatorManager.Instance.InsertAnimatorTimeStep(AnimatorGenerator.GenerateSummonTimeStep(charaObj, chara));
            }
            
          
            // 调整角色实体
            BucketEntity Bucket = GameContext.GetPlayerBySite(PlayerSite).CharaBucket;

            UnitEntity unitEntity = charaObj.GetComponent<UnitEntity>();
            unitEntity.Init(PlayerSite, Bucket);
            BucketEntityManager.Instance.SetBucketCarryFlag(Bucket.serial, unitEntity);
            
            // AnimatorManager.Instance.InsertAnimatorTimeStep(AnimatorGenerator.GenerateSummonTimeStep(unitEntity.gameObject,chara));

          
            // 调整主Banner
            CharaBannerDisplay CharaBanner = GameContext.GetPlayerBySite(PlayerSite).CharaManager.CurrentCharaBanner.GetComponent<CharaBannerDisplay>();

            Debug.Log("节点3");
            CharaBanner.Init(null, chara, PlayerSite);
            CharaBanner.transform.localScale = Vector3.one;
            
           
            BanOperations(CharaBanner);


            // GameContext.GetPlayerBySite(PlayerSite).CharaManager.RefreshCharaUI(chara.GetView());
           
            //柳星yanashi：运行此部分，在刻晴时会报错，无法运行到最后的销毁场下卡牌区
            // 施放出场技
            //if (!isPassive)  // 如果不带这个if会一秒报100错，是什么重要的事情需要每时每刻都不停的判断？
            //{
            //    SkillManager.Instance.SkillRequest(LubanLoader.GetTables().CardItems.DataList.Single(chara => chara.Id == unitEntity.GetUnit().BaseUnit.UnitID).BaseSkill, unitEntity);
            //}

            Debug.Log("删除节点");
            // 删除对应收起标题框
            GameContext.GetPlayerBySite(PlayerSite).CharaManager.Remove(Title);


            Destroy(Title.gameObject);
            // 删除自身
            Destroy(this.gameObject);
            
        }


        /// <summary>
        /// 显示卡牌：将卡牌数据与UI绑定
        /// </summary>
        public async void SetImage()
        {
            try
            {
                const float imageSizeScale = 1.5f;

                Sprite sprite = await Addressables.LoadAssetAsync<Sprite>(chara.UnitName + "_Head").Task;
                charaImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                charaImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log( chara.UnitName+"无头像");
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
            atkText.text = chara.Atk + "";
            hpText.text = chara.Hp + "";
            engText.text = chara.MP + "";
            //TODO: 改变角色标签的各种条
        }

        /// <summary>
        /// 实时更新角色UI接口
        /// </summary>
        public void RefreshUI(UnitView unitView)
        {
            charaName.text = chara.UnitName;
            hpText.text = unitView.Hp.ToString();
            atkText.text = unitView.Atk.ToString();
            engText.text = unitView.Mp.ToString();
            //TODO: 改变角色标签的各种条
            SetImage();
        }

        public void ShowInfo()
        {
            unitinfodisplay = PrefabsLoader.Instance.infoCard.GetComponent<UnitInfoDisplay>();
            unitinfodisplay.Init(chara.GetView());
            unitinfodisplay.Display();//InfoCardType.MonsterOnBattleInfo 原来有这个类型的传参
        }
        
        public void UnShowInfo()
        {
            unitinfodisplay.isHide = true;
        }
    }
}