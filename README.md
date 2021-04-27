# Ocelot.Provider.Nacos
Ocelot集成Nacos注册中心组件

### 开发环境

#### Nacos 1.x
+ .Net Core 3.1 因为最新稳定版的Ocelot是在.Net Core 3.1上构建的(目前以支持.net5，由张队进行升级的)
+ Ocelot版本 v16.0.1(最新版已是17.0.0)
+ Nacos访问组件 [nacos-sdk-csharp](https://github.com/catcherwong/nacos-sdk-csharp)
  ```
  <PackageReference Include="nacos-sdk-csharp-unofficial" Version="0.2.7" />
  ```
  它其实是有一个asp.net core版本的组件，但是我没有选用，虽然那个用起来功能很强大，但是我需要自己改造一下，让它能更好的适配Ocelot
  
#### Nacos 2.0
+ .Net5
+ Ocelot版本 v17.0.0
+ Nacos访问组件 [nacos-sdk-csharp](https://github.com/nacos-group/nacos-sdk-csharp)

```
 <PackageReference Include="nacos-sdk-csharp" Version="1.1.0-alpha20210426120755" />
```

### 添加引用
不同版本支持naocs版本不一样

#### Naocs 1.x
```
<PackageReference Include="Ocelot.Provider.Nacos" Version="1.0.0" />
```
或
```
dotnet add package Ocelot.Provider.Nacos --version 1.0.0
```
<b>目前以支持.net5，请如有需要请引入最新的1.1.0版本</b>
```
<PackageReference Include="Ocelot.Provider.Nacos" Version="1.1.0" />
```
或
```
dotnet add package Ocelot.Provider.Nacos --version 1.1.0
```

#### Nacos 2.0

管理界面安装的话注意勾选 包括预览发行版，因为目前构建的是预览版
```
<PackageReference Include="Ocelot.Provider.Nacos" Version="1.2.0-preview.1" />
```
或
```
dotnet add package Ocelot.Provider.Nacos --version 1.2.0-preview.1
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
**使用Nacos 2.0的时候注意 nacos-sdk-csharp 1.1.0版本配置文件发生的变化，如果在Nacos 2.0管理界面的服务列表里展示服务，需要新建自己的命名空间并将NameSpace上填写Nacos的NameSpaceId,如下所示**
```json
"nacos": {
    "ServerAddresses": [ "http://192.168.219.1:8848" ],
    "ServiceName": "apigateway",
    "DefaultTimeOut": 15000,
    //自定义Namespace的Id
    "Namespace": "2ae308e2-7e8a-4602-9d1c-56508a3e263c",
    "GroupName": "DEFAULT_GROUP",
    "ClusterName": "DEFAULT",
    "ListenInterval": 1000,
    "RegisterEnabled": true,
    "InstanceEnabled": true,
    "LBStrategy": "WeightRandom",
    "NamingUseRpc": true
  }
```

