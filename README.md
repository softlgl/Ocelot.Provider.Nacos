# Ocelot.Provider.Nacos
Ocelot集成Nacos注册中心组件

### 开发环境
+ .Net Core 3.1 因为最新稳定版的Ocelot是在.Net Core 3.1上构建的(目前以支持函.net5，由张队进行升级的)
+ Ocelot版本 v16.0.1(最新版已是17.0.0)
+ Nacos访问组件 [nacos-sdk-csharp](https://github.com/catcherwong/nacos-sdk-csharp)
  ```
  <PackageReference Include="nacos-sdk-csharp-unofficial" Version="0.2.7" />
  ```
  它其实是有一个asp.net core版本的组件，但是我没有选用，虽然那个用起来功能很强大，但是我需要自己改造一下，让它能更好的适配Ocelot

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
      // 用于服务发现的名称，也就是注册到nacos上的名称
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
    // 网关服务名称
    "ServiceName": "apigateway"
}
```


