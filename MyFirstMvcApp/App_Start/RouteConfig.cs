using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyFirstMvcApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UpdateProduct",
                url: "ürün/güncelle/{proId}",
                defaults: new { controller = "Product", action = "productUpdate" }
            );

            routes.MapRoute(
                name: "InsertProduct",
                url: "ürün/ekle",
                defaults: new { controller = "Product", action = "productInsert" }
            );

            routes.MapRoute(
                name: "DeleteProduct",
                url: "ürün/sil/{proId}",
                defaults: new { controller = "Product", action = "productDelete" }
            );

            routes.MapRoute(
                name: "ListProduct",
                url: "ürün/listele",
                defaults: new { controller = "Product", action = "productList" }
            );

            routes.MapRoute(
                name: "Login",
                url: "kullanıcı/giriş",
                defaults: new { controller = "User", action = "Login"}
            );

            routes.MapRoute(
                name: "ForgotPassword",
                url: "kullanıcı/şifremi_unuttum",
                defaults: new { controller = "User", action = "ForgotPassword" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
