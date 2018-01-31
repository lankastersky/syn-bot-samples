using System;
using System.IO;
using Syn.Bot.Oscova;
using Syn.Oryzer.TextRepresentation;
using Syn.WordNet;

namespace OscovaConsoleBot
{
    internal class Program
    {
        const String ROOT_DIR = @"C:\Users\antonpopov\Downloads\";

        /** Loads WordNet, see {@link https://developer.syn.co.in/tutorial/bot/oscova/load-wordnet.html} */
        private static void LoadWordNet(OscovaBot bot)
        {
            var directory = ROOT_DIR + @"wordnet\dict";
            var wordNet = bot.Language.WordNet;

            //Data
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);

            //Indices
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);

            wordNet.Load();
        }

        /** Loads word vectors, see {@link https://developer.syn.co.in/tutorial/bot/oscova/load-word-vectors.html} */
        private static void LoadWordVectors(OscovaBot bot)
        {
            // Loaded from https://s3-us-west-1.amazonaws.com/fasttext-vectors/wiki.en.vec
            //var dbName = "wiki.en.vec";

            // Loaded from http://nlp.stanford.edu/data/glove.6B.zip
            var dbName = @"glove.6B\glove.6B.50d.txt";

            var fileStream = new FileStream(ROOT_DIR + dbName, FileMode.Open);
            bot.Language.WordVectors.Load(fileStream, VectorDataFormat.Text);
        }

        [STAThread]
        private static void Main(string[] args)
        {
            var bot = new OscovaBot();

            // License is required, see https://developer.syn.co.in/tutorial/bot/activate-license.html
            //bot.Configuration.ProcessingMode = ProcessingMode.Deep;

            Console.WriteLine("Loading WordNet");
            LoadWordNet(bot);

            Console.WriteLine("Loading word vectors");
            LoadWordVectors(bot);

            Console.WriteLine("Adding dialogs");
            bot.Dialogs.Add(new HelloBotDialog());
            bot.Dialogs.Add(new AppDialog());
            //bot.Dialogs.Add(new MyDialog());

            Console.WriteLine("Training the model");
            bot.Trainer.StartTraining();

            Console.WriteLine("Ready!");
            bot.MainUser.ResponseReceived += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Response.Text);
            };

            while (true)
            {
                var request = Console.ReadLine();
                var evaluationResult = bot.Evaluate(request);
                evaluationResult.Invoke();
            }
        }
    }
}
 