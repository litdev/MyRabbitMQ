## RabbitMQDemo程序
学习记录

#### 通过Docker安装RabbitMQ

docker地址：https://hub.docker.com/_/rabbitmq

1. 安装最新的带Web管理界面的RabbitMQ：
```
    docker pull rabbitmq:management-alpine
```
2. 构建容器并运行
```
    docker run -dit --name MyRabbitMQ -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=admin -p 15672:15672 -p 5672:5672 rabbitmq:management-alpine
```
> 15672是管理界面的端口，5672是服务的端口，这里将管理系统的用户名和密码设置为admin admin

#### 笔记

RabbitMQ处理消息的流程
发布者 > 路由(ExChanges) > 管道(Channels) > 订阅者
> 如果多个订阅者订阅同一个管道，同一个消息只会有一个订阅者收到

管道需要跟路由进行绑定，共有四种类型

###### 1.Direct(发布/订阅模式)

管道和路由绑定的时候指定`routingKey`，一个标识，发布消息的时候，同样指定这个key，消息会被路由转发到指定`routingKey`的管道

###### 2. Fanout(广播模式)

管道和路由绑定时`无需`指定`routingKey`,一个路由绑定了多少个管道，那么发送消息时会同时转发给`所有绑定的管道`

###### 3. Topic(发布/订阅通配符模式)

在Direct基础上，`routingKey`支持通配符模糊查找，`*`匹配一个单词，`#`匹配多个单词，消息会被路由转发到模糊匹配的管道中
> 假如`ChannelA`指定的`routingKey='China.#'`，那么发送消息如果指定的`routingKey='China.News'`，就是匹配成功，消息会被发到`ChannelA`
> 假如`ChannelB`指定的`routingKey='#.News'`，那么发送消息如果指定的`routingKey='News.China'`，就是匹配成功，消息会被发到`ChannelB`
>贪婪匹配，如果所有Channel都符合routingKey的，则所有Channel都会收到消息

###### 4. Headers(通过头部信息进行过滤)
这个头部信息有点像Http请求中的报文头，更详细的介绍：https://www.cnblogs.com/wyt007/p/9078647.html