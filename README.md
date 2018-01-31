Forked from https://github.com/SynHub/syn-bot-samples/tree/master/oscova-console-bot

#Oscova chatbot
There is a possibility to use some internal Oscova platform techniques that can help to enable better semantic analysis of tokens in user input.
 
- [Loading WordNet Database](https://developer.syn.co.in/tutorial/bot/oscova/load-wordnet.html)
- [Loading Word Vectors](https://developer.syn.co.in/tutorial/bot/oscova/load-word-vectors.html) (you can train your own words set or use pretrained one)
- Use [Oscova Machine Learning](https://developer.syn.co.in/tutorial/bot/oscova/machine-learning.html). This feature is available with a [license](https://developer.syn.co.in/tutorial/bot/activate-license.html) only and I was not able to test it. 
 
It's not clear how to test the understanding of user input by means of this platform because it works mostly as a black box. As you can see from simple examples below, enabling 1 and 2 together can help to better understand the meaning of some words. To make the chatbot understand sentences we need to hack the Oscova on low level (hook and transform user inputs - not sure if it's doable though) or consider using some other platform which support such advanced techniques as doc2vec etc.
 
 
## Some examples of chatting with Oscova console chatbot
The code of chatbot is taken from [SynHub](https://github.com/SynHub/syn-bot-samples/tree/master/oscova-console-bot).
To compile: 
- Install and open MS Visual Studio,
- Go to Tools->NuGet Package Manager->Package Manager Console
- Run install-package Syn.Bot
It will take several seconds for the app to run.
 
### 0 default mode
hello
Hello Bot developer!
hey
Please specify an App name.
hi
Please specify an App name.
bot
Hello Bot developer!
robot
Please specify an App name.
how are you
-

### 1 with WordNet (understands “hi”)
hello
Hello Bot developer!
hey
Please specify an App name.
hi
Hello Bot developer!
bot
Hello Bot developer!
robot
Please specify an App name.
how are you
(no answer)

### 2.1 with word2vec (glove.6B.50d.txt - understands “robot”)
hello
Hello Bot developer!
hey
Please specify an App name.
hi
Please specify an App name.
bot
Hello Bot developer!
robot
Hello Bot developer!
how are you
(no answer)

### 2.2 with word2vec (wiki.en.vec - shows worse results in my test than 2.1)
hello
Hello Bot developer!
hey
Please specify an App name.
hi
Please specify an App name.
bot
Hello Bot developer!
robot
Please specify an App name.
how are you
(no answer)
