using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkillBoxCourseCSharp.CustomException
{
    internal class CustException
    {
        public static void ExceptionStart()
        {
            try
            {
                int i = 0;
                if (int.TryParse("error?", out i))
                    MessageBox.Show("Круто");
                else
                    //throw new Exception("При преобразовании произошла ошибка");
                    throw new StringToIntException("При преобразовании типа string в int произошла ошибка",
                        new Exception("Прикол")) 
                    { HelpLink = "https://github.com/" };
            }
            catch (StringToIntException e)
            {
                MessageBox.Show($"StringToIntException:{e.Message}\nСсылка на справочник:{e.HelpLink}\n\n\nStackTrace:{e.StackTrace}\nHResult:{e.HResult}\nMethod:{e.TargetSite}");

                try
                {
                    //File.Open("", FileMode.Open);
                }
                //Выполнится при условии, что у исключения StringToIntException e.InnerException содержится внутреннее исключение
                catch (Exception e2) when (e.InnerException != null)
                {
                    throw new StringToIntException(e.Message, e2);
                }
            }
            finally
            {
                MessageBox.Show("Мне на ошибки все равно, кроме тех, которые преостанавливают работу приложения");
            }
            //catch (Exception er)
            //{
            //    MessageBox.Show($"Message:{er.Message}\nStackTrace:{er.StackTrace}\nHResult:{er.HResult}\nMethod:{er.TargetSite}");
            //}
            //catch
            //{
            //    MessageBox.Show("Общий catch для всех исключений");
            //}
        }
    }
}
