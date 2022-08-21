using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class CardDTO{
    public  int CardID;
    //public string CardType;
    public string CardName;
    public string[] CardInfo;

    public CardDTO() { }

    public CardDTO(int id, cfg.card.CardType cardType, string cardName, string[] cardInfo)
    {
        CardID = id;
        CardName = cardName;
        CardInfo = cardInfo;
    }

    public object Clone()  // 此方法目前只看到给CardDeck用
    {
        return MemberwiseClone();
    }
}
