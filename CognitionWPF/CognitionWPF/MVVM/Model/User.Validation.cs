using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitionWPF.MVVM.Model
{
    public partial class User : Base.EntityBase, IDataErrorInfo
    {
        public string Error => "";

        #region Проверка значений 

        /// <summary>
        /// Проверка входного свойства на допустимое значение
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                bool hasError = false;

                switch (columnName)
                {
                    case nameof(Id):
                        if (AddErrors(nameof(Id), GetErrorsFromAnnotations(nameof(Id), Id)))
                            hasError = true;

                        break;

                    case nameof(Login):
                    {
                        if (LoginEqualsPassword())
                            hasError = true;

                        if (Login == "admin")
                        {
                            AddError(nameof(Login), $"Данный логин {Login} неявно зарегистрирован");
                            hasError = true;
                        }

                        //if (!hasError)
                        //    hasError = LoginEqualsPassword();

                        //if (!hasError)
                        //{
                        //    ClearErrors(nameof(Login));
                        //    ClearErrors(nameof(Password));
                        //}

                        if (AddErrors(nameof(Login), GetErrorsFromAnnotations(nameof(Login), Login)))
                            hasError = true;

                        break;
                    }

                    case nameof(Password):
                    {
                        hasError = LoginEqualsPassword();

                        //if (!hasError)
                        //{
                        //    ClearErrors(nameof(Login));
                        //    ClearErrors(nameof(Password));
                        //}

                        if (AddErrors(nameof(Password), GetErrorsFromAnnotations(nameof(Password), Password)))
                            hasError = true;

                        break;
                    }
                       
                    case nameof(Fullname):
                        if (AddErrors(nameof(Fullname), GetErrorsFromAnnotations(nameof(Fullname), Fullname)))
                            hasError = true;

                        break;
                }

                if (!hasError)
                    ClearErrors(columnName);

                return string.Empty;
            }
        }

        internal bool LoginEqualsPassword()
        {
            if (Login == Password)
            {
                AddError(nameof(Login), $"Логин ({Login}) не должен быть похож на пароль ({Password})");
                AddError(nameof(Password), $"Пароль ({Password}) не должен быть похож на логин ({Login})");
                return true;
            }
            else
            {
                ClearErrors(nameof(Login));
                ClearErrors(nameof(Password));
            }


            return false;
        }

        #endregion
    }
}
