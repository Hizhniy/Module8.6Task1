using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите путь к папке для зачистки: ");
        string dirPath = Console.ReadLine();
        if (Directory.Exists(dirPath)) // проверяем, что директория существует
        {
            FolderFilesKills(dirPath, DateTime.Now.AddMinutes(-30)); // ...не понял как можно применить TimeSpan.FromMinutes(30)       
        }
        else Console.WriteLine("Папка по указанному пути не найдена...");
    }

    static void FolderFilesKills(string path, DateTime dateTime)
    {
        try
        {
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                if (File.GetLastWriteTime(f) < dateTime) // если файл протух, то удаляем
                {
                    File.Delete(f);
                }
            }
            var dirs = Directory.GetDirectories(path);
            foreach (var d in dirs)
            {
                if (Directory.GetCreationTime(d) < dateTime) // если папка протухла
                {
                    Directory.Delete(d, true); // удаляем папку со всем барахлом внутри
                    continue;
                }
                FolderFilesKills(d, dateTime);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}