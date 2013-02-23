using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace bjExtract
{
	class SelfExtractor : IDisposable
	{
		
        // Source file of standalone exe 
        protected List<string> sourceNames = new List<string>();

        public void AddSourceName(string sourcefilename)
        {
            sourceNames.Add(sourcefilename);
        }
        /*
        protected readonly string[] sourceName = {@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjViewer\bjViewer\Program.cs"
                                                 ,@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjViewer\bjViewer\Form1.cs"
                                                 ,@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjViewer\bjViewer\Form1.Designer.cs"
                                                 ,@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjViewer\bjViewer\clsXmlDocs.cs"
                                                 ,@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjViewer\bjViewer\Properties\AssemblyInfo.cs"
                                                 ,@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjViewer\bjViewer\Properties\Resources.Designer.cs"
                                                 ,@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjViewer\bjViewer\Properties\Settings.Designer.cs"
                                                  };
        */
		// Compressed files ready to embed as resource
		protected List<string> filenames = new List<string>();
        
        public void AddFile(string filename)
        {
            AddFile(filename, string.Empty);
        }

		public void AddFile(string filename, string guid)
		{
            string tempfilename = string.Empty;
            if (guid == string.Empty)
                tempfilename = Path.Combine(Program.temp, Path.GetFileName(filename)) + ".gz";
             else
                tempfilename = Path.Combine(Program.temp, guid) + ".gz";
            
            // Compress input file using System.IO.Compression
			using (Stream file = File.OpenRead(filename))
			{
				byte[] buffer = new byte[file.Length];

				if (file.Length != file.Read(buffer, 0, buffer.Length))
					throw new IOException("Unable to read " + filename);

                using (Stream gzFile = File.Create(tempfilename))
				{
					using (Stream gzip = new GZipStream(gzFile, CompressionMode.Compress))
					{
						gzip.Write(buffer, 0, buffer.Length);
					}
				}
			}
			// Store filename so we can embed it on CompileArchive() call
            filenames.Add(tempfilename);
		}

		public void CompileArchive(string archiveFilename)
		{
			CompileArchive(archiveFilename, false, null);
		}

		public void CompileArchive(string archiveFilename, bool run1stItem, string iconFilename)
		{
			CodeDomProvider csc = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();

			cp.GenerateExecutable = true;
			cp.OutputAssembly = archiveFilename;
			cp.CompilerOptions = "/target:winexe";

			// Custom option to run a file after extraction  
			if (run1stItem)
			{
				cp.CompilerOptions += " /define:RUN_1ST_ITEM";
			}
			if (!string.IsNullOrEmpty(iconFilename))
			{
				cp.CompilerOptions += " /win32icon:" + iconFilename;
			}
			cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            cp.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            cp.ReferencedAssemblies.Add("System.Data.dll");
            cp.ReferencedAssemblies.Add("System.Deployment.dll");
            cp.ReferencedAssemblies.Add("System.Drawing.dll");
            cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            cp.ReferencedAssemblies.Add("System.Xml.dll");

			// Add compressed files as resource
			cp.EmbeddedResources.AddRange(filenames.ToArray());	

			// Compile standalone executable with input files embedded as resource
            CompilerResults cr = csc.CompileAssemblyFromFile(cp, sourceNames.ToArray());

			// yell if compilation error
			if (cr.Errors.Count > 0)
			{
				string msg = "Errors building " + cr.PathToAssembly;

				foreach (CompilerError ce in cr.Errors)
				{
					msg += Environment.NewLine + ce.ToString();
				}
				throw new ApplicationException(msg);
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			foreach (string path in filenames)
			{
				File.Delete(path);
			}
			filenames.Clear();

			GC.SuppressFinalize(this); 
		}

		#endregion
	}
}
