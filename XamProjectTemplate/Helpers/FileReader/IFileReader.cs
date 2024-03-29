﻿using System.Threading;
using System.Threading.Tasks;

namespace XamProjectTemplate.Helpers.FileReader
{
    public interface IFileReader
    {
        void SetDelegate(IFileConnector weakReference);
        Task ReadFile(string fileName, CancellationToken ct, int wsType);
    }
}
