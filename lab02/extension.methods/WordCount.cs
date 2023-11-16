using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TPP.Laboratory.ObjectOrientation.Lab02 {

    static class StringExtesion {

        // Método extensor (¡static!) en clase static y con primer parámetro (a extender) antepuesto por 'this'
        static public uint CountWords(this string str) {
            // static type of var?
            // El sistema de tipos comprueba el tipo almacenado en var en tiempo de compilación
            var lines = Regex.Split(str, "\r|\n|[.]|[,]|[ ]");
            uint numberOfWords = 0;
            foreach (var line in lines)
                if (!String.IsNullOrEmpty(line) && !String.IsNullOrWhiteSpace(line))
                    numberOfWords++;
            return numberOfWords;
        }

    }


}
