using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UVA_Arena
{
    public static class TaskQueue
    {
        public class Task
        {
            public Function func;
            public int timeout;            

            public Task(Function f, int time)
            {
                func = f;
                timeout = time;
            }
        }

        public static Timer timer1;
        public delegate void Function();
        public static List<Task> queue = new List<Task>();

        public static void StartTimer()
        {
            if (timer1 != null) return;
            timer1 = new Timer();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            timer1.Enabled = true;
        }

        public static void AddTask(Function func, int timeout)
        {
            AddTask(new Task(func, timeout));
        }
        public static void AddTask(Task t)
        {
            StartTimer();
            queue.Add(t);
        }
        private static void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < queue.Count; ++i)
            {
                queue[i].timeout -= timer1.Interval;
                if (queue[i].timeout < 10)
                {
                    queue[i].func();
                    queue.RemoveAt(i);
                    --i;
                }
            }
        }
    }
}
