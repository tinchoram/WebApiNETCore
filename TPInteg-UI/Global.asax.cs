using System;
using System.IO;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace TPInteg_UI
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //Se agregan nuevas rutas
            RouteTable.Routes.MapPageRoute("ABMProveedores", "ABMProveedores",
                "~/Pages/Proveedores/Administracion.aspx");
            RouteTable.Routes.MapPageRoute("ListadoProveedores", "ListadoProveedores",
                "~/Pages/Proveedores/Listado.aspx");
            RouteTable.Routes.MapPageRoute("ABMProductos", "ABMProductos",
                "~/Pages/Productos/Administracion.aspx");
            RouteTable.Routes.MapPageRoute("ListadoProductos", "ListadoProductos",
                "~/Pages/Productos/Listado.aspx");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}