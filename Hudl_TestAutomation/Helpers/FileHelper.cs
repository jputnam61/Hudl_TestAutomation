using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Hudl_TestAutomation.Helpers.DataHelpers
{
        /// <summary>
            /// Class to assist with File IO
            /// </summary>
        public static class FileHelper
        {
            private static readonly  HudlLogger Log = new HudlLogger("FileHelper");

            /// <summary>
                    /// Attempts to retrieve the contents from a given file located with the given file path. The contents are retrieved and returned as a string.
                    /// The method checks for the file path in the base directory of the project.
                    /// <para>Ex. If the full path to the file is 
                    /// "c:\users\myuser\documents\visual studio 2015\Projects\TestProject\ApiProject\Requests\request.json" then the filePath parameter would be "Requests\request.json".</para>
                    /// </summary>
                    /// <param name="filePath"></param>
                    /// <returns></returns>
            public static string RetrieveFileContents(string filePath)
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + $"\\{filePath}";
                var fileContents = string.Empty;

                try
                {
                    using (var r = File.OpenText(path))
                    {
                        fileContents = r.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("Could not find file"))
                    {
                        Log.Error($"Unable to retrieve file contents. Make sure the file is set to copy to the output directory. {e.Message}");
                        Assert.Fail($"Unable to retrieve file contents. Make sure the file is set to copy to the output directory. {e.Message}");
                    }
                    else
                    {
                        Log.Error($"Unable to retrieve file contents. {e.Message}");
                        Assert.Fail($"Unable to retrieve file contents. {e.Message}");
                    }
                }

                return fileContents;
            }

            /// <summary>
                    /// Attempts to retrieve the contents from a given file name and a list of folder names. The folder names need to be entered as separate parameters and the method
                    /// will then create an array of those folder names and concatenate them to build a file path.
                    /// <para>Ex. RetrieveFileContents("myFileName.json", "BaseFolder", "SubFolder", "SubSubFolder1", "SubSubFolder2")</para>
                    /// </summary>
                    /// <param name="fileName"></param>
                    /// <param name="folders"></param>
                    /// <returns></returns>
            public static string RetrieveFileContents(string fileName, params string[] folders)
            {

                var fileContents = string.Empty;
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var sb = new StringBuilder();

                try
                {
                    sb.Append(path);

                    // looping through the array of folders and appending them to the directory path variable
                    foreach (var folder in folders)
                    {
                        sb.Append($"{folder}\\");
                    }

                    // adding the filename to the end of the full path 
                    sb.Append(fileName);

                    // retrieving the file contents 
                    using (var r = File.OpenText(sb.ToString()))
                    {
                        fileContents = r.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Unable to RetrieveFileContents. {e.Message}");
                    Assert.Fail($"Unable to RetrieveFileContents. {e.Message}");
                }

                return fileContents;
            }

            /// <summary>
                    /// Waits a given amount of time (in seconds) for a given file to be downloaded. The file will be downloaded to
                    /// the "bin/xxx/downloads" location in the project directory.
                    /// <para/>
                    /// The max timeout is 30 seconds by default. You can set it to be a different value otherwise the default value will be used.
                    /// </summary>
                    /// <param name="filename"></param>
                    /// <param name="maxTimeoutInSeconds"></param>
            public static void WaitForFileToDownload(string filename, int maxTimeoutInSeconds = 30)
            {
                var exists = false;

                // retrieves the download directory. if the "DownloadLocation" property in app.config 
                // is null then the "bin/xxx" location will be used.
                var filePath = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DownloadLocation"]?.ToString()) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : ConfigurationManager.AppSettings["DownloadLocation"].ToString();

                try
                {
                    for (int x = 0; x <= maxTimeoutInSeconds; x++)
                    {
                        if (!File.Exists($"{filePath}\\{filename}"))
                        {
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        Log.Error($"File: {filename} failed to download to path: {filePath} within the specified time limit: {maxTimeoutInSeconds} seconds.");
                        Assert.Fail($"File: {filename} failed to download to path: {filePath} within the specified time limit: {maxTimeoutInSeconds} seconds.");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Exception while trying to download file: {filename} to path: {filePath}. {e.Message}");
                    Assert.Fail($"Exception while trying to download file: {filename} to path: {filePath}. {e.Message}");
                }
            }

            /// <summary>
                    /// Waits a given amount of time (in seconds) for a file to be downloaded. The method checks for the file to contain
                    /// the <paramref name="partialFilename"/>. The file will be downloaded to the "bin/xxx/downloads" location in the project directory.
                    /// <para/>
                    /// The max timeout is 30 seconds by default. You can set it to be a different value otherwise the default value will be used.
                    /// </summary>
                    /// <param name="partialFilename"></param>
                    /// <param name="maxTimeoutInSeconds"></param>
            public static void WaitForPartialFilenameToDownload(string partialFilename, int maxTimeoutInSeconds = 30)
            {
                var exists = false;

                // retrieves the download directory. if the "DownloadLocation" property in app.config 
                // is null then the "bin/xxx" location will be used.
                var filePath = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DownloadLocation"]?.ToString()) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : ConfigurationManager.AppSettings["DownloadLocation"].ToString();

                try
                {
                    for (int x = 0; x <= maxTimeoutInSeconds; x++)
                    {
                        // generating the list of files in the filepath that contain the partial file name
                        var directoryFiles = new DirectoryInfo(filePath).GetFiles().Where(y => y.Name.Contains(partialFilename));

                        // if the list is empty then wait 1 second and try again
                        if (directoryFiles.Count() == 0)
                        {
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            exists = true;
                            break;
                        }
                    }

                    // checking if exists is true. this is mainly a check against the max timeout
                    if (!exists)
                    {
                        Log.Error($"File: {partialFilename} failed to download to path: {filePath} within the specified time limit: {maxTimeoutInSeconds} seconds.");
                        Assert.Fail($"File: {partialFilename} failed to download to path: {filePath} within the specified time limit: {maxTimeoutInSeconds} seconds.");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Exception while waiting for partial file download: {partialFilename} to path: {filePath}. {e.Message}");
                    Assert.Fail($"Exception while waiting for partial file download: {partialFilename} to path: {filePath}. {e.Message}");
                }
            }

            /// <summary>
                    /// Attempts to delete a file.
                    /// <para/>
                    /// If no <paramref name="filePath"/> is provided then the method will look to 
                    /// delete the file from the default directory of "bin/xxx/downloads" in the automation project.
                    /// </summary>
                    /// <param name="filename"></param>
                    /// <param name="filePath"></param>
            public static void DeleteFile(string filename, string filePath = "")
            {
                string path;

                // checking if the filepath parameter is null or empty. if it is then the path gets set to either the 
                // download location specified in app.config (if it exists) otherwise it gets set to "bin/xxx/downloads" in
                // the project directory
                if (string.IsNullOrEmpty(filePath))
                {
                    path = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DownloadLocation"]?.ToString()) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : ConfigurationManager.AppSettings["DownloadLocation"].ToString();
                }
                else
                {
                    path = filePath;
                }
                //var path = string.IsNullOrEmpty(filePath) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : filePath;

                try
                {
                    if (File.Exists($"{path}\\{filename}"))
                    {
                        File.Delete($"{path}\\{filename}");
                        Log.Info($"File: {filename} deleted from file path: {path}.");
                    }
                    else
                    {
                        Log.Info($"Couldn't delete file: {filename}. File not found using file path: {path}.");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to delete file: {filename} using file path: {path}. {e.Message}");
                    Assert.Fail($"Failed to delete file: {filename} using file path: {path}. {e.Message}");
                }
            }

            /// <summary>
                    /// Attempts to delete the first file that contains the <paramref name="partialFilename"/>.
                    /// <para/>
                    /// If no <paramref name="filePath"/> is provided then the method will look to 
                    /// delete the file from the default directory of "bin/xxx/downloads" in the automation project.
                    /// </summary>
                    /// <param name="partialFilename"></param>
                    /// <param name="filePath"></param>
                    /// <author>James Hulsey</author>
            public static void DeleteFileWithPartialName(string partialFilename, string filePath = "")
            {
                string path;

                // checking if the filepath parameter is null or empty. if it is then the path gets set to either the 
                // download location specified in app.config (if it exists) otherwise it gets set to "bin/xxx/downloads" in
                // the project directory
                if (string.IsNullOrEmpty(filePath))
                {
                    path = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DownloadLocation"]?.ToString()) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : ConfigurationManager.AppSettings["DownloadLocation"].ToString();
                }
                else
                {
                    path = filePath;
                }
                //var path = string.IsNullOrEmpty(filePath) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : filePath;

                try
                {
                    // first checking if there are any files in the directory
                    if (Directory.EnumerateFiles(path).Any())
                    {
                        // pulling back all the files in the directory that contain the partial file name
                        var filesToDelete = new DirectoryInfo(path).GetFiles().Where(x => x.Name.Contains(partialFilename));

                        if (filesToDelete.Any())
                        {
                            // deleting the first file in the list
                            File.Delete(filesToDelete.First().FullName);
                        }
                        else
                        {
                            Log.Info($"Directory: {path} didn't contain any files with partial name: {partialFilename}.");
                        }
                    }
                    else
                    {
                        Log.Info($"Directory: {path} didn't contain any files.");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to delete file with partial name: {partialFilename} using file path: {path}. {e.Message}");
                    Assert.Fail($"Failed to delete file with partial name: {partialFilename} using file path: {path}. {e.Message}");
                }
            }

            /// <summary>
                    /// Checks if a file exists with a given <paramref name="fileName"/>. 
                    /// <para/>
                    /// If <paramref name="filePath"/> has a value then the method will check in that location for the file. 
                    /// If <paramref name="filePath"/> is null or empty then the method will check if the file exists in the "DownloadLocation" app.config value (if it exists)
                    /// otherwise it will check in the "bin/xxx/downloads" directory in the automation project.
                    /// </summary>
                    /// <param name="fileName"></param>
                    /// <param name="filePath"></param>
                    /// <returns></returns>
                    /// <author>James Hulsey</author>
            public static bool DoesFileExist(string fileName, string filePath = "")
            {
                string path;

                // checking if the filepath parameter is null or empty. if it is then the path gets set to either the 
                // download location specified in app.config (if it exists) otherwise it gets set to "bin/xxx/downloads" in
                // the project directory
                if (string.IsNullOrEmpty(filePath))
                {
                    path = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DownloadLocation"]?.ToString()) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : ConfigurationManager.AppSettings["DownloadLocation"].ToString();
                }
                else
                {
                    path = filePath;
                }

                bool exists;

                try
                {
                    exists = File.Exists($"{path}\\{fileName}");
                    Log.Info($"File: {fileName} is present in path: {path}.");
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to check if file: {fileName} exists in path: {path}. {e.Message}");
                    Assert.Fail($"Failed to check if file: {fileName} exists in path: {path}. {e.Message}");
                    throw;
                }

                return exists;
            }

            /// <summary>
                    /// Checks if a file exists that has a filename that contains <paramref name="partialFilename"/>. 
                    /// <para/>
                    /// If <paramref name="filePath"/> has a value then the method will check in that location for the file. 
                    /// If <paramref name="filePath"/> is null or empty then the method will check if the file exists in the "DownloadLocation" app.config value (if it exists)
                    /// otherwise it will check in the "bin/xxx/downloads" directory in the automation project.
                    /// </summary>
                    /// <param name="partialFilename"></param>
                    /// <param name="filePath"></param>
                    /// <returns></returns>
                    /// <author>James Hulsey</author>
            public static bool DoesFileExistWithPartialName(string partialFilename, string filePath = "")
            {
                var exists = false;
                string path;

                // checking if the filepath parameter is null or empty. if it is then the path gets set to either the 
                // download location specified in app.config (if it exists) otherwise it gets set to "bin/xxx/downloads" in
                // the project directory
                if (string.IsNullOrEmpty(filePath))
                {
                    path = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DownloadLocation"]?.ToString()) ? $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\downloads" : ConfigurationManager.AppSettings["DownloadLocation"].ToString();
                }
                else
                {
                    path = filePath;
                }

                try
                {
                    // first checking if there are any files in the directory
                    if (Directory.EnumerateFiles(path).Any())
                    {
                        // pulling back all the files in the directory that contain the partial file name
                        exists = new DirectoryInfo(path).GetFiles().Where(x => x.Name.Contains(partialFilename)).Any();
                    }
                    else
                    {
                        Log.Info($"Directory: {path} didn't contain any files.");
                    }

                    Log.Info($"File with partial name: {partialFilename} is present in path: {path}.");
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to check if file with partial name: {partialFilename} exists in path: {path}. {e.Message}");
                    Assert.Fail($"Failed to check if file with partial name: {partialFilename} exists in path: {path}. {e.Message}");
                    throw;
                }

                return exists;
            }
        }
    }

