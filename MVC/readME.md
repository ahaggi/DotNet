# ASP.NET Core MVC 	
 
## conventionally routing vs **attribute routing**
https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.1


MVC applications can mix the use of **conventional routing** and **attribute routing**. It's typical to use conventional routes for controllers serving HTML pages for browsers, and **attribute routing** for controllers serving REST APIs.

Actions are either conventionally routed or attribute routed. Placing a route on the controller or the action makes it attribute routed. Actions that define attribute routes cannot be reached through the conventional routes and vice-versa. Any route attribute on the controller makes all actions in the controller attribute routed.


### Conventional routing

At **Startup.Configure()** methode:
```
    app.UseMvc(routes => {
        routes.MapRoute( name: "default", template: "{controller=HelloWorldController}/{action=Index}/{id?}"); 

        routes.MapRoute(name:"blog", template: "blog/{*article}", defaults: new { controller = "Blog", action = "Article" });
    });
```



That will result at
    name: "default" + {controller=HelloWorldController} defines HelloWorldController as the default controller for the applicaion
    {action=Index} defines Index as the default action
    {id?} defines id as optional
*Will match*
	"/HelloWorld/index/10"
	"/HelloWorld/index"
	"/HelloWorld"
	"/"


**and** 

	name: "blog" + "blog/{*article}" dedicated route ,, "controller=" and "action=" don't appear in the route template 
    defaults: new { controller = "Blog", action = "Article" }
*Will match*
	"/blog/artical"
	"/blog/..../...artical"

> Every public method in a controller is callable as an HTTP endpoint = "actoin". In the sample, both methods return a string. Note the comments preceding each method

> Index() ==> default action 
> Something() ==> sub endpoint GET: /<ControllerName>/Something/
	
```
public class HelloWorldController : Controller {

        // GET: /HelloWorld/
        public string Index(){
            return "This is my default action...";
        }

        // GET: /HelloWorld/Welcome/ 
        public string Welcome(){
            return "This is the Welcome action method...";
        }

        /HelloWorld/???????????????
        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";
            return View();  //*******There's a view with the name of About.cshtml******
        }

}			
```

### Attribute routing **Easier**

#### Enabling Attribute Routing

In **WebApiConfig.Register(..)**

add

`config.MapHttpAttributeRoutes();`

**Enabling Attribute Routing in ASP.NET Core**

ASP.NET Core no longer uses Global.asax, web.config, or App_Start folders. Instead, all startup tasks are done in Startup.cs in the root of the project (see Application Startup). In ASP.NET Core MVC, attribute-based routing is now included by default when UseMvc() is called; and, this is the recommended approach for configuring Web API routes (and is how the Web API starter project handles routing).
    
In **Startup.Configure(..)**
add
    
`app.UseMvc();`

```
[Route("")] //defines MyDemoController as the default controller for the applicaion
public class MyDemoController : Controller
{
   [Route("")] //defines MyIndex as the default action
   [Route("Home")]
   [Route("Home/Index")]
   public IActionResult MyIndex()
   {
      return View("Index");
   }
   [Route("Home/About")]
   public IActionResult MyAbout()
   {
      return View("About");
   }
   [Route("Home/Contact")]
   public IActionResult MyContact()
   {
      return View("Contact");
   }
}
```

**Or Combining routes**

```
[Route("Home")]
public class MyDemoController : Controller
{
   [Route("")]
   [Route("Index")]
   public IActionResult MyIndex()   //"Home" or "Home/Index"
   {
      return View("Index");
   }
   [Route("About")]
   public IActionResult MyAbout()  //"Home/About"
   {
      return View("About");
   }
   [Route("Contact")]
   public IActionResult MyContact()  //"Home/Contact"
   {
      return View("Contact");
   }
}
```

**Or**

**Attribute routing with Http[Verb] attributes**

```
[HttpGet("/products")]
[HttpPost("/products")]

[Route("[controller]/[action]")]
public class ProductsController : Controller
{
    [HttpGet] // Matches '/Products/List'
    public IActionResult List() {
        // ...
    }

    [HttpGet("{id}")] // Matches '/Products/Edit/{id}'
    public IActionResult Edit(int id) {
        // ...
    }
}
```

**Or**
**Without action** 

```
[Route("api/[controller]")]
public class ProductsController : Controller
{
    [HttpGet] // Matches 'api/Products'
    public IActionResult List() {
        // ...
    }

    [HttpGet("{id}")] // Matches 'api/Products/{id}'
    public IActionResult Edit(int id) {
        // ...
    }
}
```

**Multiple Routes**

```[Route("Store")]
[Route("[controller]")]
public class ProductsController : Controller{
   [HttpPost("Buy")]     // Matches 'Products/Buy' and 'Store/Buy'
   [HttpPost("Checkout")] // Matches 'Products/Checkout' and 'Store/Checkout'
   public IActionResult Buy(){...}
}
```

##### Specifying attribute route optional parameters, default values, and constraints
`[HttpPost("product/{id:int}")]`



##### OBS 

> 1-
> name attr. in 
> 	[Route("Contact") Name = "Contact_info")]
> 
> 	routes.MapRoute(name: "blog"...
> 	routes.MapRoute(name: "default" ...



> 2-
> Route names have no impact on the URL matching behavior of routing and are only used for URL generation. Route names must be unique application-wide.
> They can be used at URL Generation
> https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.1#url-generation




> **Attribute routing + convention-based routing in The same projuct**
> 
> 	Attribute routing can be combined with convention-based routing. To define convention-based routes, call the MapHttpRoute method.
> 
> 	    WebApiConfig.Register(HttpConfiguration config){
> 	        // Attribute routing. ***FØRST***
> 	        config.MapHttpAttributeRoutes();
> 
> 	        // Convention-based routing.
> 	        config.Routes.MapHttpRoute(
> 		            name: "DefaultApi",
> 		            routeTemplate: "api/{controller}/{id}",
> 		            defaults: new { id = RouteParameter.Optional }
> 	        );
> 	    }
