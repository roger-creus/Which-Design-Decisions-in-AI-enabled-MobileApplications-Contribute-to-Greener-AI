# Which-Design-Decisions-in-AI-enabled-MobileApplications-Contribute-to-Greener-AI

This repository contains the replication package for the paper "Which Design Decisions in AI enabled Mobile Applications Contribute to Greener AI" developed by Roger Creus Castanyer, Silverio Martínez-Fernández and Xavier Franch and accepted as an empirical confirmatory study at EMSE 2022. 

The repository is structured as follows:

```
├── README.md     <- The top-level README for developers using this project.
├── data
│       ├── accuracy tests    <- Contains the data used for systematically measuring the accuracy during the system operation phase
│       ├── dictionaries      <- Contains the word-to-int mapping learnt by the language models
│   	  └── results           <- Contains the analysis datasets with information of the AI models configuration and metrics on their performance
│                  ├── image      
│                  └── text
│
├── src	
│      ├── notebooks      <- Contains all the source code for developing the AI models for all vision and language domains (used as Kaggle notebooks)
│                   ├── image      
│                   ├── text      
│
│	
├── apps      <- Folder containing the source code of all AI-enabled mobile applications
│       ├── ImageClassificationUnity
│       ├── TextClassificationUnity
│		  
│
│
```
