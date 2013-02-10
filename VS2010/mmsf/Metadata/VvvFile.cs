// <copyright>
//     Copyright (c) Victor Derks. See README.TXT for the details of the software licence.
// </copyright>

using System.IO;

namespace Metadata
{
    internal class VvvFile
    {
        internal VvvFile(Stream stream)
        {
            Label = "LABEL PLACEHOLDER";
            FileCount = 5;
        }

        internal string Label { get; set; }

        internal int FileCount { get; set; }
    }
}
