## Minimal project to reproduce Microsoft.AspNetCore.OData 8.0.0-rc POST bug

1.
```
cp appsettings.example.json appsettings.json
```
2. Edit DefaultConnection in `appsettings.json` or remove mysql
3. Build project
4. When swagger opens press `try it out` on POST `/odata/users` with default user and press `execute` object and get:
```
fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
      An unhandled exception has occurred while executing the request.
      System.Runtime.Serialization.SerializationException: The last segment of the request URI 'Microsoft.OData.UriParser.ODataPath' was not recognized as an OData action.
         at Microsoft.AspNetCore.OData.Formatter.Deserialization.ODataActionPayloadDeserializer.GetAction(ODataDeserializerContext readContext)
         at Microsoft.AspNetCore.OData.Formatter.Deserialization.ODataActionPayloadDeserializer.ReadAsync(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
         at Microsoft.AspNetCore.OData.Formatter.ODataBodyModelBinder.ReadODataBodyAsync(ModelBindingContext bindingContext)
         at Microsoft.AspNetCore.OData.Formatter.ODataBodyModelBinder.BindModelAsync(ModelBindingContext bindingContext)
         at Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BinderTypeModelBinder.BindModelAsync(ModelBindingContext bindingContext)
         at Microsoft.AspNetCore.Mvc.ModelBinding.ParameterBinder.BindModelAsync(ActionContext actionContext, IModelBinder modelBinder, IValueProvider valueProvider, ParameterDescriptor parameter, ModelMetadata metadata, Obje
ct value, Object container)
         at Microsoft.AspNetCore.Mvc.Controllers.ControllerBinderDelegateProvider.<>c__DisplayClass0_0.<<CreateBinderDelegate>g__Bind|0>d.MoveNext()
      --- End of stack trace from previous location ---
         at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|19_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
         at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
         at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)
         at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
         at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
         at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)


```

**UPD**

I'm also trying to replace [FromODataBody] attribute to [FromBody], then I get `System.NullReferenceException`