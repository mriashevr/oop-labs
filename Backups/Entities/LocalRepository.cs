﻿using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Backups.Entities
{
    public class LocalRepository : IRepository
    {
        public LocalRepository(DirectoryInfo directory)
        {
            DirectoryPath = directory;
        }

        public DirectoryInfo DirectoryPath { get; }

        public void CreateDirectory(string directoryName)
        {
            Directory.CreateDirectory($"{DirectoryPath}/{directoryName}");
        }

        public List<Storage> StartBackUp(List<Storage> localStorages, string newDirectory)
        {
            var newStorages = new List<Storage>();
            foreach (Storage storage in localStorages)
            {
                var newStorage = new Storage();
                var zipFile = new ZipFile();
                foreach (JobObject jobObject in storage.ReservedJobObjects)
                {
                    string jobObjectName = jobObject.Path.Substring(jobObject.Path.LastIndexOf('/') + 1);
                    string newPath = $@"{DirectoryPath.FullName}/{newDirectory}/{jobObjectName}.zip";
                    var newJobObject = new JobObject(newPath);
                    newStorage.ReservedJobObjects.Add(newJobObject);
                    zipFile.AddFile(jobObject.Path);
                }

                newStorages.Add(newStorage);
                zipFile.Save($@"{DirectoryPath.FullName}/{newDirectory}/{IdGenerator.NewId()}.zip");
            }

            return newStorages;
        }
    }
}