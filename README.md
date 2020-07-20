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
  "Routes": [
    {
      "ServiceName": "productservice",
      "DownstreamScheme": "http",
      "DownstreamPathTemplate": "/productapi/{everything}",
      "UpstreamPathTemplate": "/productapi/{everything}",
      "UpstreamHttpMethod": [ "Get", "Post" ],
      "LoadBalancerOptions": {
        "Type": "RoundRobin"  
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


