using System.IO;

namespace Parking.Logger
{
    internal class FileLogger : BaseLogger
    {
        string _filePath;
        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public override void Log(string message)
        {
            if (!File.Exists(_filePath)) File.Create(_filePath);

            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
