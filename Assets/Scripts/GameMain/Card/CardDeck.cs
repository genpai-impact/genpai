
using System;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// ����
    /// </summary>
    public class CardDeck
    {
        public PlayerID owner;

        public static int S_HandCardLimit = 10;

        /// <summary>
        /// ���ϳ��Ŀ���
        /// </summary>
        LinkedList<Card> CardLibrary = new LinkedList<Card>();
        /// <summary>
        /// ����
        /// </summary>
        LinkedList<Card> HandCardList = new LinkedList<Card>();
        /// <summary>
        /// ���ÿ���
        /// </summary>
        LinkedList<Card> UsedList = new LinkedList<Card>();
        /// <summary>
        /// ���ϳ���Ӣ��
        /// </summary>
        // LinkedList<Hero> HeroLibrary = new LinkedList<Hero>();
        public void Init()
        {

        }

    }
}

