using System;
using System.IO;
using Newtonsoft.Json.Linq;


            String myStringData = File.ReadAllText("testData.json");
            var jsonObject = JToken.Parse(myStringData);
            Console.WriteLine(jsonObject.SelectToken("username").Value<string>());

