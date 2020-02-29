using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using курсовая_работа;

namespace SyllablesTest
{
    [TestClass]
    public class SyllablesTest
    {
        [TestMethod]
        public void Divide_SeveralWords()
        {
            string word = "слог слово";
            string result = Syllables.DivideText(word);
            Assert.AreEqual("слог сло-во ", result);
        }

        [TestMethod]
        public void Rules_ZeroSlog()
        {
            string word = "слг";
            string result = Syllables.Rules(word);
            Assert.AreEqual("слг", result);
        }

        [TestMethod]
        public void Rules_GlasnSonornGlasn()
        {
            string word = "око";
            string result = Syllables.Rules(word);
            Assert.AreEqual("о-ко", result);
        }

        [TestMethod]
        public void Rules_OneSlog()
        {
            string word = "слог";
            string result = Syllables.Rules(word);
            Assert.AreEqual("слог", result);
        }

        [TestMethod]
        public void Rules_ShumnAfterGlasn()
        {
            string word = "роса";
            string result = Syllables.Rules(word);
            Assert.AreEqual("ро-са", result);
        }

        [TestMethod]
        public void Rules_DoubleLetter()
        {
            string word = "масса";
            string result = Syllables.Rules(word);
            Assert.AreEqual("ма-сса", result);
        }

        [TestMethod]
        public void Rules_DoubleShumn()
        {
            string word = "такса";
            string result = Syllables.Rules(word);
            Assert.AreEqual("та-кса", result);
        }

        [TestMethod]
        public void Rules_GlasnAfterGlasn()
        {
            string word = "диалог";
            string result = Syllables.Rules(word);
            Assert.AreEqual("ди-а-лог", result);
        }

        [TestMethod]
        public void Rules_SonornAfterJ()
        {
            string word = "стойло";
            string result = Syllables.Rules(word);
            Assert.AreEqual("стой-ло", result);
        }

        [TestMethod]
        public void Rules_SonornSonorn()
        {
            string word = "полный";
            string result = Syllables.Rules(word);
            Assert.AreEqual("по-лный", result);
        }

        [TestMethod]
        public void Rules_ContainsHyphren_GlasnSlog()
        {
            string word = "как-то";
            string result = Syllables.Rules(word);
            Assert.AreEqual("как-то", result);
        }

        [TestMethod]
        public void Rules_UnknownSymbol_NoSonorn()
        {
            string word = "вьюга";
            string result = Syllables.Rules(word);
            Assert.AreEqual("вью-га", result);
        }

        [TestMethod]
        public void Rules_UnknownSymbol_Sonorn()
        {
            string word = "слмъово";
            string result = Syllables.Rules(word);
            Assert.AreEqual("слмъо-во", result);

        }

        [TestMethod]
        public void Rules_ContainsHyphren_NoGlasn()
        {
            string word = "зачем-то";
            string result = Syllables.Rules(word);
            Assert.AreEqual("за-чем-то", result);
        }

        [TestMethod]
        public void Rules_GlasnShumnSonorn()
        {
            string word = "весна";
            string result = Syllables.Rules(word);
            Assert.AreEqual("ве-сна", result);
        }

        [TestMethod]
        public void Rules_GlasnSonornShumn()
        {
            string word = "оконце";
            string result = Syllables.Rules(word);
            Assert.AreEqual("о-кон-це", result);
        }

        [TestMethod]
        public void Rules_JShumn()
        {
            string word = "бойкий";
            string result = Syllables.Rules(word);
            Assert.AreEqual("бой-кий", result);
        }

        [TestMethod]
        public void Rules_UnknownSymbol_NorLetterNorHyphren()
        {
            string word = "сло8о";
            string result = Syllables.Rules(word);
            Assert.AreEqual("сло8-о", result);
        }

        [TestMethod]
        public void PrintSlog_firstPrintedfalse_cut0()
        {
            string result = "";
            StringBuilder slog = new StringBuilder("тест");
            bool firstPrinted = false;
            slog = Syllables.PrintSlog(slog, ref result, ref firstPrinted);
            Assert.AreEqual("", slog.ToString());
            Assert.AreEqual("тест", result);
        }

        [TestMethod]
        public void PrintSlog_firstPrintedfalse_cut2()
        {
            string result = "";
            StringBuilder slog = new StringBuilder("тест");
            bool firstPrinted = false;
            slog = Syllables.PrintSlog(slog, ref result, ref firstPrinted,2);
            Assert.AreEqual("ст", slog.ToString());
            Assert.AreEqual("те", result);
        }

        [TestMethod]
        public void PrintSlog_firstPrintedtrue_cut0()
        {
            string result = "";
            StringBuilder slog = new StringBuilder("тест");
            bool firstPrinted = true;
            slog = Syllables.PrintSlog(slog, ref result, ref firstPrinted);
            Assert.AreEqual("", slog.ToString());
            Assert.AreEqual("-тест", result);
        }

        [TestMethod]
        public void PrintSlog_firstPrintedtrue_cut2()
        {
            string result = "";
            StringBuilder slog = new StringBuilder("тест");
            bool firstPrinted = true;
            slog = Syllables.PrintSlog(slog, ref result, ref firstPrinted, 2);
            Assert.AreEqual("ст", slog.ToString());
            Assert.AreEqual("-те", result);
        }
    }
}
