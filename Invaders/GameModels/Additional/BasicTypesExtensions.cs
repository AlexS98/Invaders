using System;
using System.Text;
using System.Threading;

namespace Invaders.GameModels.Additional
{
    public static class BasicTypesExtensions
    {
        public static int IndexOf(this StringBuilder sb, Char value)
        {
            for (int index = 0; index < sb.Length; index++)
                if (sb[index] == value) return index;
            return -1;
        }

        public static void InvokeAndCatch<TException>(this Action<Object> _del, Object arg, Action catchAction = null) 
                                    where TException : Exception
        {
            try
            {
                _del(arg);
            }
            catch (TException)
            {
                catchAction?.Invoke();
            }
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T t = b;
            b = a;
            a = t;
        }

        public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventDelegate)
        {
            Volatile.Read(ref eventDelegate)?.Invoke(sender, e);
        }
    }
}
