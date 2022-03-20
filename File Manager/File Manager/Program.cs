using System;
using System.IO;


namespace File_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            string adres = Environment.CurrentDirectory;
            string[] folder = new string[20]; 
          
            
            Console.Write($"{adres} ");

            do {


                command = System.Console.ReadLine();

                string com = null;
                string objecA = null;
                string objecB = null;


                CommandHandler(command, ref com, ref objecA, ref objecB);

                

                if ((com == "ls") & (objecA == null))
                {
                    Console.Clear();
                    PrintDirectoriesFiles(adres, 0, 20, ref folder);
                }


                if ((com == "cd") & (Directory.Exists(folder[Convert.ToInt32(objecA)])) & (objecA != null)) //переход в новый директорий
                {
                    Console.Clear();
                    adres = folder[Convert.ToInt32(objecA)];
                }

                if ((com == "cd-") & (Directory.Exists(adres + "\\" + objecA))) //уход с директория
                {
                    Console.Clear();
                    command = Path.GetFileName(adres); //присвоили короткое наименования директории для подсчёта индекса
                    adres = adres.Remove(adres.Length - command.Length - 1); //уменьшили текущий адрес                    
                }

                if ((com == "p") & (objecA != null))
                {
                    Console.Clear();
                    PrintDirectoriesFiles(adres, Convert.ToInt32(objecA) * 20 - 20, Convert.ToInt32(objecA) * 20, ref folder);
                }

                if ((com == "cp") & (objecA != null) & (objecB != null))//копирование файлов и папок
                {
                    Console.Clear();
                    Coppy(folder[Convert.ToInt32(objecA)], objecB); 
                }

                if((com == "rm") & (objecA != null))//удаление файлов и папок
                {
                    Dellet(folder[Convert.ToInt32(objecA)], true);
                }

                if ((com == "file") & (File.Exists(folder[Convert.ToInt32(objecA)])))//читаем TXT
                {
                    Console.Clear();
                    StreamReader readText = new StreamReader(folder[Convert.ToInt32(objecA)]);
                    string text = readText.ReadToEnd();
                    Console.WriteLine($"Содержимое {text}");
                    Console.WriteLine("Нажмите любую клавишу для выхода с режима чтения:");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine($"Файл {Path.GetFileName(folder[Convert.ToInt32(objecA)])} закрыт");
                    readText.Close();
                }

                if(com == "?")
                {
                    Console.Clear();
                    Console.WriteLine("cd [Ind] // для перехода в новую директорию введите команду cd с номером папки" +
                        "\ncd- // выход с директории " +
                        "\np [№ строки] // переход на вторую строку " +
                        "\ncp [№ Ind] [адрес] // копирование файлов и целых директорий " +
                        "\nrm [Ind] // удаление файлов и папок " +
                        "\nfile [Ind] // запуск содержимомго файла формата TXT на экран консоли " +
                        "\nls // показать файлы и папки в директории");
                }

                Console.Write($"{adres} ");
            } while (command != "exit");
            Environment.Exit(0);




        }
        static void PrintDirectoriesFiles(string currentDir, int page, int pageSize, ref string[] DirFile)
        {

            string[] dirName = Directory.GetDirectories(currentDir); //массив папок
            string[] fileName = Directory.GetFiles(currentDir); //массив файлов

            int indFil = 0;
            int arrayIndex = 0;

            if(page != 0) //для корректного отражения файлов на странице 2 и далее
            {
                indFil = page - dirName.Length;
            }
            if (0 > indFil)
                indFil = 0;

            for (int i = page; i < pageSize; i++)
            {
                if (i < dirName.Length)
                {
                    DirFile[arrayIndex] = dirName[i];
                    Console.WriteLine($"Dir  {arrayIndex}\t {Path.GetFileName(DirFile[arrayIndex])}");
                }
                

                if ((i >= dirName.Length) & (indFil < fileName.Length))
                {
                    DirFile[arrayIndex] = fileName[indFil];
                    Console.WriteLine($"Fail {arrayIndex}\t {Path.GetFileName(DirFile[arrayIndex])} {arrayIndex}");
                    indFil++;
                }

                //если объектов меньше чем размер массива папок + файлов, замещаем данные
                if (i > dirName.Length + fileName.Length)
                {
                    DirFile[arrayIndex] = "-";
                    Console.WriteLine(DirFile[arrayIndex]);
                }
                arrayIndex++;
            }
            Console.WriteLine("Количество папок " + dirName.Length + " Количество файлов " + fileName.Length);
        }

        static void CommandHandler(string _command,ref string instruction, ref string objectA,ref string objectB)
        {

            string[] cutCommand = _command.Split(); //разбиваем одну команду на 3 объекта

            instruction = cutCommand[0];
            
            
            if(cutCommand.Length >= 2)
                objectA = cutCommand[1];
            
            if(cutCommand.Length >= 3)
                objectB = cutCommand[2];
            
        }

        static void Coppy(string source, string target)
        {
            if (!Directory.Exists(target)) //если директории нет создаём её
            {
                Directory.CreateDirectory(target);
                Console.WriteLine("Директория создана " + target);
            }

            if (Directory.Exists(source)) //проверяем наличие папки
            {
                foreach (string fileName in Directory.GetFiles(source))
                {
                    if (!File.Exists(target + "\\" + Path.GetFileName(fileName))) //проверяем на дубликаты
                    {
                        File.Copy(fileName, target + "\\" + Path.GetFileName(fileName));
                        Console.WriteLine($"Скопирован файл  {Path.GetFileName(fileName)}");
                    } else
                    {
                        string dublicatTime = Convert.ToString(DateTime.Now);
                        File.Copy(fileName, target + "\\" + "Дубль от " + dublicatTime.Replace(':', '.') + " " + Path.GetFileName(fileName));
                        Console.WriteLine($"Обнаружен дубликат {Path.GetFileName(fileName)} файл продублирован");
                    }
                }
                foreach (string rec in Directory.GetDirectories(source))
                {
                    Coppy(rec, target + "\\" + Path.GetFileName(rec));
                    Console.WriteLine($"Скопирована папка  {Path.GetFileName(rec)}");
                }
            }
            
            if (File.Exists(source)) //проверяем наличие и копируем файл
            {
               
                if (!File.Exists(target + "\\" + Path.GetFileName(source))) //проверяем на дубликаты
                {
                    File.Copy(source, target + "\\" + Path.GetFileName(source));
                    Console.WriteLine("Файл создан " + target);
                }
                else
                {
                    string dublicatTime = Convert.ToString(DateTime.Now);
                    File.Copy(source, target + "\\" + "Дубль от " + dublicatTime.Replace(':', '.') + " " + Path.GetFileName(source));
                    Console.WriteLine($"Обнаружен дубликат {Path.GetFileName(source)} файл продублирован");
                }
            }

        }

        static void Dellet (string source, bool target) //удаляем файл
        {
            Console.Clear();
            if (Directory.Exists(source)) 
            {
                Directory.Delete(source, true);
                Console.WriteLine("Папка и всё содержимое удалено " + source);
            }
            if (File.Exists(source))
            {
                Console.WriteLine("Файл удалён " + source);
                File.Delete(source);
            }
        }
    }
}