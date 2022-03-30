using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{

    public NNModel model;
    private Model m_RuntimeModel;
    public Dictionary<int, string> idx_to_labels = new Dictionary<int, string>();
    public Dictionary<int, string> accuracy_test = new Dictionary<int, string>();
    public Dictionary<string, int> word_dictionary = new Dictionary<string, int>();
    public InputField twitInput;
    public Text outputText;

    public int nLayers;
    public int hidden_dim;

    public void measureAccuracy()
    {
        TextAsset f = (TextAsset)Resources.Load("twitter-accuracy-measure", typeof(TextAsset));
        string text = f.text;
        string[] lines = text.Split(',');
        float test_size = lines.Length;

        var worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, m_RuntimeModel);
        Tensor h = new Tensor(n: nLayers, h: 1, w: hidden_dim, c: 1);
        Tensor c = new Tensor(n: nLayers, h: 1, w: hidden_dim, c: 1);

        float accuracy_count = 0;
        foreach (string line in lines)
        {
            string[] line_to_words = line.Split(' ');
            int index = int.Parse(line_to_words[line_to_words.Length - 1]);

            string sentence = "";
            for (int i = 0; i < line_to_words.Length - 1; i++)
            {
                sentence += line_to_words[i] + " ";
            }

            
            for (int i = 0; i < nLayers; i++)
            {
                for(int j=0; j < hidden_dim; j++)
                {
                    h[i, 0, j, 0] = 0;
                    c[i, 0, j, 0] = 0;
                }
                
            }

            Tensor input = preprocessSentence(sentence, word_dictionary);
            var inputs = new Dictionary<string, Tensor>();
            inputs["x"] = input;
            inputs["1"] = h;
            inputs["2"] = c;

            worker.Execute(inputs);

            Tensor O = worker.PeekOutput();

            int class_output = getOutputPrediction(O);

            if(class_output == index)
            {
                accuracy_count += 1;
            }

            input.Dispose();
            O.Dispose();
            h.Dispose();
            c.Dispose();

        }
        Debug.Log((float)(accuracy_count / test_size) * 100);

    }


    public Dictionary<int, string> ReadLabels()
    {
        Dictionary<int, string> IDXS = new Dictionary<int, string>();
        TextAsset f = (TextAsset)Resources.Load("twitter-labels", typeof(TextAsset));

        if (f != null)
        {
            string text = f.text;
            string[] lines = text.Split(',');

            foreach (string line in lines)
            {
                string[] line_to_words = line.Split(' ');
                int index = int.Parse(line_to_words[1]);
                string fullname = "";
                for (int i = 0; i < line_to_words.Length - 1; i++)
                {
                    fullname += line_to_words[i];
                }

                IDXS[index] = fullname;

            }
        }
        return IDXS;
    }

    public Dictionary<string, int> ReadWordDictionary()
    {
        Dictionary<string, int > IDXS = new Dictionary<string, int>();
        TextAsset f = (TextAsset)Resources.Load("twitter-dictionary", typeof(TextAsset));

        if (f != null)
        {
            string text = f.text;
            string[] lines = text.Split(',');

            foreach (string line in lines)
            {
                string[] line_to_words = line.Split(' ');
                int index = int.Parse(line_to_words[1]);
                string fullname = "";
                for (int i = 0; i < line_to_words.Length - 1; i++)
                {
                    fullname += line_to_words[i];
                }

                
                IDXS[fullname] = index;

            }
        }

        return IDXS;
    }
    

    double[] Softmax(double[] oSums)

    {
        double max = oSums[0];
        for (int i = 0; i < oSums.Length; ++i)
            if (oSums[i] > max) max = oSums[i];

        double scale = 0.0;
        for (int i = 0; i < oSums.Length; ++i)
            scale += Math.Exp(oSums[i] - max);

        double[] result = new double[oSums.Length];
        for (int i = 0; i < oSums.Length; ++i)
            result[i] = Math.Exp(oSums[i] - max) / scale;

        return result;
    }




    Tensor preprocessSentence(string sentence, Dictionary<string, int> word_dictionary)
    {
        List<String> words = new List<String>();
        
        foreach (string word in sentence.Split(' '))
        {
            if (word_dictionary.ContainsKey(word))
            {
                words.Add(word);
            }
            
        }

        int sentence_length = words.Count;

        //Prepare text input
        Tensor input = new Tensor(n: 1, h: 1, w: 1, c: 30);
        for (int i = 0; i < 30 - sentence_length; i++)
        {
            input[0, 0, 0, i] = 0;
        }

        for (int i = 0; i < sentence_length; i++)
        {
            if (word_dictionary.ContainsKey(words[sentence_length - i - 1]))
            {
                input[0, 0, 0, 30 - 1 - i] = word_dictionary[words[sentence_length - i - 1]];
            }
            
        }

        return input;
    }

    int getOutputPrediction(Tensor O)
    {
        double[] outputs_probs = new double[O.length];
        for (int i = 0; i < O.length; i++)
        {
            outputs_probs[i] = O[0, 0, 0, i];  
        }


        if (outputs_probs[0] >= 0.5)
        {
            outputText.text = "The twit sentiment is positive with probability: " + ((float)System.Math.Round(outputs_probs[0] * 100, 2)).ToString();
            return 0;
        }
        else
        {
            outputText.text = "The twit sentiment is negative with probability: " + ((float)System.Math.Round(100 - (outputs_probs[0] * 100))).ToString();
            return 1;
        }
        
    }



    public void Predict()
    {
        var worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, m_RuntimeModel);

        Tensor h = new Tensor(n: nLayers, h: 1, w: hidden_dim, c: 1);
        Tensor c = new Tensor(n: nLayers, h: 1, w: hidden_dim, c: 1);

        for (int i = 0; i < nLayers; i++)
        {
            for (int j = 0; j < hidden_dim; j++)
            {
                h[i, 0, j, 0] = 0;
                c[i, 0, j, 0] = 0;
            }

        }

        string twit = twitInput.text;
        Tensor input = preprocessSentence(twit, word_dictionary);

        //Only for LSTM
        
        var inputs = new Dictionary<string, Tensor>();
        inputs["x"] = input;
        inputs["1"] = h;
        inputs["2"] = c;

        // For transformer -> feed input object only
        // For lstm -> feed inputs dictionary
        worker.Execute(inputs);

        Tensor O = worker.PeekOutput();

        int class_output = getOutputPrediction(O);
        Debug.Log(idx_to_labels[class_output]);


        input.Dispose();
        O.Dispose();
        h.Dispose();
        c.Dispose();

    }

    // Start is called before the first frame update
    void Start()
    {
        idx_to_labels = ReadLabels();
        word_dictionary = ReadWordDictionary();

        m_RuntimeModel = ModelLoader.Load(model);

        //Only for LSTM -> hidden states
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
