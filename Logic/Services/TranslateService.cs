using System;
using System.Collections.Generic;
using System.Text;
using Logic.Services.Interfases;

namespace Logic.Services
{

    public class TranslateService : ITranslateService
    {
        public string Translate(string word)
        {
            return word.ToLower();
        }
    }
}
