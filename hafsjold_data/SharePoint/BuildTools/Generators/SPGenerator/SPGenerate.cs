using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class SPGenerate
{
    static void Main(string[] args)
    {
        GenerateMetadata.GenerateMetadataMain(args);
        GenerateColumns.MainGenerateColumns(args);
        GenerateTypes.GenerateTypesMain(args);
        GenerateList.MainGenerateList(args);
    }
}

