using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBoxCourseCSharp.Interface
{
    internal class ArticleComparer : IComparer
    {
        /// <summary>
        /// Интерфейс IComparer сосредоточен на реализации
        /// любого количества вспомогательных классов для сортировки.
        /// Метод Compare предназначен для сравнения двух объектов.
        /// Принцип сравнивания предоставлен в CompareTo(),
        /// где возвращаются значения -1, 0, 1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        int IComparer.Compare(object x, object y)
        {
            Product p1 = x as Product;
            Product p2 = y as Product;

            if (p1 == null || p2 == null)
                throw new ArgumentException("Оба объекта не Product");
            else
                return string.Compare(p1.Article.ToString(), p2.Article.ToString());
        }
    }
}
