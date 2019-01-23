/********************************************************************************
** Company：  
** auth：    lingyc
** date：    2018/11/29 18:28:22
** desc：    Threading类
** Ver.:     V1.0.0
*********************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Infrastructure.Threading
{
    //线程池
    public class ThreadPool
    {
        bool PoolEnable = false; //线程池是否可用 
        List<Thread> ThreadContainer = null; //线程的容器
        ConcurrentQueue<ActionData> JobContainer = null; //任务的容器
        public ThreadPool(int threadNumber)
        {
            PoolEnable = true;
            ThreadContainer = new List<Thread>(threadNumber);
            JobContainer = new ConcurrentQueue<ActionData>();
            for (int i = 0; i < threadNumber; i++)
            {
                var t = new Thread(RunJob);
                ThreadContainer.Add(t);
                t.Start();
            }
        }
        //向线程池添加一个任务
        public void AddTask(Action<object> job, object obj, Action<Exception> errorCallBack = null)
        {
            if (JobContainer != null)
            {
                JobContainer.Enqueue(new ActionData { Job = job, Data = obj, ErrorCallBack = errorCallBack });
            }

        }
        //终止线程池
        public void FinalPool()
        {
            PoolEnable = false;
            JobContainer = null;
            if (ThreadContainer != null)
            {
                foreach (var t in ThreadContainer)
                {
                    //强制线程退出并不好，会有异常
                    //t.Abort();
                    t.Join();
                }
                ThreadContainer = null;
            }

        }
        private void RunJob()
        {
            while (true && JobContainer != null && PoolEnable)
            {
                //任务列表取任务
                ActionData job = null;
                JobContainer?.TryDequeue(out job);
                if (job == null)
                {
                    //如果没有任务则休眠
                    Thread.Sleep(10);
                    continue;
                }
                try
                {
                    //执行任务
                    job.Job.Invoke(job.Data);
                }
                catch (Exception error)
                {
                    //异常回调
                    job?.ErrorCallBack(error);
                }
            }
        }
    }

    public class ActionData
    {
        //执行任务的参数
        public object Data { get; set; }
        //执行的任务
        public Action<object> Job { get; set; }
        //发生异常时候的回调方法
        public Action<Exception> ErrorCallBack { get; set; }
    }
}
