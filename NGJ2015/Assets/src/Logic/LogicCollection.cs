using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Logic;

namespace Assets.Backend.GameLogic
{
    public class LogicCollection
    {
        private static LogicCollection _logicCollection;
        public static LogicCollection Instance
        {
            get
            {
                if(_logicCollection == null)
                {
                    _logicCollection = new LogicCollection();
                }
                return _logicCollection;
            }
        }

        private EnemyLogic _enemyLogic;
        public EnemyLogic EnemyLogic
        {
            get
            {
                if (_enemyLogic == null)
                {
                    _enemyLogic = new EnemyLogic();
                }
                return _enemyLogic;
            }
        }

        //private EventLogic _eventLogic;
        //public EventLogic EventLogic
        //{
        //    get
        //    {
        //        if (_eventLogic == null)
        //        {
        //            _eventLogic = new EventLogic();
        //        }
        //        return _eventLogic;
        //    }
        //}
    }
}
