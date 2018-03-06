using System;

namespace Invaders.UIHelpers
{
    public class HireEventArgs : EventArgs
    {
        private readonly String _mFrom, _mTo, _mType;

        public String From { get { return _mFrom; } }
        public String To { get { return _mTo; } }
        public String WariorType { get { return _mType; } }

        public HireEventArgs(string mFrom, string mTo, string mType)
        {
            this._mFrom = mFrom;
            this._mTo = mTo;
            this._mType = mType;
        }
    }
}
