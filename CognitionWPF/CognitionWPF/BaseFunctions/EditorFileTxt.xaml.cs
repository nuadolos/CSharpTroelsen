using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInput;

namespace CognitionWPF.BaseFunctions
{
    /// <summary>
    /// Логика взаимодействия для EditorFileTxt.xaml
    /// </summary>
    public partial class EditorFileTxt : Page
    {
        private byte[] binaryText;
        private string nameFile;

        public EditorFileTxt()
        {
            InitializeComponent();

            //Установка произвольных методов команды ApplicationCommands.Help 
            SetF1CommandBinding();

            //Запуск командной строки с командой ping 8.8.8.8 
            //Process cmd = new Process();
            //cmd.StartInfo.UseShellExecute = true;
            //cmd.StartInfo.FileName = "cmd.exe";
            //cmd.StartInfo.Arguments = "/C ping 8.8.8.8";
            //cmd.Start();
        }

        #region Не касается проекта

        /// <summary>
        /// Автокликер
        /// </summary>
        /// <returns></returns>
        //async Task Simulator()
        //{
        //    await Task.Run(() =>
        //    {
        //        InputSimulator sim = new InputSimulator();

        //        while (true)
        //        {
        //            Task.Delay(10000);
        //            sim.Mouse.LeftButtonClick();
        //        }
        //    });

        //Запуск командной строки с командой ping 8.8.8.8 
        //Process cmd = new Process();
        //cmd.StartInfo.UseShellExecute = true;
        //cmd.StartInfo.FileName = "cmd.exe";
        //cmd.StartInfo.Arguments = "/C ping 8.8.8.8";
        //cmd.Start();
        //}

        #endregion

        #region Открытие страницы VK в браузере

        /// <summary>
        /// Установка произвольных методов для команды ApplicationCommands.Help
        /// </summary>
        private void SetF1CommandBinding()
        {
            CommandBinding linkVkAvtor = new CommandBinding(ApplicationCommands.Help);
            linkVkAvtor.CanExecute += LinkVkAvtor_CanExecute;
            linkVkAvtor.Executed += LinkVkAvtor_Executed;
            CommandBindings.Add(linkVkAvtor);
        }

        /// <summary>
        /// Открытие браузера с указанным URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkVkAvtor_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/nuadolos");
            e.Handled = true;
        }

        /// <summary>
        /// Проверка прав администратора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkVkAvtor_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((bool)Application.Current.Properties["AdminRights"])
                e.CanExecute = true;
        }

        #endregion

        #region Кнопки вкладки File

        /// <summary>
        /// Переход на начальную страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseEditorExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //NavigationFrame.MainFrame.GoBack();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Нереализованная провека выполнения команды ApplicationCommands.Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseEditorCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Открытие текстового файла по выбранному расположению пути
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            StatusTxt.Text = "Opening";

            OpenFileDialog openTxt = new OpenFileDialog() { Filter = "Text Files (*.txt)|*.txt" };

            if (openTxt.ShowDialog() == true)
            {
                nameFile = openTxt.SafeFileName;

                using (StreamReader savingFile = new StreamReader(
                    new FileStream(openTxt.FileName, FileMode.Open, FileAccess.Read, FileShare.None)))
                {
                    EditorBox.Text = savingFile.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Нереализованная провека выполнения команды ApplicationCommands.Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Сохранение текстового файла по выбранному расположению пути
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            StatusTxt.Text = "Saving";

            SaveFileDialog saveTxt = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt" };

            if (saveTxt.ShowDialog() == true)
            {
                nameFile = saveTxt.SafeFileName;

                using (StreamWriter savingFile = new StreamWriter(
                    new FileStream(saveTxt.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)))
                {
                    savingFile.Write(EditorBox.Text);
                }
            }
        }

        /// <summary>
        /// Нереализованная провека выполнения команды ApplicationCommands.Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region Изменение статуса программы

        private void EditorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StatusTxt.Text = $"Редактирование файла {nameFile}";
        }

        #endregion

        #region Конвертация текста в двоичный код и наоборот
        
        private void ConvertTextToBinaryBtn_Click(object sender, RoutedEventArgs e)
        {
            string d = null;
            binaryText = Encoding.UTF8.GetBytes(EditorBox.Text);
            
            foreach (byte binary in binaryText)
            {
                byte binaryToD = binary;
                string tempD = null;

                if (binary != 0)
                {
                    while (binaryToD > 1)
                    {
                        tempD += binaryToD % 2;
                        binaryToD /= 2;
                    }

                    string reverseD = null;
                    for (int i = tempD.Length - 1; i >= 0; i--)
                    {
                        reverseD += tempD[i];
                    }

                    d += $"1{reverseD} ";
                }
            }

            EditorBox.Text = d;
        }

        private void ConvertBinaryToTextBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] d = EditorBox.Text.Split(' ');
            byte dByte = 0;
            List<byte> lByte = new List<byte>();

            foreach (string dCode in d)
            {
                for (int i = 0; i < dCode.Length; i++)
                {
                    if (dCode[i] == '1')
                        dByte += (byte)(1 * Math.Pow(2, dCode.Length - (i + 1)));
                }

                lByte.Add(dByte);
                dByte = 0;
            }
            
            binaryText = lByte.ToArray();
            EditorBox.Text = Encoding.UTF8.GetString(binaryText);
        }

        #endregion

        #region Исправление ошибки в слове

        private void EditorBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            SpellingHintsList.Children.Clear();

            SpellingError error = EditorBox.GetSpellingError(EditorBox.CaretIndex);
            if (error != null)
            {
                foreach (string s in error.Suggestions)
                {
                    if (SpellingHintsList.Children.Count < 5)
                    {
                        Button btn = new Button();
                        btn.Click += EditError;
                        btn.Background = Brushes.Transparent;
                        btn.Content = s;

                        SpellingHintsList.Children.Add(btn);
                    }
                }

                ExpanderSHL.IsExpanded = true;
            }
            else
                ExpanderSHL.IsExpanded = false;
        }

        private void EditError(object sender, RoutedEventArgs e)
        {
            int start = EditorBox.GetSpellingErrorStart(EditorBox.CaretIndex);
            int length = EditorBox.GetSpellingErrorLength(EditorBox.CaretIndex);

            string oldErrorText = EditorBox.Text.Substring(start, length);
            string newText = ((Button)sender).Content.ToString();

            EditorBox.Text = EditorBox.Text.Replace(oldErrorText, newText);

            SpellingHintsList.Children.Clear();
            ExpanderSHL.IsExpanded = false;
        }

        #endregion
    }
}
