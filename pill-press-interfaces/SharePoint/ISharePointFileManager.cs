using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Interfaces
{
    /// <summary>
    /// Minimal interface for SharePoint file management operations currently used in the application.
    /// Supports both on-premises and cloud SharePointManagers.
    /// </summary>
    public interface ISharePointFileManager
    {
        string WebName { get; }

        Task<bool> FolderExists(string listTitle, string folderName);

        Task<bool> DocumentLibraryExists(string listTitle);

        Task<object> CreateDocumentLibrary(
            string listTitle,
            string documentTemplateUrlTitle = null
        );

        Task CreateFolder(string listTitle, string folderName);

        Task<string> UploadFile(
            string fileName,
            string listTitle,
            string folderName,
            byte[] data,
            string contentType
        );
        Task<string> UploadFile(
            string fileName,
            string listTitle,
            string folderName,
            Stream fileData,
            string contentType
        );

        Task<string> AddFile(
            string folderName,
            string fileName,
            byte[] fileData,
            string contentType
        );

        Task<string> AddFile(
            string documentLibrary,
            string folderName,
            string fileName,
            byte[] fileData,
            string contentType
        );

        Task<string> AddFile(
            string folderName,
            string fileName,
            Stream fileData,
            string contentType
        );

        Task<string> AddFile(
            string documentLibrary,
            string folderName,
            string fileName,
            Stream fileData,
            string contentType
        );

        Task<byte[]> DownloadFile(string url);

        Task<bool> DeleteFile(string serverRelativeUrl);

        Task<bool> DeleteFile(string listTitle, string folderName, string fileName);

        Task<List<SharePointFileDetailsList>> GetFileDetailsListInFolder(
            string listTitle,
            string folderName,
            string documentType
        );

        string GetServerRelativeURL(string listTitle, string folderName);
    }
}
