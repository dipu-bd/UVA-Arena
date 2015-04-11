using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UVA_Arena
{
    internal static class TaskQueue
    {
        public class Task
        {
            public int timeout;
            public object data;
            public Function func;
            public Function2 func2;

            public Task(Function f, int time)
            {
                func = f;
                data = null;
                timeout = time;
            }

            public Task(Function2 f2, object dat, int time)
            {
                func2 = f2;
                data = dat;
                timeout = time;
            }

            public void Call()
            {
                if (data == null) func();
                else func2(data);
            }
        }

        public static Timer timer1;

        public delegate void Function();

        public delegate void Function2(object data);

        public static List<Task> queue = new List<Task>();

        public static void StartTimer()
        {
            if (timer1 != null) return;
            timer1 = new Timer();
            timer1.Interval = 100;
            timer1.Tick += timer1_Tick;
            timer1.Enabled = true;
        }

        public static Task AddTask(Function2 func2, object data, int timeout)
        {
            return AddTask(new Task(func2, data, timeout));
        }

        public static Task AddTask(Function func, int timeout)
        {
            return AddTask(new Task(func, timeout));
        }

        public static Task AddTask(Task t)
        {
            StartTimer();
            queue.Add(t);
            return t;
        }

        private static void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < queue.Count; ++i)
            {
                queue[i].timeout -= timer1.Interval;
                if (queue[i].timeout < 10)
                {
                    queue[i].Call();
                    queue.RemoveAt(i--);
                }
            }
        }
    }
}