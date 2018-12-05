using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace parknetApp
{
    class Program
    {
        static Hashtable hashTable = new Hashtable();
        static string downloadName = "mobyDeneme";
        static void Main(string[] args)
        {
            downloadBook();
            findCountOfWords();
        }
        private static void findCountOfWords()
        {
            StreamReader streamReader;
            string bookTxt = "";
            streamReader = File.OpenText(@"/Users/capi/Desktop/Projects/C#/parknetApp/" + downloadName + ".txt");
            bookTxt = streamReader.ReadToEnd();
            streamReader.Close();
            bookTxt = Regex.Replace(bookTxt, "\\.|;|:|,|[0-9]|'", "");
            MatchCollection matchColl = Regex.Matches(bookTxt, @"[\w]+", RegexOptions.Multiline);

            LinkedList<string> wordList = new LinkedList<string>();
            //Hashtable fHash = new Hashtable();
            LinkedList<string> uniqueWord = new LinkedList<string>();

            for (int i = 0; i < matchColl.Count; i++)
            {
                wordList.AddLast(matchColl[i].ToString().ToLower().Trim());
            }

            foreach (var word in wordList)
            {
                if (uniqueWord.Contains(word))
                {
                    int wordCount = int.Parse(hashTable[word].ToString());
                    wordCount++;
                    hashTable[word] = wordCount;
                }
                else
                {
                    uniqueWord.AddLast(word);
                    hashTable.Add(word, 1);
                }
            }
            Console.WriteLine("uniq words: " + uniqueWord.Count);
            Console.WriteLine("total words :" + wordList.Count);
            writeToXml();
        }
        private static void writeToXml()
        {
            StreamWriter streamWriter;
            streamWriter = File.AppendText(@"/Users/capi/Desktop/Projects/C#/parknetApp/" + downloadName + ".xml");
            XmlTextWriter xmlWriter = new XmlTextWriter(streamWriter);
            xmlWriter.WriteStartElement("words");

            foreach (string word in hashTable.Keys)
            {
                xmlWriter.WriteWhitespace("\n  ");
                xmlWriter.WriteStartElement("word");
                //xmlWriter.WriteWhitespace("\n  ");
                xmlWriter.WriteAttributeString("text", word);
                //xmlWriter.WriteWhitespace("\n  ");
                xmlWriter.WriteAttributeString("count", hashTable[word].ToString());
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteWhitespace("\n  ");
            xmlWriter.Close();
        }
        private static void downloadBook()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("http://www.gutenberg.org/files/2701/2701-0.txt", downloadName + ".txt");
        }
    }
}