﻿namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}