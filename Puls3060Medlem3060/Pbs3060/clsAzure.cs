using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace nsPbs3060
{
    public class clsAzure
    {
        CloudBlobContainer m_container;

        public clsAzure()
        {
            string storageacc = ConfigurationManager.AppSettings["StorageConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageacc);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            m_container = blobClient.GetContainerReference("pbsfiles");
            m_container.CreateIfNotExists();
        }

        public void uploadBlob(string PBSFilename, byte[] b_PBSFile, bool bTilPBS)
        {
            CloudBlockBlob blockBlob = m_container.GetBlockBlobReference(PBSFilename);
            blockBlob.UploadFromByteArray(b_PBSFile, 0, b_PBSFile.Length);
            if (bTilPBS)
            {
                blockBlob.Metadata["sendt_af"] = "Puls3060";
            }
            else 
            {
                blockBlob.Metadata["sendt_af"] = "PBS";           
            }
            blockBlob.Metadata["sendt_dato"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            blockBlob.SetMetadata();
        }
    }
}
