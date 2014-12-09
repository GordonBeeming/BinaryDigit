using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

public static class DirectoryInfoExtensions
{
    #region Delegates

    public delegate void FileFoundDelegate(FileInfo fi);

    #endregion

    #region Public Methods and Operators

    public static void GetFiles(this DirectoryInfo di, string searchPattern, FileFoundDelegate callback, string groupName)
    {
        var bw = new BackgroundWorker();
        bw.DoWork += (sv, ev) =>
        {
            var startInfo = new ProcessStartInfo(@"cmd.exe");
            startInfo.CreateNoWindow = true;
            startInfo.ErrorDialog = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            Read(process.StandardOutput, callback, groupName);
            process.StandardInput.WriteLine("dir \"" + di.FullName.TrimEnd('\\') + "\\" + searchPattern + "\" /S /B");
            process.WaitForExit();
            string e = string.Empty;
        };
        bw.RunWorkerAsync();
    }

    #endregion

    #region Methods

    private static void Read(StreamReader reader, FileFoundDelegate callback, string groupName)
    {
        new Thread(() =>
        {
            var currentString = new StringBuilder();
            while (true)
            {
                int current;
                while ((current = reader.Read()) >= 0)
                {
                    currentString.Append((char)current);
                    //Console.Write((char)current);
                    if ((char)current == '\n')
                    {
                        if (File.Exists(currentString.ToString().Trim()))
                        {
                            var fi = new FileInfo(currentString.ToString().Trim());
                            callback(fi);
                        }
                        currentString = new StringBuilder();
                    }
                }
            }
        }).Start();
    }

    #endregion
}