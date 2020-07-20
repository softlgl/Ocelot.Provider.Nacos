# Ocelot.Provider.Nacos
Ocelot集成Nacos注册中心组件

### 开发环境
+ .Net Core 3.1 因为最新稳定版的Ocelot是在.Net Core 3.1上构建的
+ Ocelot版本 v16.0.1
+ Nacos访问组件 [nacos-sdk-csharp-unofficial](https://github.com/catcherwong/nacos-sdk-csharp) v0.2.7

### 添加引用
```
<PackageReference Include="Ocelot.Provider.Nacos" Version="1.0.0" />
```
或
```
dotnet add package Ocelot.Provider.Nacos --version 1.0.0
```

### 使用方式
在已有的Ocelot的项目上添加以下内容，具体操作可查看[demo](https://github.com/softlgl/Ocelot.Provider.Nacos/tree/master/demo/ApiGatewayDemo)
```cs
public void ConfigureServices(IServiceCollection services)
{
    //注册服务发现
    services.AddOcelot().AddNacosDiscovery();
}
```
再已有的ocelot配置文件上添加
```json
{
  // 转发路由，数组中的每个元素都是某个服务的一组路由转发规则
  "Routes": [
    {
      "ServiceName": "productservice",
      // Uri方案，http、https
      "DownstreamScheme": "http",
      // 下游（服务提供方）服务路由模板
      "DownstreamPathTemplate": "/productapi/{everything}",
      // 上游（客户端，服务消费方）请求路由模板
      "UpstreamPathTemplate": "/productapi/{everything}",
      "UpstreamHttpMethod": [ "Get", "Post" ],
      "LoadBalancerOptions": {
        "Type": "RoundRobin" //轮询     
      },
      // 使用服务发现
      "UseServiceDiscovery": true
    }
  ],
  "GlobalConfiguration": {
    "ServiceDiscoveryProvider": {
      //这里是重点
      "Type": "Nacos"
    }
  }
}
```
然后添加在appsettings.json文件中添加,具体配置字段和nacos-sdk-csharp是保持一致的
```json
"nacos": {
    "ServerAddresses": [ "http://localhost:8848" ],
    "DefaultTimeOut": 15000,
    "Namespace": "",
    "ListenInterval": 1000,
    "ServiceName": "apigateway"
}
```


