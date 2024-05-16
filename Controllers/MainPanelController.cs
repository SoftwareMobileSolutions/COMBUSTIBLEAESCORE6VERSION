using COMBUSTIBLEAESCORE.Models;
using COMBUSTIBLEAESCORE.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMBUSTIBLEAESCORE.Controllers
{
    public class MainPanelController : Controller
    {
        public async Task< IActionResult> MainPanel()
        {
            //ViewData["shortcuts"] = "XD";
            return View(ViewData["shortcuts"] = "XD");
        }


        protected JsonResult shortcuts()
        {
            var menu = _Sesion.Get<IEnumerable<ModulosModel>>(HttpContext.Session, "menu");

            var grandParent = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                url = t.UrlPage,
                Nivel = t.Nivel
            }).Where(t => t.Nivel == 0);

            var parent = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                parent = t.Parent,
                url = t.UrlPage,
                Nivel = t.Nivel
            }).Where(t => t.Nivel == 1);

            var children = menu.Select(t => new {
                Name = t.Nombre,
                codigo = t.Codigo,
                grandParent = t.GrandParent,
                parent = t.Parent,
                url = t.UrlPage,
                Nivel = t.Nivel
            }).Where(t => t.Nivel == 2);

            var arbolMenu = grandParent.Select(gp => new
            {
                name = gp.Name,
                codigo = gp.codigo,
                url = gp.url,
                nivel = gp.Nivel,
                HasChildren = parent.Any(),
                Children = parent.Select(p => new
                {
                    Name = p.Name,
                    codigo = p.codigo,
                    parent = p.parent,
                    url = p.url,
                    Nivel = p.Nivel,
                    HasChildren = children.Any(),
                    children = children.Select(c => new {
                        Name = c.Name,
                        codigo = c.codigo,
                        grandParent = c.grandParent,
                        parent = c.parent,
                        url = c.url,
                        Nivel = c.Nivel
                    })

                }).Where(p => p.parent == gp.codigo)
            });
            //var parent 


            return Json(arbolMenu);
        }

        public async Task<JsonResult> getShortcuts()
        {
            return (shortcuts());
        }
    }

    
}
