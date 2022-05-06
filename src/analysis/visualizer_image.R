library(ggplot2)

mnist_data = read.csv("mnist-fulldata.csv")
gtsrb_data = read.csv("gtsrb-fulldata.csv")
cifar_data = read.csv("cifar-fulldata.csv")

mnist_data_dense = mnist_data[mnist_data$EXTENSION != "CNN" , ]
mnist_data_cnn = mnist_data[mnist_data$EXTENSION != "DENSE" , ]
gtsrb_data_dense = gtsrb_data[gtsrb_data$EXTENSION != "CNN" , ]
gtsrb_data_cnn = gtsrb_data[gtsrb_data$EXTENSION != "DENSE" , ]
cifar_data_dense = cifar_data[cifar_data$EXTENSION != "CNN" , ]
cifar_data_cnn = cifar_data[cifar_data$EXTENSION != "DENSE" , ]


full = rbind(mnist_data, gtsrb_data, cifar_data)
full_cnn = rbind(mnist_data_cnn, gtsrb_data_cnn, cifar_data_cnn)
full_dense = rbind(mnist_data_dense, gtsrb_data_dense, cifar_data_dense)


## FULL DATA PLOT (RTP-P) 
ggplot(full, aes(x=P, y=RTP, color = D, shape = AT)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()


## RTP - P CNN DATA
ggplot(full_cnn, aes(x=P, y=RTP, color = D, shape= AT)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ exp(x/1000000)), se = FALSE, size = 1)+theme_bw()

ggplot(full_dense, aes(x=P, y=RTP, color = AT, size = 3, shape= D)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()


## FULL DATA PLOT (SW-P)
ggplot(full, aes(x=P, y=SW, color = AT, size = 3, shape= D)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

## FULL DATA PLOT (ACC-P)
ggplot(full, aes(x=P, y=A, color = AT, size = 3, shape= D)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

## CNN DATA PLOT (ACC-P)
ggplot(full_cnn, aes(x=P, y=A, color = AT, size = 3, shape= D)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()

## DENSE DATA PLOT (ACC-P)
ggplot(full_dense, aes(x=P, y=A, color = AT, size = 3, shape= D)) +
  geom_point(size = 4) +
  geom_smooth(method=lm,formula= (y ~ x), se = FALSE, size = 1)+theme_bw()