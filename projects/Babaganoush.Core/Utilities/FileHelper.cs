using System;
using System.IO;
using System.Reflection;
using Babaganoush.Core.Extensions;
using Babaganoush.Core.Wrappers;
using Babaganoush.Core.Wrappers.Interfaces;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// A file helper.
    /// </summary>
    public static class FileHelper
    {
        private static readonly IFileSystem _fileSystem = new FileSystemWrapper();
        private static readonly IHttpContext _httpContext = new SystemHttpContextWrapper();

        /// <summary>
        /// Gets the physical path to virtual.
        /// </summary>
        ///
        /// <param name="file">The file.</param>
        /// <param name="mustExist">(Optional) if set to <c>true</c> [must exist].</param>
        ///
        /// <returns>
        /// The physical path.
        /// </returns>
        public static string GetPhysicalPath(string file, bool mustExist = false)
        {
            //VALIDATE INPUT
            if (string.IsNullOrWhiteSpace(file))
            {
                return string.Empty;
            }

            //GET ABSOLUTE PATH
            if (!Path.IsPathRooted(file))
            {
                file = _httpContext.MapPath(file);
            }

            //VALIDATE EXISTS
            return mustExist && !_fileSystem.Exists(file) ? string.Empty : file;
        }

        /// <summary>
        /// Gets the virtual path.
        /// </summary>
        ///
        /// <param name="file">The file.</param>
        ///
        /// <returns>
        /// The virtual path.
        /// </returns>
        public static string GetVirtualPath(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
            {
                return string.Empty;
            }

            return "~/" + file
                .Replace(_httpContext.GetPhysicalApplicationPath(), string.Empty)
                .Replace("\\", "/");
        }

        /// <summary>
        /// Search for files in a directory and return list of virtual paths.
        /// </summary>
        ///
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        ///
        /// <returns>
        /// An array of string.
        /// </returns>
        public static string[] GetVirtualFiles(string directoryPath, string searchPattern)
        {
            string[] fileList = null;

            try
            {
                fileList = _fileSystem.GetFiles(GetPhysicalPath(directoryPath), searchPattern, SearchOption.AllDirectories);

                // Convert each file path to virtual path
                for (int i = 0; i < fileList.Length; i++)
                {
                    fileList[i] = GetVirtualPath(fileList[i]);
                }
            }
            catch (Exception)
            {
                //TODO: LOG ERROR
            }

            return fileList;
        }

        /// <summary>
        /// Determines whether [is file locked] [the specified file].
        /// </summary>
        ///
        /// <param name="file">The file.</param>
        ///
        /// <returns>
        /// true if file locked, false if not.
        /// </returns>
        public static bool IsFileLocked(string file)
        {
            // Convert to physical file path
            file = GetPhysicalPath(file);

            try
            {
                FileStream fileStream = _fileSystem.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                fileStream.Close();
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the next available file that doesn't exist.
        /// </summary>
        ///
        /// <param name="file">The file.</param>
        ///
        /// <returns>
        /// The next available file.
        /// </returns>
        public static string GetNextAvailableFile(string file)
        {
            // Convert to physical file path
            file = GetPhysicalPath(file);

            string uniqueFile = file;
            int i = 2;

            while (_fileSystem.Exists(uniqueFile))
            {
                uniqueFile = file.Substring(0, file.LastIndexOf('.')) + "_" + i + Path.GetExtension(file);
                i++;
            }

            return uniqueFile;
        }

        /// <summary>
        /// Copies the directory.
        /// </summary>
        ///
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destPath">The dest path.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public static bool CopyDirectory(string sourcePath, string destPath, bool overwrite)
        {
            sourcePath = GetPhysicalPath(sourcePath);
            destPath = GetPhysicalPath(destPath);

            var sourceDir = new DirectoryInfo(sourcePath);
            var destDir = new DirectoryInfo(destPath);

            try
            {
                if (sourceDir.Exists)
                {
                    if (!destDir.Exists)
                    {
                        destDir.Create();
                    }

                    // Copy sub-files
                    foreach (var file in sourceDir.GetFiles())
                    {
                        if (overwrite)
                        {
                            file.CopyTo(Path.Combine(destDir.FullName, file.Name), true);
                        }
                        else if (!_fileSystem.Exists(Path.Combine(destDir.FullName, file.Name)))
                        {
                            file.CopyTo(Path.Combine(destDir.FullName, file.Name), false);
                        }
                    }

                    // Copy sub-directories
                    foreach (var dir in sourceDir.GetDirectories())
                    {
                        CopyDirectory(dir.FullName, Path.Combine(destDir.FullName, dir.Name), overwrite);
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                //TODO: LOG ERROR
            }

            return false;
        }

        /// <summary>
        /// Gets the safe file exts.
        /// </summary>
        ///
        /// <returns>
        /// An array of string.
        /// </returns>
        public static string[] GetSafeFileExts()
        {
            return new[] { 
                ".jpg",
                ".jpeg",
                ".gif",
                ".png",
                ".bmp",
                ".tif",
                ".tiff",
                ".txt",
                ".rtf",
                ".doc",
                ".docx",
                ".xls",
                ".xlsx",
                ".pps",
                ".pdf",
                ".mp3",
                ".wav",
                ".avi",
                ".mpg",
                ".mpeg",
                ".wmv",
                ".iso",
                ".flv",
                ".mov",
                ".dvr",
                ".vob",
                ".zip",
                ".tar",
                ".rar",
                ".psd",
                ".ai"
            };
        }

        /// <summary>
        /// Saves the stream to file.
        /// </summary>
        ///
        /// <param name="fileFullPath">The file full path.</param>
        /// <param name="stream">The stream.</param>
        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0)
            {
                return;
            }

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = _fileSystem.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                var bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        /// <summary>
        /// Gets plain text file's contents.
        /// </summary>
        ///
        /// <param name="file">The file.</param>
        ///
        /// <returns>
        /// The content.
        /// </returns>
        public static string GetContent(string file)
        {
            // Convert to physical file path
            file = GetPhysicalPath(file);

            using (StreamReader sr = new StreamReader(file))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Updates plain text file's contents.
        /// </summary>
        ///
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        public static void UpdateContent(string file, string contents)
        {
            string originalContents = GetContent(file);

            file = GetPhysicalPath(file);

            if (contents != originalContents)
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.Write(contents);
                }
            }
            else
            {
                // Update last modified date only
                FileInfo fi = new FileInfo(file);
                fi.LastWriteTime = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Finds the line containing <paramref name="linePattern"/>.
        /// </summary>
        ///
        /// <param name="linePattern">The line pattern.</param>
        /// <param name="file">The file.</param>
        ///
        /// <returns>
        /// The found line containing.
        /// </returns>
        public static string FindLineContaining(string linePattern, string file)
        {
            string contents = GetContent(file);
            return contents.FindLineContainingString(linePattern);
        }

        /// <summary>
        /// Gets embedded resource.
        /// </summary>
        ///
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="type">(Optional) the type.</param>
        ///
        /// <returns>
        /// The embedded resource as a string.
        /// </returns>
        public static string GetEmbeddedResource(string resourceName, Type type = null)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentNullException("resourceName");
            }

            //RESOLVE ASSEMBLY BY PARAMETER OR AUTOMATICALLY
            Assembly assembly = type != null ? type.Assembly : Assembly.GetExecutingAssembly();

            //STREAM DATA
            Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
            if (manifestResourceStream == null)
            {
                return string.Empty;
            }

            using (var reader = new StreamReader(manifestResourceStream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Creates an empty file at <paramref name="path"/> and immediately closes it, to simulate "touch" operation.
        /// Allows for file to be accessed without getting exceptions related to the file already being open.
        /// </summary>
        public static void CreateEmptyFile(string path)
        {
            _fileSystem.Create(path).Dispose();
        }
    }
}
