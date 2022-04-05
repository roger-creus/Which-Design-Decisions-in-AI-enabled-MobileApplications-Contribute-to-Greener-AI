library(ggplot2)

twitter_data = read.csv("twitter-fulldata.csv")
imdb_data = read.csv("imdb-fulldata.csv")
amazon_data = read.csv("amazon-fulldata.csv")

twitter_data_emb = twitter_data[twitter_data$EXTENSION != "nn" , ]
twitter_data_nn = twitter_data[twitter_data$EXTENSION != "emb" , ]
imdb_data_emb = imdb_data[imdb_data$EXTENSION != "nn" , ]
imdb_data_nn = imdb_data[imdb_data$EXTENSION != "emb" , ]
amazon_data_emb = amazon_data[amazon_data$EXTENSION != "nn" , ]
amazon_data_nn = amazon_data[amazon_data$EXTENSION != "emb" , ]


full = rbind(twitter_data, imdb_data, amazon_data)
full_nn = rbind(twitter_data_nn, imdb_data_nn)
full_emb = rbind(twitter_data_emb, imdb_data_emb, amazon_data_emb)


## FULL DATA PLOT (RTP-P) 
ggplot(full, aes(x=P, y=RTP, color = D, shape = AT)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()


## RTP - P CNN DATA
ggplot(full_nn, aes(x=P, y=RTP, color = D, shape = AT, size = 3)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

ggplot(full_emb, aes(x=P, y=RTP, color = D, shape = AT, size = 3)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()


## FULL DATA PLOT (SW-P)
ggplot(full, aes(x=P, y=SW, color = D, shape = AT, size = 3)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

## FULL DATA PLOT (ACC-P)
ggplot(full, aes(x=P, y=A,color = D, shape = AT, size = 3)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

## CNN DATA PLOT (ACC-P)
ggplot(full_nn, aes(x=P, y=A, color = D, shape = AT, size = 3)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

## DENSE DATA PLOT (ACC-P)
ggplot(full_emb, aes(x=P, y=A,color = D, shape = AT, size = 3)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

