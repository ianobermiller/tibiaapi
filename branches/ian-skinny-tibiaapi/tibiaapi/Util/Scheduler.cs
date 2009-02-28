using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Tibia.Util
{
    public static class Scheduler
    {
        public static void AddTask(Delegate ev, object[] paramArray, int time)
        {
            lock ("addEvent")
            {
                Action<Delegate, object[], int> myDelegate = new Action<Delegate, object[], int>(AddTaskDelay);
                myDelegate.BeginInvoke(ev, paramArray, time, null, null);
            }
        }

        private static void AddTaskDelay(Delegate ev, object[] paramArray, int time)
        {
            System.Threading.Thread.Sleep(time);
            bool bFired;

            if (ev != null)
            {
                foreach (Delegate singleCast in ev.GetInvocationList())
                {
                    bFired = false;
                    try
                    {
                        ISynchronizeInvoke syncInvoke = (ISynchronizeInvoke)singleCast.Target;
                        if (syncInvoke != null && syncInvoke.InvokeRequired)
                        {
                            bFired = true;
                            syncInvoke.BeginInvoke(singleCast, paramArray);
                        }
                        else
                        {
                            bFired = true;
                            singleCast.DynamicInvoke(paramArray);
                        }
                    }
                    catch (Exception)
                    {
                        if (!bFired)
                            singleCast.DynamicInvoke(paramArray);
                    }
                }
            }
        }
    }
}
