using System;

namespace Invaders.UIHelpers
{
    public class HireEventArgs : EventArgs
    {
        private readonly String m_from, m_to, m_type;

        public String From { get { return m_from; } }
        public String To { get { return m_to; } }
        public String WariorType { get { return m_type; } }

        public HireEventArgs(string m_from, string m_to, string m_type)
        {
            this.m_from = m_from;
            this.m_to = m_to;
            this.m_type = m_type;
        }
    }
}
