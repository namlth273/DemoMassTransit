﻿namespace Demo.Publisher
{
    interface IConfig
    {
        int? NumberOfMessageToPublish { get; set; }
    }

    class WorkerConfig : IConfig
    {
        public int? NumberOfMessageToPublish { get; set; }
    }
}