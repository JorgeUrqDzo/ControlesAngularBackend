using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClbNegControles;
using System.IO;
using WebApiControles.Models;
using ClbModControles;
using ClbModControles.Documentos;

namespace WebApiControles
{
    public class Utils
    {
        public string FileName { get; set; }
        public string TempFolder { get; set; }
        public int MaxFileSizeMB { get; set; }
        public List<String> FileParts { get; set; }
        private readonly clsNegUploadFiles _negUploadFiles;
        public Response objResponse = new Response();

        public Utils()
        {
            _negUploadFiles = new clsNegUploadFiles();
            FileParts = new List<string>();
        }

        public Response MergeFile(string FileName, clsModUploadFiles Documento)
        {
            bool rslt = false;

            string partToken = ".part_";
            string baseFileName = FileName.Substring(0, FileName.IndexOf(partToken));
            string trailingTokens = FileName.Substring(FileName.IndexOf(partToken) + partToken.Length);
            int FileIndex = 0;
            int FileCount = 0;
            int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
            int.TryParse(trailingTokens.Substring(trailingTokens.IndexOf(".") + 1), out FileCount);

            string Searchpattern = Path.GetFileName(baseFileName) + partToken + "*";
            string[] FilesList = Directory.GetFiles(Path.GetDirectoryName(FileName), Searchpattern);
            FileStream FS = null;
            Response objResponse = new Response();
            objResponse.Success = false;

            if (FilesList.Count() == FileCount)
            {

                if (!MergeFileManager.Instance.InUse(baseFileName))
                {
                    MergeFileManager.Instance.AddFile(baseFileName);
                    if (File.Exists(baseFileName))
                        File.Delete(baseFileName);

                    List<SortedFile> MergeList = new List<SortedFile>();
                    foreach (string Files in FilesList)
                    {
                        SortedFile sFile = new SortedFile();
                        sFile.FileName = Files;
                        baseFileName = Files.Substring(0, Files.IndexOf(partToken));
                        trailingTokens = Files.Substring(Files.IndexOf(partToken) + partToken.Length);
                        int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
                        sFile.FileOrder = FileIndex;
                        MergeList.Add(sFile);
                    }

                    var MergeOrder = MergeList.OrderBy(s => s.FileOrder).ToList();

                    using (FS = new FileStream(baseFileName, FileMode.Create))
                    {

                        foreach (var chunk in MergeOrder)
                        {
                            try
                            {
                                using (FileStream fileChunk = new FileStream(chunk.FileName, FileMode.Open))
                                {
                                    fileChunk.CopyTo(FS);

                                }
                                File.Delete(chunk.FileName);
                            }
                            catch (IOException)
                            {
                                // handle                                
                            }
                        }
                        var tam = FS.Length;

                    }

                    var GUI = Guid.NewGuid();
                    byte[] arr = File.ReadAllBytes(baseFileName);
                    Documento.Archivo = arr;
                    var docu = Documento;

                    if (Documento.IsFisico == true)
                    {
                    
                        clsModErrorBase objModErrorBase = new clsModErrorBase();

                        var IdDoc = _negUploadFiles.AgregarLargeFile(Documento.TablaRelacion, Documento.IdRelacion, Documento);

                    }

                    if (Documento.IsFisico == false)
                    {
                        clsModErrorBase objModErrorBase = new clsModErrorBase();

                        var IdDoc = _negUploadFiles.Agregar(Documento.TablaRelacion, Documento.IdRelacion, Documento);

                        File.Delete(baseFileName);
                    }

                    objResponse.Success = true;
                    MergeFileManager.Instance.RemoveFile(baseFileName);
                }
            }

            return objResponse;
        }

        public byte[] getArrayFile(Stream file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }


        public struct SortedFile
        {
            public int FileOrder { get; set; }
            public String FileName { get; set; }
        }
        public class MergeFileManager
        {
            private static MergeFileManager instance;
            private List<string> MergeFileList;

            private MergeFileManager()
            {
                try
                {
                    MergeFileList = new List<string>();
                }
                catch { }
            }

            public static MergeFileManager Instance
            {
                get
                {
                    if (instance == null)
                        instance = new MergeFileManager();
                    return instance;
                }
            }

            public void AddFile(string BaseFileName)
            {
                MergeFileList.Add(BaseFileName);
            }

            public bool InUse(string BaseFileName)
            {
                return MergeFileList.Contains(BaseFileName);
            }

            public bool RemoveFile(string BaseFileName)
            {
                return MergeFileList.Remove(BaseFileName);
            }
        }
    }
}