using System.Collections.Generic;
using cfg.level;
using DataScripts;

namespace GameSystem.LevelSystem.EventSystem
{
    /// <summary>
    /// 以后再说吧现在懒得写
    /// </summary>
    public class EventController
    {
        public int EventId;

        public EventItem EventItem;

        public EventController(int eventId)
        {
            EventId = eventId;
            EventItem = LubanLoader.GetTables().EventItems.Get(EventId);
        }
        
        public void GetReward()
        {
            
        }
    }
}