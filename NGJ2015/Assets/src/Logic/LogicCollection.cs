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

        private GenericLogic _genericLogic;
        public GenericLogic GenericLogic
        {
            get
            {
                if (_genericLogic == null)
                {
                    _genericLogic = new GenericLogic();
                }
                return _genericLogic;
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
